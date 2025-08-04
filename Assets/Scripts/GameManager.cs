using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //доступ из других скриптов

    public TextMeshProUGUI scoreText; //счетчик
    public TextMeshProUGUI timerText; //таймер

    public TextMeshProUGUI endGameText; //текст в конце раунда

    public GameObject gameOverPanel;

    private int score = 0; //первоначальный счет
    private float remainingTime = 10f;//первоначальное время
    private bool isGameActive = true; //флаг игры - игра идет или закончилась

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!isGameActive) return; //проверка - идет игра или нет

        remainingTime -= Time.deltaTime; //обновляю время
        timerText.text = $"Время: {(int)remainingTime}"; //вывожу

        if (remainingTime <= 0)//время закончилось
        {
            EndGame();
        }
    }

    public void AddScore(int value) //обновление счетчика монет
    {
        score += value;
        scoreText.text = $"Монет собрано: {score}";
    }

    void EndGame()
    {
        if (endGameText != null)
        {
            endGameText.text = $"Игра окончена! Монет собрано {score}";
        }
        else
        {
            Debug.LogError("EndGameText не назначен в инспекторе!");
        }

        isGameActive = false;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame() //перезапуск игры
    {
        Time.timeScale = 1;

        remainingTime = 10f;
        score = 0;
        scoreText.text = "Монет собрано: 0";

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu() //в главное меню
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene("MenuScene"); 
    }
}