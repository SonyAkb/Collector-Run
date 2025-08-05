using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }

    [SerializeField] private Image fadeImage; //то  что используется для затемнения
    [SerializeField] private float fadeDuration = 0.5f; //длительность перехода

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(FadeAndLoadScene(sceneName));
    }

    private IEnumerator FadeAndLoadScene(string sceneName) //для ассинхронного выполнения
    {
        Color originalColor = fadeImage.color;

        float elapsedTime = 0f; //время от начала затемнения 
        while (elapsedTime < fadeDuration) //пока не пройдет время затемнения
        {
            elapsedTime += Time.deltaTime; //время между каждрами
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha); //меняет прозрачность
            yield return null;
        }

        SceneManager.LoadScene(sceneName); //загрузка сцены

        elapsedTime = 0f; //время от начала осветления
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = 1 - Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        fadeImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0); //полная прозрачность
    }
}
