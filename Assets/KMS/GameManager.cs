using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] bool isGameOver;
    [SerializeField] int score;
    private Scene lastOpenedScene;
    private Scene currentScene;
    private void Awake()
    {
        SetInstance();
    }
    private void Start()
    {
        //previousScene = SceneManager.GetActiveScene();
        currentScene = SceneManager.GetActiveScene();
    }
    private void Update()
    {

    }

    public void LoadScene(int index)
    {
        lastOpenedScene = currentScene;
        currentScene = SceneManager.GetSceneByBuildIndex(index);
        SceneManager.LoadSceneAsync(currentScene.buildIndex);
    }
    public void LoadScene(string name)
    {
        lastOpenedScene = currentScene;
        currentScene = SceneManager.GetSceneByName(name);
        SceneManager.LoadSceneAsync(currentScene.buildIndex);
    }
    public void LoadLastOpenedScene()
    {
        Scene temp = currentScene;
        currentScene = lastOpenedScene;
        lastOpenedScene = temp;
        SceneManager.LoadSceneAsync(currentScene.buildIndex);
    }
    public void LoadNextScene()
    {
        lastOpenedScene = currentScene;
        currentScene = SceneManager.GetSceneByBuildIndex(currentScene.buildIndex + 1);
        SceneManager.LoadSceneAsync(currentScene.buildIndex);
    }
    public void LoadPreviousScene()
    {
        lastOpenedScene = currentScene;
        currentScene = SceneManager.GetSceneByBuildIndex(currentScene.buildIndex - 1);
        SceneManager.LoadSceneAsync(currentScene.buildIndex);
    }
}
