using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������Ʈ Ŭ������ �̱������� ����� �߻� Ŭ�����Դϴ�.
// ������Ʈ Ŭ���� ������
// public class CompoClass : Singleton<CompoClass> { }
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    // CompoClass.Instance �� ���� �ش� ������Ʈ�� �ν��Ͻ��� ���� �����ϵ��� �ϴ� ������Ƽ�Դϴ�.
    // ���� �ν��Ͻ��� ���µ� �ش� ������Ƽ�� ����Ѵٸ�
    // �� ���� ��� �𸣰� �̱����� ���ӿ� �Ҵ����� ���� ���
    // ������Ʈ�� �̸����� ���ο� ���ӿ�����Ʈ�� ���� ��,
    // �ش� ���ӿ�����Ʈ�� ������Ʈ�� ���Դϴ�.
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameManager = new GameObject(typeof(T).Name);
                instance = gameManager.AddComponent<T>();
                DontDestroyOnLoad(gameManager);
            }
            return instance;
        }
    }

    // ������Ʈ�� Awake() �ܰ迡�� �Ʒ��� �Լ��� �����ϵ��� �մϴ�.
    // ���� instance�� �ν��Ͻ� �Ҵ� �� ���ӿ�����Ʈ�� �������� �ʵ��� �����Ǹ�
    // ���� �߰��� ���ӿ�����Ʈ�� �ش� ������Ʈ�� �پ��ִٸ�, ������ŵ�ϴ�.
    protected void SetInstance()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
