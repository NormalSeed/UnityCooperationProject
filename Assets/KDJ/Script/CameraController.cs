using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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