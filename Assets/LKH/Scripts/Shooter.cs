using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Shooter : MonoBehaviour
{
    [SerializeField] BulletPool bulletPool;

    [Range(10, 30)]
    [SerializeField] float bulletSpeed;

    private Vector3 bulletSpawnPos;

    public void Fire()
    {
        GetDirection();
        PooledBullet bullet = (PooledBullet)bulletPool.GetObject(bulletSpawnPos, transform.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;
    }

    private void GetDirection()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(x, 0, z);
        if (direction.sqrMagnitude > 0 )
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
        }
        bulletSpawnPos = transform.position + transform.forward * 1.5f;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }
}
