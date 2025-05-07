using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarPos2 : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float x;
    [SerializeField] float y;
    [SerializeField] float z;
    [SerializeField] float xRotateDegree;
    
    void Update()
    {
        gameObject.transform.position = player.transform.position + new Vector3(x, y, z);
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(xRotateDegree, 0, 0);
    }
}
