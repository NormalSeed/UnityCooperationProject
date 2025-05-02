using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntLionTrap : MonoBehaviour
{
    [SerializeField] float maxPullForce;    // 최대 끌어당기는 힘
    public float escapeForce;               // 빠져나갈 때 가해주는 힘
    public float minDistance;               // 최소 거리 제한
    public float escapeThreshold;           // 일정 속도 이상일 때 탈출 가능
    public Transform trapCenter;            // 트랩 중심

    void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            Rigidbody rb = other.attachedRigidbody;
            float distance = Vector3.Distance(trapCenter.position, other.transform.position);

            if (distance > minDistance)
            {
                float forceMagnitude = maxPullForce / distance; // 거리에 반비례하는 힘 설정
                Vector3 direction = (trapCenter.position - other.transform.position).normalized;
                rb.AddForce(direction * forceMagnitude, ForceMode.Acceleration);

                // 플레이어가 빠져나가려 할 때 일정 속도 이상이면 탈출 가능
                if (rb.velocity.magnitude > escapeThreshold)
                {
                    Vector3 escapeDirection = rb.velocity.normalized;
                    rb.AddForce(escapeDirection * escapeForce, ForceMode.Acceleration);
                }
            }
        }
    }

}
