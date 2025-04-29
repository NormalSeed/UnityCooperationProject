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
    // 아이템 효과 실행
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
    // 일정시간 동안 공격력 증가 및 복구
    private IEnumerator TemporaryAttackBuff(Testplayercontroll player)
    {
        player.IncreaseAttack(attackIncrease);      // 공격력 증가
        yield return new WaitForSeconds(buffTime);  // 버프 지속 시간
        player.IncreaseAttack(-attackIncrease);     // 원래 공격력을 복구
    }
}
