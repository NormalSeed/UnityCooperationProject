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
    // 세팅을 초기화합니다.
    public void Init()
    {
        UIInit(pauseUI);
        UIInit(gameOverUI);
        UIInit(stageClearUI);
        UIInit(stageFailedUI);
        info = Instantiate(infoUI);
        DontDestroyOnLoad(info);
        info.SetActive(false);
    }
    // 초기화 시에 호출되는 함수로
    // 필요한 UI를 불러와 사라지지 않도록 만들고, UI 리스트에 추가합니다.
    private void UIInit(GameObject UI)
    {
        currentUI = Instantiate(UI);
        DontDestroyOnLoad(currentUI);
        currentUI.SetActive(false);
        UIList.Add(currentUI);
    }
    // 게임오버 UI를 활성화합니다.
    // 게임매니저 측의 GameOver 함수에 포함되어있습니다.

    public void OpenUI(int index)
    {
        if (isUIOpend == true)
        {
            return;
        }
        isUIOpend = true;
        currentUI = UIList[index];
        currentUI.SetActive(true);
    }
    // 일시정지 UI를 활성화합니다.
    // 게임매니저 측의 Pause 함수에 포함되어있습니다.
    public void OpenPauseUI()
    {
        OpenUI(0);
    }
    public void OpenGameOverUI()
    {
        OpenUI(1);
    }
    // 레벨 클리어 UI를 활성화합니다.
    // 게임매니저 측의 LevelClear 함수에 포함되어있습니다.
    public void OpenStageClearUI()
    {
        OpenUI(2);

    }
    public void OpenStageFailedUI()
    {
        OpenUI(3);
    }
    // UI를 비활성화시켜 UI에서 빠져나오는 함수입니다.
    // 게임이 정지되었을 경우 다시 재개시킵니다.
    public void ExitUI()
    {
        Time.timeScale = 1.0f;
        isUIOpend = false;
        currentUI.SetActive(false);
    }

    // 게임 정보 UI의 활성화 여부를 결정합니다.
    // 해당 함수는 onSceneLoaded 이벤트에 할당되어 있어 씬이 바뀔때마다 호출됩니다.
    // 결과적으로 타이틀 씬이 아닐 경우에만 UI를 활성화합니다.
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
