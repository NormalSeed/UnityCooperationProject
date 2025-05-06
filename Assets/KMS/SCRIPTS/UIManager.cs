using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

// UI를 관리하는 UI매니저 싱글톤 컴포넌트입니다. 게임매니저 게임오브젝트에 추가되어 있습니다.
public class UIManager : Singleton<UIManager>
{

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject stageClearUI;
    [SerializeField] private GameObject stageFailedUI;
    [SerializeField] private GameObject infoUI;

    // 스크린 UI의 참조를 담는 딕셔너리입니다.
    private Dictionary<string, GameObject> screenUIDict = new Dictionary<string, GameObject>();
    private GameObject currentUI;
    private GameObject info;
    private bool isScreenUIOpened = false;
    public bool IsScreenUIOpened { get {  return isScreenUIOpened; } }
    private void Awake()
    {
        SetInstance();
    }
    private void Start()
    {
        Init();
        GameManager.Instance.onSceneLoaded.AddListener(ToggleInfoUI);
        ToggleInfoUI(GameManager.Instance.CurrentSceneIndex);
    }

    // 세팅을 초기화합니다.
    private void Init()
    {
        InitScreenUI("pause", pauseUI);
        InitScreenUI("gameover", gameOverUI);
        InitScreenUI("clear", stageClearUI);
        InitScreenUI("failed", stageFailedUI);

        info = Instantiate(infoUI);
        DontDestroyOnLoad(info);
        info.SetActive(false);
    }
    // 초기화 시에 호출되는 함수로
    // 필요한 UI를 불러와 사라지지 않도록 만들고, UI 리스트에 추가합니다.
    private void InitScreenUI(string name, GameObject UI)
    {
        currentUI = Instantiate(UI);
        DontDestroyOnLoad(currentUI);
        currentUI.SetActive(false);
        screenUIDict.Add(name, currentUI);
    }

    // 지정된 스크린UI를 딕셔너리에서 불러와 활성화합니다.
    public void OpenScreenUI(string name)
    {
        if (isScreenUIOpened)
        {
            return;
        }
        isScreenUIOpened = true;
        currentUI = screenUIDict[name];
        currentUI.SetActive(true);
    }
    public void OpenPauseUI()
    {
        OpenScreenUI("pause");
    }
    public void OpenGameOverUI()
    {
        OpenScreenUI("gameover");
    }
    public void OpenStageClearUI()
    {
        OpenScreenUI("clear");
    }
    public void OpenStageFailedUI()
    {
        OpenScreenUI("failed");
    }
    // UI를 비활성화시켜 UI에서 빠져나오는 함수입니다.
    // 게임이 정지되었을 경우 다시 재개시킵니다.
    public void ExitUI()
    {
        Time.timeScale = 1.0f;
        isScreenUIOpened = false;
        currentUI.SetActive(false);
    }

    // 게임 정보 UI의 활성화 여부를 결정합니다.
    // 해당 함수는 onSceneLoaded 이벤트에 할당되어 있어 씬이 바뀔때마다 호출됩니다.
    // 결과적으로 타이틀 씬이 아닐 경우에만 UI를 활성화하는 기능을 합니다.
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
