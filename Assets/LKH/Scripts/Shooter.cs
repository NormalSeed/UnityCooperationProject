using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Shooter : MonoBehaviour
{
    [SerializeField] BulletPool bulletPool;

    [Range(10, 30)]
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform player;

    private Vector3 bulletSpawnPos;
    private Vector3 fixedForward = Vector3.forward;

    public void Fire()
    {
        PooledBullet bullet = (PooledBullet)bulletPool.GetObject(bulletSpawnPos, transform.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = fixedForward * bulletSpeed;
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
            fixedForward = direction.normalized;
        }
        bulletSpawnPos = player.position + fixedForward * 1.5f;
    }

    public void Update()
    {
        GetDirection();
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }
}
