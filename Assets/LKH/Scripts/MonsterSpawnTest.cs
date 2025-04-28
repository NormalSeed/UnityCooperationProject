using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnTest : MonoBehaviour
{
    [SerializeField] MonsterPool monsterPool;
    private Coroutine spawnCoroutine;

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
    }

    public void SpawnMonster()
    {
        PooledMonster monster = (PooledMonster)monsterPool.GetObject(transform.position, transform.rotation);
        Debug.Log($"{monster}�� �÷��̾ �Ѿư��ϴ�");
    }
    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            SpawnMonster();
            yield return new WaitForSeconds(3f);
        }
    }
}
