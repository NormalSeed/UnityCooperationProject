using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationPlaneTest : MonoBehaviour
{
    [Range(1f, 10f)]
    [SerializeField] float spdUpFactor;

    [Range(10f, 100f)]
    [SerializeField] float maxSpd;

    private void OnCollisionStay(Collision collision)
    {
        Rigidbody colliderRb = collision.gameObject.GetComponent<Rigidbody>();

        if (collision.gameObject.CompareTag("Player"))
        {
            if (colliderRb != null)
            {
                Vector3 currentVelocity = colliderRb.velocity;
                Vector3 accelaration = colliderRb.velocity.normalized * spdUpFactor;
                if (currentVelocity.magnitude > maxSpd)
                {
                    colliderRb.velocity = colliderRb.velocity.normalized * maxSpd;
                }
                else
                {
                    colliderRb.AddForce(accelaration, ForceMode.Impulse);
                }
            }
        }
    }
}
