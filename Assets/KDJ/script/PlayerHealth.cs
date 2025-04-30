using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 10;
    private int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            currentHP--;
            Debug.Log("체력: " + currentHP);
            if (currentHP <= 0)
            {
                Debug.Log("Game Over");
                gameObject.SetActive(false); // 혹은 씬 리셋
            }
        }
    }
}
