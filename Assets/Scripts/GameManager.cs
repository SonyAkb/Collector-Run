using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //доступ из других скриптов

    public TextMeshProUGUI scoreText; //счетчик
    public TextMeshProUGUI timerText; //таймер

    public TextMeshProUGUI endGameText; //текст в конце раунда
    public TextMeshProUGUI bestScoreText; //рекорд

    public GameObject gameOverPanel;

    private int score = 0; //первоначальный счет
    private float remainingTime = 60f;//первоначальное время
    private bool isGameActive = true; //флаг игры - игра идет или закончилась

    void Start()
    {
        UpdateBestScoreUI(); //данные о последнем рекорде
    }

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

        if (score > PlayerPrefs.GetInt("BestScore", 0)) //обновляю рекорд
        {
            PlayerPrefs.SetInt("BestScore", score);
            UpdateBestScoreUI(); 
        }
    }

    void UpdateBestScoreUI()
    {
        if (bestScoreText != null)
        {
            bestScoreText.text = $"Рекорд: {PlayerPrefs.GetInt("BestScore", 0)}";
        }
    }

    void EndGame()
    {
        if (endGameText != null)
        {
            endGameText.text = $"Игра окончена! Монет собрано {score}\nРекорд: {PlayerPrefs.GetInt("BestScore", 0)}";
        }
        else
        {
            Debug.LogError("EndGameText не назначен в инспекторе!");
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopMusic(); //безопасная остановка музыки
        }
        else
        {
            Debug.LogWarning("AudioManager не найден!");
        }

        var player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.SetCanMove(false);
        }

        gameOverPanel.SetActive(true);
        isGameActive = false;
        Time.timeScale = 0;
        
        
    }

    public void RestartGame() //перезапуск игры
    {
        Time.timeScale = 1;
        remainingTime = 60f;
        score = 0;
        scoreText.text = "Монет собрано: 0";

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu() //в главное меню
    {
        PlayerPrefs.Save();
        Time.timeScale = 1;

        if (SceneTransitionManager.Instance != null)
        {
            SceneTransitionManager.Instance.LoadSceneWithFade("MenuScene");
        }
        else
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}