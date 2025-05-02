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
    // 게임 이름은 타이틀 UI에서 게임의 제목을 나타내기 위해 사용됩니다.
    [SerializeField] private string gameTitle = "BALL GAME";
    public string GameTitle { get { return gameTitle; } }

    // 남은 목숨을 나타냅니다.
    [SerializeField] private int lifePoint;
    [SerializeField] private int initialLifePoint;
    public int LifePoint
    {
        get
        {
            return lifePoint;
        }
        set
        {
            int next = value;
            if (next < 0)
            {
                next = 0;
            }
            lifePoint = next;
            onLifePointChanged?.Invoke();
        }
    }

    // 생명 포인트 값이 수정될 때 불러올 이벤트입니다.
    [SerializeField] public UnityEvent onLifePointChanged;

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
    private void Update()
    {
        // 테스트 코드
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    // 게임 매니저 세팅을 초기화합니다.
    // 이전에 열렸던 씬의 인덱스는 최초 -1로 지정합니다.
    public void Init()
    {
        lifePoint = initialLifePoint;
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
    // 가장 마지막에 열렸던 씬을 3번 씬으로 합니다.
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

    public void LoadCurrentScene() => LoadScene(currentSceneIndex);
    public void LoadFirstScene()
    {
        LoadScene(0);
        lifePoint = initialLifePoint;
    }
    public void GameOver()
    {
        UIManager.Instance.OpenGameOverUI();
    }
    public void Pause()
    {
        UIManager.Instance.OpenPauseUI();
    }
}
