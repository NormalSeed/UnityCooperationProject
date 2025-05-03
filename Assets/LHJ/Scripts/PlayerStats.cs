using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private float playerSpeed;
    [SerializeField] private int attack;
    [SerializeField] private int score;
    [SerializeField] private int maxHp;
    [SerializeField] private int currentHp;

    public void IncreaseAttack(int amount)
    {
        attack += amount;
    }
    public void IncreaseSpeed(float amount)
    {
        playerSpeed += amount;
    }
    public void IncreaseScore(int amount)
    {
        StageManager.Instance.StageScore++;
    }

    public bool Heal(int amount)
    {
        if(currentHp >= maxHp)
        {
            return false;
        }
        currentHp += amount;
        if (currentHp > maxHp)
            currentHp = maxHp;
        return true;
    }
}
