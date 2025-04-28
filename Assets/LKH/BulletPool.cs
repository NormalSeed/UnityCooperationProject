using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour, IObjectPool
{
    [SerializeField] List<PooledBullet> pool = new List<PooledBullet>();
    [SerializeField] PooledBullet prefab;
    [SerializeField] int poolSize;

    private void Awake()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            PooledBullet instance = Instantiate(prefab);
            instance.gameObject.SetActive(false);
            pool.Add(instance);
        }
    }
    public IPooledObject GetObject(Vector3 position, Quaternion rotation)
    {
        if (pool.Count == 0)
        {
            return Instantiate(prefab);
        }

        PooledBullet instance = pool[pool.Count - 1];
        pool.RemoveAt(pool.Count - 1);

        instance.poolToReturn = this;
        instance.transform.position = position;
        instance.transform.rotation = rotation;
        instance.gameObject.SetActive(true);

        return (PooledBullet)instance;
    }

    public void ReturnObject(IPooledObject instance)
    {
        instance.ResetObject();
        pool.Add((PooledBullet)instance);
    }
}
