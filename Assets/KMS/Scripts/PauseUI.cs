using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] Button btn;
    private void Start()
    {
        btn.onClick.AddListener(GameManager.Instance.ExitUI);
    }

}
