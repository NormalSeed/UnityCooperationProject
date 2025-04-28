using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public interface IObjectPool
{
    public IPooledObject GetObject();

    public void ReturnObject();
}
