using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingWall : MonoBehaviour
{
    [SerializeField] float bounceSpd;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 incomingVec = collision.relativeVelocity.normalized;
            Vector3 colliderNormalVec = collision.contacts[0].normal;
            Vector3 reflectVec = Vector3.Reflect(incomingVec, colliderNormalVec);
            Rigidbody colliderRb = collision.gameObject.GetComponent<Rigidbody>();
            colliderRb.AddForce(reflectVec * bounceSpd, ForceMode.Impulse);
        }
    }
}
