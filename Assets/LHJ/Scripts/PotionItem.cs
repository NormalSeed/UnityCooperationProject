using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItem : Item
{
    [SerializeField] private int heal;
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
            playerController.Heal(heal);
        }
        Destroy(gameObject);
    }
}
