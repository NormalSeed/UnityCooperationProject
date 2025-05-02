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
        PlayerStats playerController = player.GetComponent<PlayerStats>();
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

                    // 회전 영향 없이 위치만 따라가게
                    EffectFollowPlayer follow = effect.AddComponent<EffectFollowPlayer>();
                    follow.target = player.transform;
                    Destroy(effect, 3f);    // 3초동안 이펙트 생성되고 이후 삭제
                }
                BuffText ui = FindObjectOfType<BuffText>();
                if (ui != null)
                {
                    ui.ShowBuff("Heal", 3f);   // 버프시간이 없기때문에 임의로 3초로 설정
                }
                Destroy(gameObject);
            }
            else
            {
                // 체력이 가득 차 있으면 Item기능을 실행하지않고 UI실행
                BuffText ui = FindObjectOfType<BuffText>();
                if (ui != null)
                {
                    ui.ShowHealFail();  // TestBuffText.cs에서 임의로 2초로 설정
                }
            }
        }  
    }
}
