using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackItem : Item
{
    [SerializeField] private int attackIncrease;
    [SerializeField] private float buffTime;


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
            playerController.StartCoroutine(TemporaryAttackBuff(playerController));
        }

        Destroy(gameObject); 
    }
    private IEnumerator TemporaryAttackBuff(Testplayercontroll player)
    {
        player.IncreaseAttack(attackIncrease);
        yield return new WaitForSeconds(buffTime);
        player.IncreaseAttack(-attackIncrease);
    }
}
