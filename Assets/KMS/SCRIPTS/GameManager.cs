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

    [SerializeField] public UnityEvent onGameOvered;
    [SerializeField] public UnityEvent onLevelCleared;
    [SerializeField] public UnityEvent onHealthChanged;
    [SerializeField] public UnityEvent onScoreChanged;
    [SerializeField] public UnityEvent onSceneLoaded;

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
                next = maxHealth;
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
        onSceneLoaded?.Invoke();
        SceneManager.LoadSceneAsync(currentSceneIndex);
    }
    // 지정한 이름의 씬으로 이동합니다.
    public void LoadScene(string name)
    {
        lastOpenedSceneIndex = currentSceneIndex;
        currentSceneIndex = SceneManager.GetSceneByName(name).buildIndex;
        onSceneLoaded?.Invoke();
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
        onSceneLoaded?.Invoke();
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
        onSceneLoaded?.Invoke();
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
        onSceneLoaded?.Invoke();
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
    public void GameOver()
    {
        Time.timeScale = 0.0f;
        onGameOvered?.Invoke();
        UIManager.Instance.OpenGameOverUI();
    }
    public void LevelClear()
    {
        Time.timeScale = 0.0f;
        onLevelCleared?.Invoke();
        UIManager.Instance.OpenLevelClearUI();
    }
    public void Pause()
    {
        Time.timeScale = 0.0f;
        UIManager.Instance.OpenPauseUI();
    }
}
