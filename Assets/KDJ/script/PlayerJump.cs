using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float JumpPower;
    Rigidbody rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();

    }
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            rigid.AddForce(new Vector3(0, JumpPower, 0), ForceMode.Impulse);
    }
    void FixwdUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
    }

}
