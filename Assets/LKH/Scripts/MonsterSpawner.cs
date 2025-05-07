using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] MonsterPool monsterPool;
    [SerializeField] float spawnTimer;
    private Coroutine spawnCoroutine;
    private PooledMonster monster;

    private void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    private void Update()
    {
        if (spawnCoroutine != null && monsterPool.pool.Count == 0)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
        else if (spawnCoroutine == null && monsterPool.pool.Count > 0)
        {
            spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            monster.ReturnPool();
        }
    }

    public void SpawnMonster()
    {
        monster = (PooledMonster)monsterPool.GetObject(transform.position, transform.rotation);
    }
    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTimer);
            SpawnMonster();
        }
    }
}
