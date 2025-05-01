using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //Ball이 어떤 벡터 방향으로 움직이다가 벽 또는 플레이어에 닿았을때 방향전환 
    public float ballSpeed; // 공이 움직이는 속도 (인스펙터에서 설정)
    public Vector3 ballMovementVector;// 공이 이동할 방향 벡터
    Rigidbody rb;// Rigidbody 컴포넌트 참조

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }
    //게임오브젝트.transform.up이 월드 기준으로 평면에서 수직으로 올라가는 벡터
    void Update()
    {
        transform.Translate(ballMovementVector.normalized * ballSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //충돌한 오브젝트가 wall 또는 Player인 경우 방향을 반사
        GameObject go = collision.gameObject;
        if (go.CompareTag("wall") || go.CompareTag("Player"))//부딫힌 면에 대한 방향 전환
        {
            //충돌한 면의 위 방향 벡터를 기준으로 어느 방향에 닿았는지 판단
            if (go.transform.up.x < -0.01f || go.transform.up.x > 0.01f)
                ballMovementVector.x *= -1;
            else if (go.transform.up.y < -0.01f || go.transform.up.y > 0.01f)
                ballMovementVector.y *= -1;
            else if (go.transform.up.z < -0.01f || go.transform.up.z > 0.01f)
                ballMovementVector.z*= -1;
        }
    }
}
