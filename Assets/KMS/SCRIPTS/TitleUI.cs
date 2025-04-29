using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleUI : MonoBehaviour
{
    [SerializeField] Button btn;
    [SerializeField] TMP_Text text;
    private void Start()
    {
        btn.onClick.AddListener(GameManager.Instance.LoadNextScene);
        text.text = GameManager.Instance.GameTitle;
    }
}
