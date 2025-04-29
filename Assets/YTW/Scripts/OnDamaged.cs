using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class OnDamaged : MonoBehaviour
{
    [SerializeField] int hp;

    public void TakeDamaged(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
