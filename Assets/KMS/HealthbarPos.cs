using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarPos : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void Update()
    {
        gameObject.transform.position = player.transform.position + new Vector3(0, 2, 0);
    }
}
