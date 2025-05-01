using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageFailedUI : MonoBehaviour
{
    [SerializeField] Button titleButton;
    [SerializeField] Button retryButton;
    private void Start()
    {
        retryButton.onClick.AddListener(UIManager.Instance.ExitUI);
        retryButton.onClick.AddListener(GameManager.Instance.LoadCurrentScene);
        titleButton.onClick.AddListener(UIManager.Instance.ExitUI);
        titleButton.onClick.AddListener(GameManager.Instance.LoadFirstScene);
    }
}
