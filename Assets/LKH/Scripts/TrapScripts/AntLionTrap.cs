using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntLionTrap : MonoBehaviour
{
    [SerializeField] Transform trapCenter;
    [SerializeField] float pullPower;

    private void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody != null) // 닿은 물체에 리지드바디가 있다면
        {
            Vector3 direction = (trapCenter.position - other.transform.position).normalized; // 방향을 trapCenter 방향으로
            other.attachedRigidbody.AddForce(direction * pullPower, ForceMode.Impulse);
        }
    }
}
