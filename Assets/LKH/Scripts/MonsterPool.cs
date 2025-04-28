using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterPool : MonoBehaviour, IObjectPool
{
    [SerializeField] public List<PooledMonster> pool = new List<PooledMonster>();
    [SerializeField] PooledMonster prefab;
    [SerializeField] int poolSize;

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            PooledMonster instance = Instantiate(prefab);
            instance.gameObject.SetActive(false);
            pool.Add(instance);
        }
    }

    public IPooledObject GetObject(Vector3 position, Quaternion rotation)
    {
        if (pool.Count == 0)
        {

        }
        PooledMonster instance = pool[pool.Count - 1];
        pool.RemoveAt(pool.Count - 1);
        instance.poolToReturn = this;
        instance.transform.position = position;
        instance.transform.rotation = rotation;
        instance.gameObject.SetActive(true);

        return (PooledMonster)instance;
    }

    public void ReturnObject(IPooledObject instance)
    {
        instance.ResetObject();
        pool.Add((PooledMonster)instance);
    }
}
