using System.Collections.Generic;
using UnityEngine;

public class BulletPooler : MonoBehaviour
{
    public static BulletPooler Instance;

    [SerializeField] private GameObject smallBulletPrefab;
    [SerializeField] private GameObject bigBulletPrefab;
    [SerializeField] private int smallBulletPoolSize = 30;
    [SerializeField] private int bigBulletPoolSize = 2;

    //private Queue<GameObject> smallBulletPool = new Queue<GameObject>();
    //private Queue<GameObject> bigBulletPool = new Queue<GameObject>();
    private Dictionary<string, Queue<GameObject>> bulletPools = new();
    private Dictionary<string, GameObject> bulletPrefabs = new();
    void Awake()
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
        if (bulletPools[bulletType].Count > 0)
        {
            var bullet = bulletPools[bulletType].Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        
        GameObject bullet = Instantiate(smallBulletPrefab);
        return bullet;
        
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        smallBulletPool.Enqueue(bullet);
    }
    public void InitialisePool()
    {
        // Associe chaque type à son prefab
        bulletPrefabs["small"] = smallBulletPrefab;
        bulletPrefabs["big"] = bigBulletPrefab;

        // Initialise les pools vides
        bulletPools["small"] = new Queue<GameObject>();
        bulletPools["big"] = new Queue<GameObject>();

        for (int i = 0; i < smallBulletPoolSize; i++)
        {
            GameObject bullet = Instantiate(smallBulletPrefab);
            bullet.SetActive(false);
            smallBulletPool.Enqueue(bullet);
        }

        for (int i = 0; i < bigBulletPoolSize; i++)
        {
            GameObject bullet = Instantiate(bigBulletPrefab);
            bullet.SetActive(false);
            bigBulletPool.Enqueue(bullet);
        }
    }
    public void ReinitialisePool()
    {
        smallBulletPool.Clear();
        InitialisePool();
    }
}
