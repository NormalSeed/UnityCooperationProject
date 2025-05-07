using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RangedAttack : MonoBehaviour, IAttackable
{    
    [SerializeField] float attackRange;
    [SerializeField] float attackCoolTime;
    [Range(10,30)]
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform bulletPos;
    [SerializeField] BulletPool bulletpool;
    [SerializeField] GameObject soundPrefab;

    private float lastAttackTime;

    public bool CanAttack(Transform target)
    {
        float distance = Vector3.Distance(transform.position, target.position);
        return distance < attackRange && Time.time >= lastAttackTime + attackCoolTime;
    }

    public void Attack(Transform target)
    {
        lastAttackTime = Time.time;

        Fire();
        Instantiate(soundPrefab, bulletPos.position, Quaternion.identity);
        Destroy(soundPrefab, 0.1f);
    }
    private void Fire()
    {
        PooledBullet bullet = (PooledBullet)bulletpool.GetObject(bulletPos.position, bulletPos.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;
    }
    
}
