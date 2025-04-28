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
        // �ʱ�ȭ �۾�
        gameObject.SetActive(false);
    }

    public GameObject GetGameObject()
    {
        // BulletPool���� ����� GameObject ��ȯ
        return gameObject;
    }

    private void OnEnable()
    {
        timer = returnTime;
    }

    private void Update()
    {
       // returnTime ��ŭ�� �ð��� �帣�� Ǯ�� ��ȯ
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
