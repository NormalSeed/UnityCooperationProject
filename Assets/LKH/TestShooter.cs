using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShooter : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] BulletPool bulletPool;

    [Range(10, 30)]
    [SerializeField] float bulletSpeed;

    public void Fire()
    {
        PooledBullet bullet = (PooledBullet)bulletPool.GetObject(transform.position, transform.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }
}
