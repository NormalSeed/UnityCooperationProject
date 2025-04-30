using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoUI : MonoBehaviour
{
    [SerializeField] StageManager stageManager;
    [SerializeField] TMP_Text stageName;
    [SerializeField] TMP_Text health;
    [SerializeField] TMP_Text score;

    private void Start()
    {
        stageName.text = stageManager.StageName;
    }
}

