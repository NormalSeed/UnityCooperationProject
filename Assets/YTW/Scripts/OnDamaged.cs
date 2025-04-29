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

    private HealthBar healthBar;
    

    private void OnEnable()
    {
        CurHP = MaxHP;
        if (healthBar == null)
        {
            healthBar = GetComponentInChildren<HealthBar>();
        }
        healthBar?.SetHP(amount);
    }

    public void TakeDamaged(int damage)
    {
        CurHP -= damage;
        healthBar?.SetHP((float)CurHP / MaxHP);
        
        Debug.Log($"{damage}데미지 받아서 현재 채력 {CurHP}");
        if (CurHP <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
