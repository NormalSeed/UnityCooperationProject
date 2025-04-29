using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public interface IObjectPool
{
    public IPooledObject GetObject(Vector3 position, Quaternion rotation);

    public void ReturnObject(IPooledObject instance);
}
