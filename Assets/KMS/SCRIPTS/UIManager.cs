using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : Singleton<UIManager>
{

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject levelClearUI;

    [SerializeField] private GameObject infoUI;
    private List<GameObject> UIList = new List<GameObject>();
    private GameObject currentUI;
    private GameObject info;
    private bool isUIOpend = false;
    private void Awake()
    {
        SetInstance();
    }
    private void Start()
    {
        Init();
        GameManager.Instance.onSceneLoaded.AddListener(ToggleInfoUI);
    }
    private void Update()
    {
    }
    // 매니저 세팅을 초기화합니다.
    public void Init()
    {
        UIInit(pauseUI);
        UIInit(gameOverUI);
        UIInit(levelClearUI);
        info = Instantiate(infoUI);
        DontDestroyOnLoad(info);
        info.SetActive(false);
    }
    // 게임 매니저 초기화 시에 호출되는 함수로
    // 필요한 UI를 불러와 사라지지 않도록 만들고, UI 리스트에 추가합니다.
    private void UIInit(GameObject UI)
    {
        currentUI = Instantiate(UI);
        DontDestroyOnLoad(currentUI);
        currentUI.SetActive(false);
        UIList.Add(currentUI);
    }
    // 게임을 정지시키고 게임오버 UI를 활성화합니다.
    public void OpenGameOverUI()
    {
        if (isUIOpend == true)
        {
            return;
        }
        isUIOpend = true;
        currentUI = UIList[1];
        currentUI.SetActive(true);
    }
    // 게임을 정지시키고 레벨 클리어 UI를 활성화합니다.
    public void OpenLevelClearUI()
    {
        if (isUIOpend == true)
        {
            return;
        }
        isUIOpend = true;
        currentUI = UIList[2];
        currentUI.SetActive(true);
    }// 게임을 정지시키고 일시정지 UI를 활성화합니다.
    public void OpenPauseUI()
    {
        if (isUIOpend == true)
        {
            return;
        }
        isUIOpend = true;
        currentUI = UIList[0];
        currentUI.SetActive(true);
    }
    // UI를 비활성화시켜 UI에서 빠져나오는 함수입니다.
    // 시간을 다시 재개시킵니다.
    public void ExitUI()
    {
        Time.timeScale = 1.0f;
        isUIOpend = false;
        currentUI.SetActive(false);
    }
    public void ToggleInfoUI(int SceneIndex)
    {
        if (info.activeSelf && SceneIndex == 0)
        {
            info.SetActive(false);
        }
        else if (!info.activeSelf && SceneIndex != 0)
        {
            info.SetActive(true);
        }
    }
}
