using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image fillImage;
    // 적용할 대상 y위치 (머리위)
    [SerializeField] Vector3 offset;
    private Transform target;

    public void SetHP(float amount)
    {
        fillImage.fillAmount = amount;
    }
        
    public void SetTarget(Transform followTarget)
    {
        target = followTarget;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.forward = Camera.main.transform.forward;
        }
    }
}
