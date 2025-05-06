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
    // 아이템 효과 실행
    public override void RunItem()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PB playerController = player.GetComponent<PB>();
        if (playerController != null)
        {
            // 플레이어 점수 증가
            playerController.IncreaseScore(scoreIncrease);

            // 이펙트 재생
            if (scoreEffect != null)
            {
                GameObject effect = Instantiate(scoreEffect, player.transform.position, Quaternion.identity);

                // 회전 영향 없이 위치만 따라가게
                EffectFollowPlayer follow = effect.AddComponent<EffectFollowPlayer>();
                follow.target = player.transform;
                Destroy(effect, 3f);       // 3초동안 실행되고 그 후 이펙트 삭제
            }
            BuffText ui = FindObjectOfType<BuffText>();
            if (ui != null)
            {
                ui.ShowBuff("Score", 3f);   // 버프시간이 없기때문에 임의로 3초로 설정
            }
        }
        Destroy(gameObject);
    }
}
