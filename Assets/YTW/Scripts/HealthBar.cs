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

}
