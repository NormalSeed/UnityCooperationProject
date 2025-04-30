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
    // 게임 이름은 타이틀 UI에서 게임의 제목을 나타내기 위해 사용됩니다.
    [SerializeField] private string gameTitle = "BALL GAME";
    public string GameTitle { get { return gameTitle; } }
    // 플레이어의 체력 부분은 현재 사용처가 없습니다.
    // 추후 다른 팀원이 구현한 체력과 겹칠 경우 지울 예정입니다.
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
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
                next = maxHealth;
            }
            health = next;
        }
    }

    // 아래의 세 이벤트는 현재 사용처가 없습니다.
    [SerializeField] public UnityEvent onGameOvered;
    [SerializeField] public UnityEvent onLevelCleared;
    [SerializeField] public UnityEvent onHealthChanged;

    // 새로운 씬이 로드될 때 불러올 이벤트입니다.
    [SerializeField] public UnityEvent<int> onSceneLoaded;

    // 바로 이전에 열렸던 씬의 인덱스를 저장합니다. 최초 -1
    private int lastOpenedSceneIndex;
    public int LastOpendSceneIndex { get { return lastOpenedSceneIndex; } }

    // 현재 열려있는 씬의 인덱스를 저장힙니다.
    private int currentSceneIndex;
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
    // 아래의 키는 기능 테스트용입니다.
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            GotoTitle();
        }
    }
    // 게임 매니저 세팅을 초기화합니다.
    // 이전에 열렸던 씬의 인덱스는 최초 -1로 지정합니다.
    public void Init()
    {
        health = maxHealth;
        lastOpenedSceneIndex = -1;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
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
        onSceneLoaded?.Invoke(currentSceneIndex);
        SceneManager.LoadSceneAsync(currentSceneIndex);
    }
    // 지정한 이름의 씬으로 이동합니다.
    public void LoadScene(string name)
    {
        lastOpenedSceneIndex = currentSceneIndex;
        currentSceneIndex = SceneManager.GetSceneByName(name).buildIndex;
        onSceneLoaded?.Invoke(currentSceneIndex);
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
        onSceneLoaded?.Invoke(currentSceneIndex);
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
        onSceneLoaded?.Invoke(currentSceneIndex);
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
        onSceneLoaded?.Invoke(currentSceneIndex);
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
    public void GotoTitle()
    {
        LoadScene(0);
    }

    // 게임을 정지시키고 게임 오버 UI를 불러오는 함수입니다.
    // 현재 지정된 이벤트는 없습니다.
    public void GameOver()
    {
        Time.timeScale = 0.0f;
        onGameOvered?.Invoke();
        UIManager.Instance.OpenGameOverUI();
    }
    // 게임을 정지시키고 레벨 클리어 UI를 불러오는 함수입니다.
    // 현재 지정된 이벤트는 없습니다.
    public void LevelClear()
    {
        Time.timeScale = 0.0f;
        onLevelCleared?.Invoke();
        UIManager.Instance.OpenLevelClearUI();
    }
    // 게임을 정지시키고 게임 오버 UI를 불러오는 함수입니다.
    public void Pause()
    {
        Time.timeScale = 0.0f;
        UIManager.Instance.OpenPauseUI();
    }
}
