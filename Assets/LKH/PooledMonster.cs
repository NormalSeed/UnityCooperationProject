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
