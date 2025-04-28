using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] bool isGameOver;
    [SerializeField] int score;
    private void Awake()
    {
        SetInstance();
    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
