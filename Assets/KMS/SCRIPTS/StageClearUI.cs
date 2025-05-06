using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageClearUI : MonoBehaviour
{
    [SerializeField] TMP_Text message;
    [SerializeField] TMP_Text buttonMessage;
    [SerializeField] Button nextButton;
    private void OnEnable()
    {
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(UIManager.Instance.ExitUI);
        if (GameManager.Instance.CurrentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            message.text = $"THANK YOU FOR PLAYING!";
            nextButton.onClick.AddListener(GameManager.Instance.LoadFirstScene);
            buttonMessage.text = "Title";
        }
        else
        {
            message.text = $"{StageManager.Instance.StageName} Clear!";
            nextButton.onClick.AddListener(GameManager.Instance.LoadNextScene);
            buttonMessage.text = "Next Stage";
        }
    }
}
