using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이 코드는 중력을 직접 조종해서 플레이어가 떨어지는 방향을 바꿉니다
//중력으로 구슬이나 물체를 이동시키는 코드입니다
public class player : MonoBehaviour
{
    Rigidbody rigid;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
    }
}
