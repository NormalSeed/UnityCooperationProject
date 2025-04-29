using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerRatator : MonoBehaviour
{
    [SerializeField] float rotationSpd;

    private void FixedUpdate()
    {
        gameObject.transform.Rotate(Vector3.up * rotationSpd);
    }
}
