using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] Button backButton;
    [SerializeField] Button retryButton;
    [SerializeField] Button titleButton;
    private void Start()
    {
        backButton.onClick.AddListener(UIManager.Instance.ExitUI);
        retryButton.onClick.AddListener(UIManager.Instance.ExitUI);
        retryButton.onClick.AddListener(GameManager.Instance.LoadCurrentScene);
        titleButton.onClick.AddListener(UIManager.Instance.ExitUI);
        titleButton.onClick.AddListener(GameManager.Instance.LoadFirstScene);
    }

}
