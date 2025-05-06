using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] Button titleButton;
    private void Start()
    {
        titleButton.onClick.AddListener(UIManager.Instance.ExitUI);
        titleButton.onClick.AddListener(GameManager.Instance.LoadFirstScene);
    }
}
