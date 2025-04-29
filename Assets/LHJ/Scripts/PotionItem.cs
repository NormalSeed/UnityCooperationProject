using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItem : Item
{
    [SerializeField] private int heal;
    [SerializeField] private GameObject healEffect;
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
            bool healed = playerController.Heal(heal);
            if (healed)
            {
                if (healEffect != null)
                {
                    GameObject effect = Instantiate(healEffect, player.transform.position, Quaternion.identity);
                    Destroy(effect, 3f);
                }
                Destroy(gameObject);
            }
            else
            {

            }
        }  
    }
}
