using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private float playerSpeed;
    [SerializeField] private int attack;

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
}
