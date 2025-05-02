using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleUI : MonoBehaviour
{
    [SerializeField] Button start;
    [SerializeField] Button quit;
    [SerializeField] TMP_Text text;
    private void Start()
    {
        start.onClick.AddListener(GameManager.Instance.LoadNextScene);
        quit.onClick.AddListener(Application.Quit);
        text.text = GameManager.Instance.GameTitle;
    }
}
