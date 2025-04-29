using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    // 가상 플레이어
    [SerializeField] int damage;
    [SerializeField] float attackRange;
    [SerializeField] float attackCoolTime;
    [SerializeField] MonsterController monster;

    private float lastAttackTime;

    
    public bool CanAttack(Transform target)
    {
        float distance = Vector3.Distance(transform.position, target.position);
        // 몬스터와 플레이어 거리가 사정거리내이고, 현재시간이 마지막 공격시간(그때 현재시간) + 공격쿨타임보다 크다면 공격이 가능(true)
        return distance < attackRange && Time.time >= lastAttackTime + attackCoolTime;
    }

    public void Attack(Transform target)
    {
        // 공격을 한 후 쿨타임 계산을 위한 공격한 시간 저장
        lastAttackTime = Time.time; 
        
        OnDamaged targetHP = target.GetComponent<OnDamaged>();
        if (targetHP != null)
        {
            targetHP.TakeDamaged(damage);
            
        }
    }
}
