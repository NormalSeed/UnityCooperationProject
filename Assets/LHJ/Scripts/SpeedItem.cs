using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : Item
{

    [SerializeField] private float speedIncrease;
    [SerializeField] private float buffTime;
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
        Testplayercontroll playerController = player.GetComponent<Testplayercontroll>();
        if (playerController != null)
        {
            playerController.StartCoroutine(TemporaryAttackBuff(playerController));
        }
        Destroy(gameObject);
    }

    // 일정 시간 동안 이동속도 증가 및 복구
    private IEnumerator TemporaryAttackBuff(Testplayercontroll player)
    {
        player.IncreaseSpeed(speedIncrease);
        yield return new WaitForSeconds(buffTime);
        player.IncreaseSpeed(-speedIncrease);
    }
}
