using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이 코드는 카메라가 특정 게임 오브젝트를 따라가도록 할 때 사용하는 카메라 추적 스크립트
//카메라가 플레이어 공을 추적하는 코드
public class CameraController : MonoBehaviour
{
    public GameObject targetCamera;
    public GameObject targetObject;

    private Vector3 offset;

    void Start()
    {
        offset = targetCamera.transform.position - targetObject.transform.position;
    }
    void Update()
    {
        targetCamera.transform.position = offset + targetObject.transform.position;

    }
}
