using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image fillImage;
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
            transform.forward = Camera.main.transform.forward;
        }
    }
}
