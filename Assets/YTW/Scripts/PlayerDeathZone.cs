using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StageManager.Instance.StageFailed();
        }
        if (other.CompareTag("Monster"))
        {
            OnDamaged target = other.GetComponent<OnDamaged>();
            target.TakeDamaged(99999);
        }
    }
}
