using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private string gameName = "공 굴리기 게임";
    [SerializeField] private bool isGameOver;
    [SerializeField] private int score;
    [SerializeField] private int maxScore;
    [SerializeField] private UnityEvent gameOverEvent;

    private int lastOpenedSceneIndex;
    private int currentSceneIndex;
    public bool IsGameOver { get { return isGameOver; } }
    public int Score { get { return score; } }
    public int LastOpendSceneIndex { get { return lastOpenedSceneIndex; } }
    public int CurrentSceneIndex {  get { return currentSceneIndex; } }
    
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
    public void Init()
    {
        score = 0;
        isGameOver = false;
        lastOpenedSceneIndex = -1;
        currentSceneIndex = 0;
        SceneManager.LoadSceneAsync(currentSceneIndex);
    }
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
    public void LoadScene(string name)
    {
        lastOpenedSceneIndex = currentSceneIndex;
        currentSceneIndex = SceneManager.GetSceneByName(name).buildIndex;
        SceneManager.LoadSceneAsync(currentSceneIndex);
    }
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

    public void ScoreUp(int amount = 1)
    {
        int nextScore = score + amount;
        if (nextScore >= maxScore)
        {
            score = maxScore;
            return;
        }
        score = nextScore;
    }
    public void ScoreDown(int amount = 1)
    {
        int nextScore = score - amount;
        if (nextScore <= 0)
        {
            score = 0;
            return;
        }
        score = nextScore;
    }

    public void GameOver()
    {
        gameOverEvent.Invoke();
    }
}
