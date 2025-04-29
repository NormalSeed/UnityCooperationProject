using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : Item
{
    [SerializeField] private int scoreIncrease;
    [SerializeField] private GameObject scoreEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RunItem();
        }
    }
    public override void RunItem()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Testplayercontroll playerController = player.GetComponent<Testplayercontroll>();
        if (playerController != null)
        {
            playerController.IncreaseScore(scoreIncrease);
            if (scoreEffect != null)
            {
                GameObject effect = Instantiate(scoreEffect, player.transform.position, Quaternion.identity);
                Destroy(effect, 3f); 
            }
        }
        Destroy(gameObject);
    }
}
