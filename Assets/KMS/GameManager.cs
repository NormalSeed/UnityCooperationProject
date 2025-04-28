using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// ���� �Ŵ��� ������Ʈ�Դϴ�. �̱����� ��ӹ޾� ���� ���� �ϳ��� �����ϸ�, �� ��ȯ�ÿ��� ������� �ʽ��ϴ�.
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private string gameTitle = "�� ������ ����";
    [SerializeField] private bool isGameOver;
    [SerializeField] private int score;
    [SerializeField] private int maxScore;
    // ���� ���� �ÿ� ����� �̺�Ʈ���� �����մϴ�.
    [SerializeField] private UnityEvent gameOverEvent;

    // �ٷ� ������ ���ȴ� ���� �ε����� �����մϴ�. ���� -1
    private int lastOpenedSceneIndex;
    // ���� �����ִ� ���� �ε����� �������ϴ�.
    private int currentSceneIndex;
    public string GameTitle {  get { return gameTitle; } }
    public bool IsGameOver { get { return isGameOver; } }
    public int Score { get { return score; } }
    public int LastOpendSceneIndex { get { return lastOpenedSceneIndex; } }
    public int CurrentSceneIndex {  get { return currentSceneIndex; } }
    
    // ���� ���ӿ� ����Ǿ� �ִ� ���� ������ �����ɴϴ�.
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
    // ���� �Ŵ��� ������ �ʱ�ȭ�մϴ�.
    // ������ 0������ �ϸ�, �ε��� 0���� ������ �̵��մϴ�.
    public void Init()
    {
        score = 0;
        isGameOver = false;
        lastOpenedSceneIndex = -1;
        currentSceneIndex = 0;
        SceneManager.LoadSceneAsync(currentSceneIndex);
    }
    // ������ �ε����� ������ �̵��մϴ�.
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
    // ������ �̸��� ������ �̵��մϴ�.
    public void LoadScene(string name)
    {
        lastOpenedSceneIndex = currentSceneIndex;
        currentSceneIndex = SceneManager.GetSceneByName(name).buildIndex;
        SceneManager.LoadSceneAsync(currentSceneIndex);
    }
    // ���� �������� ���ȴ� ������ �̵��մϴ�.
    // ���� 3�� ���̰�, ������ 1�� ���̾��ٸ�, 1�� ������ �̵��ϰ�
    // ���� �������� ���ȴ� ���� 3�� ������ �մϴ�.
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
    // ���� �ٷ� ���� �ε����� ������ �̵��մϴ�.
    // ���� ���, 1�� ������ 2�� ������ �̵��մϴ�.
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
    // ���� �ٷ� ���� �ε����� ������ �̵��մϴ�.
    // ���� ���, 2�� ������ 1�� ������ �̵��մϴ�.
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

    // ������ ������ŭ ������ �ø��ϴ�.
    // ������ maxScore���� Ŀ�� �� �����ϴ�.
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

    // ������ ������ŭ ������ �����ϴ�.
    // ������ 0 ���� �۾��� �� �����ϴ�.
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

    // ���� ���� �� ������ �̺�Ʈ�� �����մϴ�.
    public void GameOver()
    {
        gameOverEvent.Invoke();
    }
}
