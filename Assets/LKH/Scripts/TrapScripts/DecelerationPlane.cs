using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecelerationPlane : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] float slowDownFactor;

    private void OnCollisionStay(Collision collision)
    {
        Rigidbody colliderRb = collision.gameObject.GetComponent<Rigidbody>();

        if (collision.gameObject.CompareTag("Player"))
        {
            if (colliderRb != null)
            {
                Vector3 currentVelocity = colliderRb.velocity;
                colliderRb.velocity = currentVelocity * slowDownFactor;
            }
        }
    }
}
