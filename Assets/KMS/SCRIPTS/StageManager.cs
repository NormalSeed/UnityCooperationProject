using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [SerializeField] string stageName;
    [SerializeField] GameObject infoUI;
    [SerializeField] int maxStageScore;
    [SerializeField] int stageScore;
    public string StageName {  get { return stageName; } }

    private GameObject info;
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
        
    }
}
