using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageClearUI : MonoBehaviour
{
    [SerializeField] Button nextButton;
    private void Start()
    {
        nextButton.onClick.AddListener(UIManager.Instance.ExitUI);
        nextButton.onClick.AddListener(GameManager.Instance.LoadNextScene);
    }
}
