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
            // 체력 회복 조건
            bool healed = playerController.Heal(heal);
            if (healed)
            {   
                // 체력 회복이 성공했으면 이펙트 재생
                if (healEffect != null)
                {
                    GameObject effect = Instantiate(healEffect, player.transform.position, Quaternion.identity);
                    effect.transform.SetParent(player.transform);
                    Destroy(effect, 3f);    // 3초동안 이펙트 생성되고 이후 삭제
                }
                Destroy(gameObject);
            }
            else
            {
                // 체력이 가득 차 있으면 실행되지않는다.
            }
        }  
    }
}
