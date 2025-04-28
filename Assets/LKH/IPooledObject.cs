using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObject
{
    void Reset();
    GameObject GetGameObject();
    public void ReturnPool();
}
