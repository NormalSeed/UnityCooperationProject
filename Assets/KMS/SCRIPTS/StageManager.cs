using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageManager : Singleton<StageManager>
{
    [SerializeField] string stageName;
    [SerializeField] int maxStageScore;
    [SerializeField] int stageScore;
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
    public void InitStageValues(int SceneIndex)
    {
        stageScore = 0;
        stageName = stageNames[SceneIndex];
        maxStageScore = maxStageScores[SceneIndex];
        onValueChanged?.Invoke();
    }
}
