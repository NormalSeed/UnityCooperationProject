using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] PooledBullet pooledBullet;
    [SerializeField] int damage;
    [SerializeField] GameObject bulletEffectPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hitObject = collision.gameObject;

        OnDamaged targetHP = hitObject.GetComponent<OnDamaged>();
        if (targetHP != null && targetHP.gameObject.CompareTag("Player"))
        {
            targetHP.TakeDamaged(damage);

        }
        if (hitObject.layer == LayerMask.NameToLayer("Wall") || hitObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            GameObject effect = Instantiate(bulletEffectPrefab, collision.transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
        pooledBullet.ReturnPool();
    }
}
