using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledMonster : MonoBehaviour, IPooledObject
{
    public MonsterPool poolToReturn;

    public void ResetObject()
    {
        gameObject.SetActive(false);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void ActiveReturn()
    {
        if (poolToReturn.pool.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ReturnPool();
            }
        }
    }

    public void ReturnPool()
    {
        if (poolToReturn == null)
        {
            Destroy(gameObject);
        }
        else
        {
            poolToReturn.ReturnObject(this);
        }
    }
}
