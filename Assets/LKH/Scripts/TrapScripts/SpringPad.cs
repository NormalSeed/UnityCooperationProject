using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPad : MonoBehaviour
{
    [SerializeField] float tiltAngle = 60f;
    [SerializeField] float tiltSpeed = 720f;
    [SerializeField] Transform pivot;
    [SerializeField] float padPower; // 발판이 플레이어를 날리는 힘의 크기
    private float resetDelay = 0.5f;
    private float returnSpeed = 2f;

    private Quaternion originalRotation;
    private Quaternion targetRotation;
    private bool isTilting = false;

    private void Start()
    {
        if (pivot == null)
        {
            return;
        }

        originalRotation = pivot.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isTilting)
        {
            StartCoroutine(TiltAndReturn());
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && isTilting)
        {
            Rigidbody rb = collision.rigidbody;
            if (rb != null)
            {
                Vector3 forceDirection = pivot.up; // 발판의 법선 벡터 방향
                rb.AddForce(forceDirection * padPower, ForceMode.Impulse);
            }
        }
    }

    private IEnumerator TiltAndReturn()
    {
        yield return new WaitForSeconds(0.5f);
        isTilting = true;

        targetRotation = Quaternion.Euler(pivot.rotation.eulerAngles.x, pivot.rotation.eulerAngles.y, tiltAngle);

        // 빠르게 기울이기
        while (Quaternion.Angle(pivot.rotation, targetRotation) > 0.5f)
        {
            pivot.rotation = Quaternion.RotateTowards(pivot.rotation, targetRotation, tiltSpeed * Time.deltaTime);
            yield return null;
        }

        pivot.rotation = targetRotation;

        yield return new WaitForSeconds(resetDelay);

        // 부드럽게 복귀
        while (Quaternion.Angle(pivot.rotation, originalRotation) > 0.1f)
        {
            pivot.rotation = Quaternion.Lerp(pivot.rotation, originalRotation, Time.deltaTime * returnSpeed);
            yield return null;
        }

        pivot.rotation = originalRotation;
        isTilting = false;
    }
}
