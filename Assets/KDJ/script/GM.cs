using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    private static GM instance;
    GameObject m_ball;

    public float initBallSpeed;
    public Vector3 m_initBallVector;

    public int score = 0;
    public int highScore = 0;
    public bool isGameOver = false;

    public Text scoreText;     // UI 텍스트를 인스펙터에서 연결
    public Text gameOverText;

    public static GM Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        m_ball = GameObject.Find("Ball");
    }

    void Start()
    {
        if (m_ball != null)
        {
            var ballScript = m_ball.GetComponent<BallScript>();
            ballScript.ballSpeed = initBallSpeed;
            ballScript.ballMovementVector = m_initBallVector;
        }

        UpdateScoreUI();
        if (gameOverText != null)
            gameOverText.enabled = false;
    }

    void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (score > highScore)
            highScore = score;

        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score} | HighScore: {highScore}";
    }

    public void GameOver()
    {
        isGameOver = true;
        if (gameOverText != null)
            gameOverText.enabled = true;
    }

    void RestartGame()
    {
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static implicit operator GM(BallPlay v)
    {
        throw new NotImplementedException();
    }
}
