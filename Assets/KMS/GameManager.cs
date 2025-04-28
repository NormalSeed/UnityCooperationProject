using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// 게임 매니저 컴포넌트입니다. 싱글톤을 상속받아 게임 내에 하나만 존재하며, 씬 전환시에도 사라지지 않습니다.
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private string gameTitle = "공 굴리기 게임";
    [SerializeField] private bool isGameOver;
    [SerializeField] private int score;
    [SerializeField] private int maxScore;
    // 게임 오버 시에 실행될 이벤트들을 지정합니다.
    [SerializeField] private UnityEvent gameOverEvent;

    // 바로 이전에 열렸던 씬의 인덱스를 저장합니다. 최초 -1
    private int lastOpenedSceneIndex;
    // 현재 열려있는 씬의 인덱스를 저장힙니다.
    private int currentSceneIndex;
    public string GameTitle {  get { return gameTitle; } }
    public bool IsGameOver { get { return isGameOver; } }
    public int Score { get { return score; } }
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            LoadScene(2);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            LoadScene(3);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LoadPreviousScene();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            LoadNextScene();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            LoadLastOpenedScene();
        }
    }
    // 게임 매니저 세팅을 초기화합니다.
    // 점수를 0점으로 하며, 인덱스 0번의 씬으로 이동합니다.
    public void Init()
    {
        score = 0;
        isGameOver = false;
        lastOpenedSceneIndex = -1;
        currentSceneIndex = 0;
        SceneManager.LoadSceneAsync(currentSceneIndex);
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

    // 지정한 정수만큼 점수를 올립니다.
    // 점수는 maxScore보다 커질 수 없습니다.
    public void ScoreUp(int amount = 1)
    {
        if (amount <= 0)
        {
            amount = 0;
        }
        int nextScore = score + amount;
        if (nextScore >= maxScore)
        {
            score = maxScore;
            return;
        }
        score = nextScore;
    }

    // 지정한 정수만큼 점수를 내립니다.
    // 점수는 0 보다 작아질 수 없습니다.
    public void ScoreDown(int amount = 1)
    {
        if (amount <= 0)
        {
            amount = 0;
        }
        int nextScore = score - amount;
        if (nextScore <= 0)
        {
            score = 0;
            return;
        }
        score = nextScore;
    }

    // 게임 오버 시 지정된 이벤트를 실행합니다.
    public void GameOver()
    {
        gameOverEvent.Invoke();
    }
}
