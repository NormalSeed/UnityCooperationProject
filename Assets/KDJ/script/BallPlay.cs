using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPlay : MonoBehaviour
{
    // GM 클래스의 싱글톤 인스턴스를 저장할 정적 변수
    private static GM instance;

    // Ball 오브젝트를 저장할 변수
    GameObject m_ball;

    // 인스펙터에서 할당 가능한 오브젝트 (현재는 사용되지 않음)
    public GameObject cube;

    // 공의 초기 속도 
    public float initBallSpeed;

    // 공의 초기 방향 벡터 (인스펙터에서 설정)
    public Vector3 m_initBallVector;

    // 싱글톤 인스턴스에 접근할 수 있도록 하는 프로퍼티
    public static GM Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        // 인스턴스가 이미 존재한다면 기존 인스턴스를 삭제하고 리턴
        if (instance)
        {
            Destroy(instance);
            return;
        }

        // 이 오브젝트를 싱글톤 인스턴스로 설정
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        // "Ball"이라는 이름의 게임 오브젝트를 찾아서 m_ball에 저장
        m_ball = GameObject.Find("Ball");
    }
    void Start()
    {
        // m_ball의 BallScript 컴포넌트에 초기 속도 및 방향값을 설정
        m_ball.GetComponent<BallScript>().ballSpeed = initBallSpeed;
        m_ball.GetComponent<BallScript>().ballMovementVector = m_initBallVector;
    }

    // 매 프레임마다 호출되지만 현재는 내용 없음 (추후 확장 가능)
    void Update()
    {
    }//수정
}
