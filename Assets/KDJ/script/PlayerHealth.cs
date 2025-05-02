using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이코드는 플레이어의 최대 체력을 설정,몬스터나 장애물과 충돌 시 데미지를 입히고 싶은 게임에서 사용
//공을 굴려 몬스터를 피해야 하는 게임에서, 몬스터에 닿으면 체력 감소
public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 10;
    private int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    private void OnCollisionEnter(Collision collision)//플레이어가 다른 물체와 충돌했을 때 실행되는 함수
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            currentHP--;
            Debug.Log("체력: " + currentHP);
            if (currentHP <= 0)
            {
                Debug.Log("Game Over");
                gameObject.SetActive(false); // 혹은 씬 리셋
            }
        }
    }
}
