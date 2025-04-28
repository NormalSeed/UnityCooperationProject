using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObject
{
    void ResetObject();
    GameObject GetGameObject();
    public void ReturnPool();
}
