using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoUI : MonoBehaviour
{
    [SerializeField] TMP_Text stageName;
    [SerializeField] TMP_Text health;
    [SerializeField] TMP_Text score;

    private void Start()
    {
        StageManager.Instance.onValueChanged.AddListener(UpdateInfo);
        UpdateInfo();
    }
    public void UpdateInfo()
    {
        stageName.text = $"Stage : {StageManager.Instance.StageName}";
        score.text = $"Score : {StageManager.Instance.StageScore} / {StageManager.Instance.MaxStageScore}";
    }
}

