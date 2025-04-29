using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackTrap : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float attackCoolTime = 1.0f;
    [SerializeField] float speedDebuffPercent = 0.3f; 

    private bool attackRange = false;
    private float lastAttackTime = 0f;
    private MonsterController targetMonster;
    private float originalSpeed;

    private void Update()
    {
       if (attackRange && targetMonster != null)
        {
            if(Time.time >= attackCoolTime + lastAttackTime)
            {
                Attack(targetMonster.transform);
                lastAttackTime = Time.time;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Debug.Log("트랩 범위에 몬스터가 들어옴");
            targetMonster = other.GetComponent<MonsterController>();
            if (targetMonster != null)
            {
                attackRange = true;
                NavMeshAgent agent = targetMonster.GetComponent<NavMeshAgent>();
                if (agent != null)
                {
                    originalSpeed = agent.speed;
                    agent.speed = originalSpeed * (1f - speedDebuffPercent); 
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Debug.Log("트랩 범위에 몬스터가 나감");
            if (targetMonster != null)
            {
                NavMeshAgent agent = targetMonster.GetComponent<NavMeshAgent>();
                if (agent != null)
                {
                    agent.speed = originalSpeed;
                }
            }
            attackRange = false;
            targetMonster = null;
        }
    }

    public void Attack(Transform target)
    {
        OnDamaged targetHP = target.GetComponent<OnDamaged>();
        targetHP?.TakeDamaged(damage);
        Debug.Log($"몬스터 공격 남은 HP : {targetHP.CURHP}");
    }

}
