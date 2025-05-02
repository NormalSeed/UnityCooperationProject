using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

//이코드는
//공이나 물체가 중력을 따라 움직이도록 만든 게임에서 중력 방향을 조작 장애물을 피하거나 진행 경로를 바꾸는 코드
public class GravityController : MonoBehaviour
{
    //중력 가속도
    const float Gravity = 9.8f;
    //중력 적용상태
    public float gravityScale = 1.0f;

    void Update()
    {
        Vector3 vector = new Vector3();

        if (Application.isEditor)
        {
            //키 입력을 검출하는 벡터
            vector.x = Input.GetAxis("Horizontal");
            vector.z = Input.GetAxis("Vertical");

            //높이 방향 판정 z
            if (Input.GetKey("z"))
            {
                vector.y = 1.0f;
            }
            else
            {
                vector.y = -1.0f;
            }
        }
        else
        {
            vector.x = Input.acceleration.x;
            vector.z = Input.acceleration.y;
            vector.y = Input.acceleration.z;
        }
        Physics.gravity = Gravity * vector.normalized * gravityScale;

        }
    }

