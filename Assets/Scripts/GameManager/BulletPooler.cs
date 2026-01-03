using System.Collections.Generic;
using UnityEngine;

public class BulletPooler : MonoBehaviour
{
    public static BulletPooler Instance;

    [SerializeField] private GameObject smallBulletPrefab;
    [SerializeField] private GameObject bigBulletPrefab;
    [SerializeField] private int smallBulletPoolSize = 30;
    [SerializeField] private int bigBulletPoolSize = 2;

    private Dictionary<string, Queue<GameObject>> bulletPools = new();
    private Dictionary<string, GameObject> bulletPrefabs = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        InitialisePool();
    }

    public GameObject GetBullet(string bulletType)
    {
        if (!bulletPools.ContainsKey(bulletType))
        {
            Debug.LogError($"Bullet type inconnu: {bulletType}");
            return null;
        }

        if (bulletPools[bulletType].Count > 0)
        {
            GameObject bullet = bulletPools[bulletType].Dequeue();
            bullet.SetActive(true);
            return bullet;
        }

        GameObject newBullet = Instantiate(bulletPrefabs[bulletType]);
        return newBullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);

        string type = bullet.CompareTag("bigBullet") ? "big" : "small";
        bulletPools[type].Enqueue(bullet);
    }

    private void InitialisePool()
    {
        bulletPrefabs["small"] = smallBulletPrefab;
        bulletPrefabs["big"] = bigBulletPrefab;

        bulletPools["small"] = new Queue<GameObject>();
        bulletPools["big"] = new Queue<GameObject>();

        for (int i = 0; i < smallBulletPoolSize; i++)
        {
            GameObject bullet = Instantiate(smallBulletPrefab);
            bullet.SetActive(false);
            bulletPools["small"].Enqueue(bullet);
        }

        for (int i = 0; i < bigBulletPoolSize; i++)
        {
            GameObject bullet = Instantiate(bigBulletPrefab);
            bullet.SetActive(false);
            bulletPools["big"].Enqueue(bullet);
        }
    }
}
