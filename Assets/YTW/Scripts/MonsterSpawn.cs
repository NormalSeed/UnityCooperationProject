using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    [SerializeField] MonsterPool monsterPool;
    [SerializeField] float coolTime;
    [SerializeField] float spawnRadius = 10f;  // 스폰 범위
    [SerializeField] Transform player;

    private Coroutine spawnCoroutine;

    private void Update()
    {
        Init();
    }

    void Init()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        bool playerInRange = distanceToPlayer <= spawnRadius;

        if (playerInRange && spawnCoroutine == null && monsterPool.pool.Count > 0)
        {
            spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }
        else if ((!playerInRange || monsterPool.pool.Count == 0) && spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }
    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(coolTime);
            SpawnMonster();
        }
    }
    public void SpawnMonster()
    {
        monsterPool.GetObject(transform.position, transform.rotation);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
