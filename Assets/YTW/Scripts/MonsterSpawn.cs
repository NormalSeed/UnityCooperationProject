using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    [SerializeField] MonsterPool monsterPool;
    [SerializeField] float coolTime; 
    private Coroutine spawnCoroutine;

    private void Update()
    {
        
        if (spawnCoroutine == null && monsterPool.pool.Count > 0)
        {
            spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }
        else if (spawnCoroutine != null && monsterPool.pool.Count == 0)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    public void SpawnMonster()
    {
        monsterPool.GetObject(transform.position, transform.rotation);
    }
    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(coolTime);
            SpawnMonster();
        }
    }
}
