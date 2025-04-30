using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] Button btn;
    private void Start()
    {
        btn.onClick.AddListener(UIManager.Instance.ExitUI);
        btn.onClick.AddListener(GameManager.Instance.RestartLevel);
    }
}
