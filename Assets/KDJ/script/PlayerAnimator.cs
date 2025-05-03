using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이 코드는 플레이어 캐릭터의 애니메이션과 움직임을 함께 제어하는 스크립트입니다.
//키 입력에 따라 공이 아닌 몬스터나 사람일경우 걷고, 달리고, 점프하고, 방향 전환기능
public class PlayerAnimator : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;
    bool wDown;
    bool jDown;
    bool Jumping;
    

    Vector3 moveVec;

    Rigidbody rigid;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>(); //컴포넌트의 자식에 위치한 Animator를 가져온다
    }
    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        
    }
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        jDown = Input.GetButtonDown("Jump");
    }

    void Move()
    {

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;//상하좌우 이동 + 대각선 방향값1로 고정한다
        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        anim.SetBool("Running", moveVec != Vector3.zero);//기본적으로 Player 달리기설정
        anim.SetBool("Walking", wDown);
    }
    void Turn()
    {
        transform.LookAt(transform.position + moveVec);//현재위치에서 움직여야할 방향으로 움직인다
    }
    void Jump()
    {
        if (jDown && !Jumping)
        {
            rigid.AddForce(Vector3.up * 15, ForceMode.Impulse);
            anim.SetBool("Jumping", true);
            anim.SetTrigger("doJump");
            Jumping = true;
        }
    }
    void OnCollisionEnter(Collision collision)//이벤트 함후로 착지 구현
    {
        if (collision.gameObject.tag == "Floor")
        {
            anim.SetBool("Jumping", false);
            Jumping = false;
        }
    }

}
//수정한것
