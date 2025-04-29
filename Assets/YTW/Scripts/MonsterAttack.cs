using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    // 가상 플레이어
    [SerializeField] int demage;
    //public void Attack(Player player)
    //{
    //    player.TakeDamage(demage);
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) 
        {
            //Player player = collision.gameObject.gameObject.GetComponent<Player>();
            //if(player != null)
            //{
            //    Attack(player);
            //}
        }
    }
}
