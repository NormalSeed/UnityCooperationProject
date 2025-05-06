using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntLionTrap : MonoBehaviour
{
    [SerializeField] float maxPullForce;
    public float escapeForce;           
    public float minDistance;           
    public float escapeThreshold;       
    public Transform trapCenter;        

    void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            Rigidbody rb = other.attachedRigidbody;
            float distance = Vector3.Distance(trapCenter.position, other.transform.position);

            if (distance > minDistance)
            {
                float forceMagnitude = maxPullForce / distance;
                Vector3 direction = (trapCenter.position - other.transform.position).normalized;
                rb.AddForce(direction * forceMagnitude, ForceMode.Acceleration);

                if (rb.velocity.magnitude > escapeThreshold)
                {
                    Vector3 escapeDirection = rb.velocity.normalized;
                    rb.AddForce(escapeDirection * escapeForce, ForceMode.Acceleration);
                }
            }
        }
    }

}
