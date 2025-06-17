using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gameWin;
    private bool isGameOver = false;
    private bool isGameWin = false;

    // Start is called before the first frame update
    void Start()
    {
        gameWin.SetActive(false);
        this.gameOverUI.SetActive(false);
        this.UpdateScore();
    }
    public void AddScore(int point)
    {
        if (!isGameOver)
        {
            score += point;
            this.UpdateScore();
        }
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void GameOver()
    {

        isGameOver = true;
        score = 0;
        Time.timeScale = 0;
        gameOverUI.SetActive(true);
    }

    public void GameWin()
    {
        isGameWin = true;
        Time.timeScale = 0;
        gameWin.SetActive(true);
    }
    public void RestartGame()
    {
        isGameOver = false;
        this.score = 0;
        this.UpdateScore();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public bool IsGameOver()
    {
        return this.isGameOver;
    }
    public bool IsGameWin()
    {
        return this.isGameWin;
    }

    public void GotoMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
