using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerRatator : MonoBehaviour
{
    [SerializeField] float rotationSpd;
    private Rigidbody rigidBody;
    private void FixedUpdate()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.angularVelocity = Vector3.up * rotationSpd;
    }
}
