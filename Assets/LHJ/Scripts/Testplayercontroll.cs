using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Testplayercontroll : MonoBehaviour
{
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private float playerSpeed;
    [SerializeField] private int attack;

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
}
