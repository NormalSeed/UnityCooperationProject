using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Testplayercontroll : MonoBehaviour
{
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private float playerSpeed;
    [SerializeField] private int attack;
    [SerializeField] private int score;
    [SerializeField] private int maxHp;
    [SerializeField] private int currentHp;

    private Vector3 inputVec;
    void Update()
    {
        PlayerInput();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void PlayerInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        inputVec = new Vector3(x, 0, z).normalized;
    }
    private void Move()
    {
        rigid.velocity = inputVec * playerSpeed;
    }
    public void IncreaseAttack(int amount)
    {
        attack += amount;
        Debug.Log("공격력" + attack);
    }
    public void IncreaseSpeed(float amount)
    {
        playerSpeed += amount;
        Debug.Log("스피드" + playerSpeed);
    }
    public void IncreaseScore(int amount)
    {
        score += amount;
        Debug.Log("점수" + score);
    }

    public bool Heal(int amount)
    {
        if(currentHp >= maxHp)
        {
            Debug.Log("체력이 가득찼습니다.");
            return false;
        }
        currentHp += amount;
        if (currentHp > maxHp)
            currentHp = maxHp;
        Debug.Log($"현재 체력: {currentHp}/{maxHp}");
        return true;
    }
}
