using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoUI : MonoBehaviour
{
    [SerializeField] TMP_Text stageName;
    [SerializeField] TMP_Text life;
    [SerializeField] TMP_Text score;
    [SerializeField] TMP_Text seconds;

    private void Start()
    {
        StageManager.Instance.onStageValueChanged.AddListener(UpdateAll);
        GameManager.Instance.onLifePointChanged.AddListener(UpdateLifePoint);
        StageManager.Instance.onSeconds.AddListener(UpdateSeconds);
        UpdateAll();
        UpdateLifePoint();
    }
    public void UpdateAll()
    {
        stageName.text = $"Stage : {StageManager.Instance.StageName}";
        life.text = $"Life : {GameManager.Instance.LifePoint}";
        score.text = $"Score : {StageManager.Instance.StageScore} / {StageManager.Instance.MaxStageScore}";
        seconds.text = $"Time : {StageManager.Instance.seconds}";
    }
    public void UpdateLifePoint()
    {
        life.text = $"Life : {GameManager.Instance.LifePoint}";
    }
    public void UpdateSeconds()
    {
        seconds.text = $"Time : {StageManager.Instance.seconds}";
    }
}

