using UnityEngine;

public class MeleeAttack : MonoBehaviour, IAttackable
{
    [SerializeField] int damage;
    [SerializeField] float attackRange;
    [SerializeField] float attackCoolTime;
    [SerializeField] GameObject soundPrefab;

    private float lastAttackTime;

    public bool CanAttack(Transform target)
    {
        // 타겟과의 거리
        float distance = Vector3.Distance(transform.position, target.position);
        // 타겟과의 거리가 사정거리내이고, 현재시간이 마지막 공격시간(그때 현재시간) + 공격쿨타임보다 크다면 공격이 가능(true)
        return distance < attackRange && Time.time >= lastAttackTime + attackCoolTime;
    }

    public void Attack(Transform target)
    {
        // 공격을 한 후 쿨타임 계산을 위한 공격한 시간 저장
        lastAttackTime = Time.time;

        OnDamaged targetHP = target.GetComponent<OnDamaged>();
        targetHP?.TakeDamaged(damage);
        Instantiate(soundPrefab, transform.position, Quaternion.identity);
    }
}
