using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEditor.UIElements;
using UnityEngine;

public class OnDamaged : MonoBehaviour
{
    [SerializeField] int MaxHP;
    [SerializeField] int CurHP;
    [SerializeField] float amount = 1;

    [SerializeField] GameObject healthBarPrefab;
    private HealthBar healthBarInstance;
    

    private void OnEnable()
    {
        CurHP = MaxHP;

        if(healthBarInstance == null)
        {
            GameObject hpbar = Instantiate(healthBarPrefab);
            healthBarInstance = hpbar.GetComponent<HealthBar>();
            healthBarInstance.SetTarget(transform);
        }
        healthBarInstance?.SetHP(amount);
    }

    public void TakeDamaged(int damage)
    {
        CurHP -= damage;
        healthBarInstance?.SetHP((float) CurHP / MaxHP);
        
        Debug.Log($"{damage}데미지 받아서 현재 채력 {CurHP}");
        if (CurHP <= 0)
        {
            healthBarInstance.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
