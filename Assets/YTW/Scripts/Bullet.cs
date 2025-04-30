using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] PooledBullet pooledBullet;
    [SerializeField] int damage;

    private void OnTriggerEnter(Collider other)
    {
        OnDamaged targetHP = other.GetComponent<OnDamaged>();
        if (targetHP != null)
        {
            targetHP.TakeDamaged(damage);
            pooledBullet.ReturnPool();
        }
    }
}
