using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterReturnTest : MonoBehaviour
{
    [SerializeField] PooledMonster monster;

    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            monster.ReturnPool();
        }
    }
}
