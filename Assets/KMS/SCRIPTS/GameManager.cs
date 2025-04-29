using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// 게임 매니저 컴포넌트입니다. 싱글톤을 상속받아 게임 내에 하나만 존재하며, 씬 전환시에도 사라지지 않습니다.
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private string gameTitle = "BALL GAME";
    [SerializeField] private int score;
    [SerializeField] private int maxScore;
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject levelClearUI;
    private List<GameObject> UIList = new List<GameObject>();
    private GameObject currentUI;
    private bool isUIOpend = false;

    [SerializeField] public UnityEvent onGameOvered;
    [SerializeField] public UnityEvent onHealthChanged;
    [SerializeField] public UnityEvent onScoreChanged;

    // 바로 이전에 열렸던 씬의 인덱스를 저장합니다. 최초 -1
    private int lastOpenedSceneIndex;
    // 현재 열려있는 씬의 인덱스를 저장힙니다.
    private int currentSceneIndex;

    public string GameTitle {  get { return gameTitle; } }
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            onScoreChanged.Invoke();
            int next = value;
            if (next < 0)
            {
                next = 0;
            }
            else if (next > maxScore)
            {
                next = maxScore;
            }
            score = next;
        }
    }
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            onHealthChanged.Invoke();
            int next = value;
            if (next < 0)
            {
                next = 0;
            }
            else if (next > maxHealth)
            {
                next = maxScore;
            }
            health = next;
        }
    }
    public int LastOpendSceneIndex { get { return lastOpenedSceneIndex; } }
    public int CurrentSceneIndex {  get { return currentSceneIndex; } }
    
    // 현재 게임에 빌드되어 있는 씬의 개수를 가져옵니다.
    private int SceneLength => SceneManager.sceneCountInBuildSettings;
    private void Awake()
    {
        SetInstance();
    }
    private void Start()
    {
        Init();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            GameOver();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            LevelClear();
        }
    }
    // 게임 매니저 세팅을 초기화합니다.
    public void Init()
    {
        score = 0;
        health = maxHealth;
        lastOpenedSceneIndex = -1;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        UIInit(pauseUI);
        UIInit(gameOverUI);
        UIInit(levelClearUI);
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
    // 지정한 인덱스의 씬으로 이동합니다.
    public void LoadScene(int index)
    {
        if (index < 0 || index >= SceneLength)
        {
            return;
        }
        lastOpenedSceneIndex = currentSceneIndex;
        currentSceneIndex = index;
        SceneManager.LoadSceneAsync(currentSceneIndex);
    }
    // 지정한 이름의 씬으로 이동합니다.
    public void LoadScene(string name)
    {
        lastOpenedSceneIndex = currentSceneIndex;
        currentSceneIndex = SceneManager.GetSceneByName(name).buildIndex;
        SceneManager.LoadSceneAsync(currentSceneIndex);
    }
    // 가장 마지막에 열렸던 씬으로 이동합니다.
    // 현재 3번 씬이고, 이전에 1번 씬이었다면, 1번 씬으로 이동하고
    // 가장 마지막여 열렸던 씬을 3번 씬으로 합니다.
    public void LoadLastOpenedScene()
    {
        if (lastOpenedSceneIndex == -1)
        {
            return;
        }
        int temp = currentSceneIndex;
        currentSceneIndex = lastOpenedSceneIndex;
        lastOpenedSceneIndex = temp;
        SceneManager.LoadSceneAsync(currentSceneIndex);

    }
    // 현재 바로 다음 인덱스의 씬으로 이동합니다.
    // 예를 들어, 1번 씬에서 2번 씬으로 이동합니다.
    public void LoadNextScene()
    {
        Debug.Log(currentSceneIndex);
        if (currentSceneIndex == SceneLength - 1)
        {
            return;
        }
        lastOpenedSceneIndex = currentSceneIndex;
        currentSceneIndex++;
        SceneManager.LoadSceneAsync(currentSceneIndex);
    }
    // 현재 바로 다음 인덱스의 씬으로 이동합니다.
    // 예를 들어, 2번 씬에서 1번 씬으로 이동합니다.
    public void LoadPreviousScene()
    {
        if (currentSceneIndex == 0)
        {
            return;
        }
        lastOpenedSceneIndex = currentSceneIndex;
        currentSceneIndex--;
        SceneManager.LoadSceneAsync(currentSceneIndex);
    }
    public void RestartLevel()
    {
        LoadScene(currentSceneIndex);
    }
    public void RestartGame()
    {
        LoadScene(1);
    }

    // 게임을 정지시키고 게임오버 UI를 활성화합니다.
    public void GameOver()
    {
        if ( currentSceneIndex == 0 || isUIOpend == true)
        {
            return;
        }
        isUIOpend = true;
        onGameOvered.Invoke();
        currentUI = UIList[1];
        currentUI.SetActive(true);
        Time.timeScale = 0.0f;
    }
    // 게임을 정지시키고 레벨 클리어 UI를 활성화합니다.
    public void LevelClear()
    {
        if (currentSceneIndex == 0 || isUIOpend == true)
        {
            return;
        }
        isUIOpend = true;
        currentUI = UIList[2];
        currentUI.SetActive(true);
        Time.timeScale = 0.0f;
    }// 게임을 정지시키고 일시정지 UI를 활성화합니다.
    public void Pause()
    {
        if (currentSceneIndex == 0 || isUIOpend == true)
        {
            return;
        }
        isUIOpend = true;
        currentUI = UIList[0];
        currentUI.SetActive(true);
        Time.timeScale = 0.0f;
    }
    // UI를 비활성화시켜 UI에서 빠져나오는 함수입니다.
    // 시간을 다시 재개시킵니다.
    public void ExitUI()
    {
        isUIOpend = false;
        currentUI.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
