using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledBullet : MonoBehaviour, IPooledObject
{
    public BulletPool poolToReturn;
    [SerializeField] float returnTime;
    private float timer;

    public void ResetObject()
    {
        // 초기화 작업
        gameObject.SetActive(false);
    }

    public GameObject GetGameObject()
    {
        // BulletPool에서 사용할 GameObject 반환
        return gameObject;
    }

    private void OnEnable()
    {
        timer = returnTime;
    }

    private void Update()
    {
       // returnTime 만큼의 시간이 흐르면 풀에 반환
        timer -= Time.deltaTime;
       if (timer <= 0)
       {
           ReturnPool();
       }
    }
    public void ReturnPool()
    {
        if (poolToReturn == null)
        {
            Destroy(gameObject);
        }
        else
        {
            poolToReturn.ReturnObject(this);
        }
    }
}
