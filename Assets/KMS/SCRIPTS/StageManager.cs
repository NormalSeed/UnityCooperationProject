using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 스테이지 별로 필요한 데이터를 관리하는 싱글톤 매니저 컴포넌트입니다.
// 게임매니저 게임오브젝트에 포함되어 있습니다.
public class StageManager : Singleton<StageManager>
{
    // 스테이지의 이름입니다.
    [SerializeField] string stageName;
    public string StageName { get { return stageName; } }
    // 스테이지에서 필요한 점수입니다. 해당 점수를 모두 충족해야 레벨 클리어가 가능합니다.
    [SerializeField] int maxStageScore;
    [SerializeField] int stageScore;
    // 스테이지 별 이름들과 필요점수를 리스트로 관리합니다.
    // 리스트의 값이 InitStageValues를 통해 변수에 할당됩니다.
    // 인덱스가 0이면 타이틀, 1이면 레벨1, 2면 레벨2에 필요한 값이 됩니다.
    [SerializeField] List<string> stageNames;
    [SerializeField] List<int> maxStageScores;
    public int seconds = 0;
    private float oneSec = 0;
    public int MaxStageScore { get { return maxStageScore; } }

    // 점수 충족 여부를 확인합니다.
    public bool IsScoreFull => stageScore == MaxStageScore;

    // 스테이지의 값(점수 등)이 바뀔때 호출됩니다.
    // infoUI 측에서 함수가 할당됩니다.
    [SerializeField] public UnityEvent onStageValueChanged;
    [SerializeField] public UnityEvent onSeconds;

    // 추후 추가될 가능성이 있는 이벤트들
    [SerializeField] public UnityEvent onStageCleared;
    [SerializeField] public UnityEvent onStageFailed;

    public int StageScore
    {
        get
        {
            return stageScore;
        }
        set
        {
            int next = value;
            if (next < 0)
            {
                next = 0;
            }
            else if (next > maxStageScore)
            {
                next = maxStageScore;
            }
            stageScore = next;
            onStageValueChanged?.Invoke();
        }
    }
    private void Awake()
    {
        SetInstance();
    }
    private void Start()
    {
        GameManager.Instance.onSceneLoaded.AddListener(SetStageValues);
        SetStageValues(GameManager.Instance.CurrentSceneIndex);
    }

    private void Update()
    {
        // 시간 측정 코드
        if (!UIManager.Instance.IsScreenUIOpened)
        {
            oneSec += Time.deltaTime;
        }
        if (oneSec >= 1)
        {
            oneSec = 0;
            seconds++;
            onSeconds?.Invoke();
        }
    }

    // onSceneLoaded 이벤트에 할당되어 씬이 바뀔때 씬 인덱스별로 값을 지정합니다.
    // 즉 씬이 로드되면 스테이지에 필요한 값을 초기화 하고,
    // onStageValueChanged를 호출하여 그 값을 InfoUI에 반영합니다.
    // 인덱스가 0이면 타이틀, 1이면 레벨1, 2면 레벨2 이런식입니다. (씬 인덱스와 대응합니다)
    public void SetStageValues(int SceneIndex)
    {
        oneSec = 0;
        seconds = 0;
        stageScore = 0;
        stageName = stageNames[SceneIndex];
        maxStageScore = maxStageScores[SceneIndex];
        onStageValueChanged?.Invoke();
    }

    // 스테이지 실패 함수입니다.
    // 자동으로 목숨을 1 차감하며, 0일 경우 게임 오버 함수를 불러옵니다.
    public void StageFailed()
    {
        GameManager.Instance.LifePoint--;
        if (GameManager.Instance.LifePoint == 0)
        {
            GameManager.Instance.GameOver();
        }
        else
        {
            onStageFailed?.Invoke();
            UIManager.Instance.OpenStageFailedUI();
        }
    }
    // 스테이지를 클리어시킵니다.
    public void StageClear()
    {
        onStageCleared?.Invoke();
        UIManager.Instance.OpenStageClearUI();
    }
}
