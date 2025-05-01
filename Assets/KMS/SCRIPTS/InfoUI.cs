using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoUI : MonoBehaviour
{
    [SerializeField] TMP_Text stageName;
    [SerializeField] TMP_Text life;
    [SerializeField] TMP_Text score;

    private void Start()
    {
        StageManager.Instance.onValueChanged.AddListener(UpdateInfo);
        GameManager.Instance.onLifePointChanged.AddListener(UpdateLifePoint);
        UpdateInfo();
        UpdateLifePoint();
    }
    public void UpdateInfo()
    {
        stageName.text = $"Stage : {StageManager.Instance.StageName}";
        life.text = $"Life : {GameManager.Instance.LifePoint}";
        score.text = $"Score : {StageManager.Instance.StageScore} / {StageManager.Instance.MaxStageScore}";
    }
    public void UpdateLifePoint()
    {
        life.text = $"Life : {GameManager.Instance.LifePoint}";
    }
}

