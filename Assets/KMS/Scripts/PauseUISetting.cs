using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUISetting : Singleton<PauseUISetting>
{
    private void Awake()
    {
        SetInstance();
    }
}
