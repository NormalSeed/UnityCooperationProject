using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : Item
{

    [SerializeField] private float speedIncrease;
    [SerializeField] private float buffTime;
    [SerializeField] private GameObject speedupEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RunItem();
        }
    }
    // 아이템 실행
    public override void RunItem()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PB playerController = player.GetComponent<PB>();
        if (playerController != null)
        {
            playerController.StartCoroutine(TemporaryAttackBuff(playerController));
            // Effect 효과 실행
            if (speedupEffect != null)
            {
                GameObject effect = Instantiate(speedupEffect, player.transform.position, Quaternion.identity);

                // 회전 영향 없이 위치만 따라가게
                EffectFollowPlayer follow = effect.AddComponent<EffectFollowPlayer>();
                follow.target = player.transform;
                Destroy(effect, buffTime);  // 버프 시간 동안 이펙트가 실행되고 그 후 이펙트 삭제
            }

            // UI실행
            BuffText ui = FindObjectOfType<BuffText>();
            if (ui != null)
            {
                // 버프 지속 시간 동안 해당하는 UI 텍스트를 화면에 표시
                ui.ShowBuff("Speed", buffTime);
            }
        }
        Destroy(gameObject);
    }

    // 일정 시간 동안 이동속도 증가 및 복구
    private IEnumerator TemporaryAttackBuff(PB player)
    {
        player.IncreaseSpeed(speedIncrease);
        yield return new WaitForSeconds(buffTime);
        player.IncreaseSpeed(-speedIncrease);
    }
}
