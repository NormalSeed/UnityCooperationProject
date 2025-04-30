using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 스테이지 별로 필요한 데이터를 관리하는 싱글톤 매니저 컴포넌트입니다.
// 게임매니저 게임오브젝트에 포함되어 있습니다.
public class StageManager : Singleton<StageManager>
{
    [SerializeField] string stageName;
    // 스테이지에서 필요한 점수입니다. 해당 점수를 모두 충족해야 레벨 클리어가 가능합니다.
    [SerializeField] int maxStageScore;
    [SerializeField] int stageScore;
    // 스테이지 별 이름들과 필요점수를 리스트로 관리합니다.
    // 리스트의 값이 InitStageValues를 통해 변수에 할당됩니다.
    // 인덱스가 0이면 타이틀, 1이면 레벨1, 2면 레벨2에 필요한 값이 됩니다.
    [SerializeField] List<string> stageNames;
    [SerializeField] List<int> maxStageScores;
    public string StageName {  get { return stageName; } }
    public int MaxStageScore { get { return maxStageScore; } }

    [SerializeField] public UnityEvent onValueChanged;

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
        }
    }
    private void Awake()
    {
        SetInstance();
    }
    private void Start()
    {
        GameManager.Instance.onSceneLoaded.AddListener(InitStageValues);
    }

    // onSceneLoaded 이벤트에 할당되어 씬이 바뀔때 씬 인덱스별로 값을 지정합니다.
    // 인덱스가 0이면 타이틀, 1이면 레벨1, 2면 레벨2 이런식입니다.
    public void InitStageValues(int SceneIndex)
    {
        stageScore = 0;
        stageName = stageNames[SceneIndex];
        maxStageScore = maxStageScores[SceneIndex];
        onValueChanged?.Invoke();
    }
}
