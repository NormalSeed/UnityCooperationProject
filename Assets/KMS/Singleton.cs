using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 컴포넌트 클래스를 싱글톤으로 만드는 추상 클래스입니다.
// 컴포넌트 클래스 선언은
// public class CompoClass : Singleton<CompoClass> { }
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    // CompoClass.Instance 를 통해 해당 컴포넌트의 인스턴스로 접근 가능하도록 하는 프로퍼티입니다.
    // 만약 인스턴스가 없는데 해당 프로퍼티를 사용한다면
    // 즉 예를 들어 모르고 싱글톤을 게임에 할당하지 않은 경우
    // 컴포넌트의 이름으로 새로운 게임오브젝트를 만든 후,
    // 해당 게임오브젝트에 컴포넌트를 붙입니다.
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

    // 컴포넌트는 Awake() 단계에서 아래의 함수를 실행하도록 합니다.
    // 최초 instance에 인스턴스 할당 시 게임오브젝트가 삭제되지 않도록 고정되며
    // 이후 추가된 게임오브젝트에 해당 컴포넌트가 붙어있다면, 삭제시킵니다.
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
