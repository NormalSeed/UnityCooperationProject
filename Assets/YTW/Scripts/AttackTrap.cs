using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackTrap : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float attackCoolTime = 1.0f;
    [SerializeField] float speedDebuffPercent = 0.3f;

    private List<MonsterController> monstersInTrap = new List<MonsterController>();
    private Dictionary<MonsterController, float> lastAttackTimes = new Dictionary<MonsterController, float>();
    private Dictionary<MonsterController, float> originalSpeeds = new Dictionary<MonsterController, float>();

    private void Update()
    {
        // 트랩내 몬스터가 없다고 불필요하게 돌릴 필요 없으니
        if (monstersInTrap.Count == 0) return;
        AttackCool();
    }

    private void OnTriggerEnter(Collider other)
    {

        MonsterController monster = other.GetComponent<MonsterController>();
        // 몬스터가 null이 아니고 트랩안몬스터에 몬스터가 없다면
        if (monster != null && !monstersInTrap.Contains(monster))
        {
            // 트랩안 몬스터 추가
            monstersInTrap.Add(monster);
            Debug.Log("몬스터가 트랩에 들어옴");

            // 속도감소를 위해 NavMeshAgent컴포넌트 가져옴
            NavMeshAgent agent = monster.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                // 각 몬스터마다 속도 디버프를 적용
                if (!originalSpeeds.ContainsKey(monster))
                {
                    originalSpeeds[monster] = agent.speed;
                }
                agent.speed = originalSpeeds[monster] * (1f - speedDebuffPercent);
            }

        }

    }

    private void OnTriggerExit(Collider other)
    {
        MonsterController monster = other.GetComponent<MonsterController>();
        {
            if(monster != null && monstersInTrap.Contains(monster))
            {
                monstersInTrap.Remove(monster);
                lastAttackTimes.Remove(monster);

                NavMeshAgent agent = monster.GetComponent<NavMeshAgent>();
                if (agent != null && originalSpeeds.ContainsKey(monster))
                {
                    agent.speed = originalSpeeds[monster];
                }
            }
            Debug.Log("몬스터가 트랩에서 나감");
        }
    }

    public void Attack(MonsterController monster)
    {
        OnDamaged targetHP = monster.GetComponent<OnDamaged>();
        targetHP?.TakeDamaged(damage);
        Debug.Log($"몬스터 공격 남은 HP : {targetHP.CURHP}");
    }

    public void AttackCool()
    {
        for (int i = monstersInTrap.Count - 1; i >= 0; i--)
        {
            MonsterController monster = monstersInTrap[i];
            if (monster == null || !monster.gameObject.activeInHierarchy)
            {
                monstersInTrap.RemoveAt(i);
                lastAttackTimes.Remove(monster);
                originalSpeeds.Remove(monster);
                Debug.Log("리스트내 죽은 몬스터 제거");
                continue;
            }

            if (!lastAttackTimes.ContainsKey(monster))
            {
                lastAttackTimes[monster] = 0;
            }

            if (Time.time - lastAttackTimes[monster] >= attackCoolTime)
            {
                Attack(monster);
                lastAttackTimes[monster] = Time.time;
            }
        }
    }
}
