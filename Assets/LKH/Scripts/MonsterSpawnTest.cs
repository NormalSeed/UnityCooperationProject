using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterSpawnTest : MonoBehaviour
{
    [SerializeField] MonsterPool monsterPool;
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
        else if(spawnCoroutine == null && monsterPool.pool.Count > 0)
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
        Debug.Log($"{monster}가 플레이어를 쫓아갑니다");
    }
    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            SpawnMonster();
        }
    }
}
