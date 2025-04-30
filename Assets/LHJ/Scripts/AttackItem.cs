using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackItem : Item
{
    [SerializeField] private int attackIncrease;
    [SerializeField] private float buffTime;
    [SerializeField] private GameObject powerupEffect;


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
            // Effect 효과 실행
            if (powerupEffect != null)
            {
                GameObject effect = Instantiate(powerupEffect, player.transform.position, Quaternion.identity);
                effect.transform.SetParent(player.transform);
                Destroy(effect, 5f);  // 5초동안 이펙트가 실행되고 그 후 이펙트 삭제
            }
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
