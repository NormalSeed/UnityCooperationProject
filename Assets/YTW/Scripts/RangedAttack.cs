using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RangedAttack : MonoBehaviour, IAttackable
{    
    [SerializeField] float attackRange;
    [SerializeField] float attackCoolTime;
    [Range(30,50)]
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
        GameObject soundInstance = Instantiate(soundPrefab, transform.position, Quaternion.identity);
        AudioSource audio = soundInstance.GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.Play();
            Destroy(soundInstance, audio.clip.length);
        }
    }
    private void Fire()
    {
        PooledBullet bullet = (PooledBullet)bulletpool.GetObject(bulletPos.position, bulletPos.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;
    }
    
}
