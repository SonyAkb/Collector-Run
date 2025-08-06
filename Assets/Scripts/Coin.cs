using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public Action OnCoinCollected; //событие - монету собрали
    public ParticleSystem pickupEffectPrefab;

    public float lifetime = 5f; //время жизни
    public Image progressBar; //изображение пргрессбара
    private float currentLifetime; //текущее время жизни

    private static List<ParticleSystem> effectsPool = new List<ParticleSystem>(); //пул эффектов

    void OnEnable()
    {
        currentLifetime = lifetime; //сброс таймера при активации
        if (progressBar != null) 
        { 
            progressBar.fillAmount = 1f;
            progressBar.gameObject.SetActive(true);
        }

        if (effectsPool == null)
        {
            effectsPool = new List<ParticleSystem>();
            SceneManager.sceneUnloaded += OnSceneUnloaded; //подписка на событие выгрузки сцены
        }
    }

    void Update()
    {
        if (currentLifetime > 0)
        {
            currentLifetime -= Time.deltaTime;

            if (progressBar != null)
            {
                progressBar.fillAmount = currentLifetime / lifetime; //обновляю прогрессбар
            }
        }
        else
        {
            OnCoinCollected?.Invoke();
            gameObject.SetActive(false); //время вышло (игрок не успел собрать монету) - убираю монету  
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //если столкнулись с игроком
        {
            GameManager.Instance.AddScore(1); //+1 очко
            OnCoinCollected?.Invoke();

            ParticleSystem effect = GetFreeEffect();//эффект на месте монеты
            effect.transform.position = transform.position; //актуальная позиция
            effect.Play(); //частицы

            AudioManager.Instance.PlayRandomCoinSound();

            gameObject.SetActive(false);
        }
    }

    ParticleSystem GetFreeEffect()
    {
        for (int i = effectsPool.Count - 1; i >= 0; i--) //устраняю все уничтоженные эффекты из списка
        {
            if (effectsPool[i] == null)
            {
                effectsPool.RemoveAt(i);
            }
        }

        foreach (var eff in effectsPool) //прохожу по всем эффектам в пуле и ищу неактивный
        {
            if (!eff.isPlaying)//неактивный эффект
            {
                return eff;
            }
        }

        ParticleSystem newEffect = Instantiate(pickupEffectPrefab); //нет свободных эффектов - создаем новый
        DontDestroyOnLoad(newEffect.gameObject); //не дает уничтожить объект
        effectsPool.Add(newEffect);
        return newEffect;
    }

    void OnSceneUnloaded(Scene scene) //очищение пула при выгрузке сцены
    {
        foreach (var effect in effectsPool) //прохожу по всем эффектам в пуле
        {
            if (effect != null) 
            { 
                Destroy(effect.gameObject); 
            }
        }
        effectsPool = null; //позволит заново задать пул при новой сцене
    }
}
