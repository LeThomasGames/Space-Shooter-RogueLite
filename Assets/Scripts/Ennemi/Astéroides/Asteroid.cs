using UnityEngine;
public class Asteroid : EnemyBase
{
    [Header("Asteroid Settings")]
    public AsteroidSize size;
    public GameObject asteroidPrefab;
    public int minSplitCount = 2;
    public int maxSplitCount = 4;

    [Header("Asteroid_Data")]
    public AsteroidData huge;
    public AsteroidData large;
    public AsteroidData medium;
    public AsteroidData small;
    private void Start()
    {
        AssignRandomSprite();
        AssignHealth();
        AssignContactDamage();
    }
    void AssignHealth()
    {
        maxHealth = size switch
        {
            AsteroidSize.Huge => huge.maxHealth,
            AsteroidSize.Large => large.maxHealth,
            AsteroidSize.Medium => medium.maxHealth,
            AsteroidSize.Small => small.maxHealth,
            _ => 0
        };
        currentHealth = maxHealth;
    }
    void AssignRandomSprite()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Sprite[] chosenArray = size switch
        {
            AsteroidSize.Huge => huge.sprites,
            AsteroidSize.Large => large.sprites,
            AsteroidSize.Medium => medium.sprites,
            AsteroidSize.Small => small.sprites,
            _ => null
        };

        if (chosenArray != null && chosenArray.Length > 0 && sr != null)
        {
            sr.sprite = chosenArray[Random.Range(0, chosenArray.Length)];
        }

        PolygonCollider2D poly = GetComponent<PolygonCollider2D>();
        if (poly != null)
        {
            Destroy(poly); // Supprime l'ancien collider
            gameObject.AddComponent<PolygonCollider2D>(); // Crée un nouveau qui corespond au sprite
        }
    }
    void AssignContactDamage()
    {
        contactDamage = size switch
        {
            AsteroidSize.Huge => huge.contactDamage,
            AsteroidSize.Large => large.contactDamage,
            AsteroidSize.Medium => medium.contactDamage,
            AsteroidSize.Small => small.contactDamage,
            _ => 5
        };
    }

    protected override void Die()
    {
        if (size != AsteroidSize.Small && asteroidPrefab != null)
        {
            PolygonCollider2D parentPoly = GetComponent<PolygonCollider2D>();

            float minRadius = parentPoly.bounds.extents.magnitude;

            // Rayon de « clearance » pour vérifier les collisions autour du point de spawn
            float clearanceRadius = parentPoly.bounds.extents.magnitude * 0.5f;

            int splitCount = Random.Range(minSplitCount, maxSplitCount + 1);

            for (int i = 0; i < splitCount; i++)
            {
                Vector2 spawnPos = Vector2.zero;
                bool found = false;

                // On tente plusieurs positions jusqu'à en trouver une libre
                for (int attempt = 0; attempt < 10; attempt++)
                {
                    // 1) Direction aléatoire
                    Vector2 dir = Random.insideUnitCircle.normalized;
                    // 2) Distance de spawn entre minRadius et 1.5×minRadius
                    float dist = Random.Range(minRadius + 2, minRadius + 2 * 1.5f);
                    Vector2 candidatePos = (Vector2)transform.position + dir * dist;

                    // 3) Vérifie qu'il n'y a pas déjà un collider à proximité
                    if (Physics2D.OverlapCircle(candidatePos, clearanceRadius) == null)
                    {
                        spawnPos = candidatePos;
                        found = true;
                        break;
                    }
                }

                // Si aucune position libre n'a été trouvée, on tombe sur un offset minRadius
                if (!found)
                {
                    Vector2 dir = Random.insideUnitCircle.normalized;
                    spawnPos = (Vector2)transform.position + dir * minRadius;
                }

                // Instanciation
                GameObject split = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
                Debug.Log($"Spawned split #{i} — components on clone : {split.GetComponents<Component>().Length}");

                // Ajuste la taille du nouveau fragment
                if (split.TryGetComponent<Asteroid>(out var asteroidScript))
                {
                    asteroidScript.size = GetSmallerSize(size);
                }

                // Donne une vitesse/direction aléatoire
                if (split.TryGetComponent<Rigidbody2D>(out var rb))
                {
                    Vector2 randomDir = Random.insideUnitCircle.normalized;
                    rb.linearVelocity = randomDir * Random.Range(1f, 3f);
                }
            }
        }

        base.Die();
        Destroy(gameObject);
    }
    public Vector2 GetRandomPointInside(PolygonCollider2D poly)
    {
        // Calculer le bounding box du collider
        Bounds bounds = poly.bounds;

        for (int attempts = 0; attempts < 100; attempts++)
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            Vector2 point = new Vector2(x, y);

            if (poly.OverlapPoint(point))
            {
                return point;
            }
        }

        Debug.LogWarning("Aucun point valide trouvé dans le PolygonCollider après 100 tentatives.");
        return poly.bounds.center;
    }

    AsteroidSize GetSmallerSize(AsteroidSize current)
    {
        return current switch
        {
            AsteroidSize.Huge => AsteroidSize.Large,
            AsteroidSize.Large => AsteroidSize.Medium,
            AsteroidSize.Medium => AsteroidSize.Small,
            _ => AsteroidSize.Small
        };
    }

    protected override void PlayDeathEffect()
    {

    }
}