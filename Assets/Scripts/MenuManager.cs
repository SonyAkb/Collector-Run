using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText; //рекорд

    void Start()
    {
        UpdateBestScoreUI();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); 
    }

    void UpdateBestScoreUI() //обновление рекорда
    {
        if (bestScoreText != null)
        {
            bestScoreText.text = $"Рекорд: {PlayerPrefs.GetInt("BestScore", 0)}";
        }
    }

    public void ResetRecord() //сброс рекорда
    {
        PlayerPrefs.DeleteKey("BestScore");
        UpdateBestScoreUI(); //обновляю текст
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // Остановка в редакторе
        #else
                    Application.Quit(); // Выход в собранной версии
        #endif
    }
}
