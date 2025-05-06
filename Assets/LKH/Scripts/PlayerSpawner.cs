using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    private void Start()
    {
        GameObject player = Instantiate(playerPrefab);
        player.transform.position = transform.position;
    }
}
