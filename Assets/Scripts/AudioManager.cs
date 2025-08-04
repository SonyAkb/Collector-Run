using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
        
    public List<AudioClip> coinSounds; //список всех доступных звуков для монет - в редакторе настроить
    private List<AudioSource> audioSources = new List<AudioSource>(); //источники звука - пул
    private int currentSourceIndex = 0;//индекс следующего используемого источника звука
    private int numberSoundSources = 5;
    public float coinVolume = 0.5f; //громкость звуков монет
        
    public AudioClip menuMusic; //музыка меню
    public AudioClip gameMusic; //музыка игры
    private AudioSource musicSource; //источник для фоновой музыки
    private string currentScene; //текущая сцена
    public float bgVolume = 0.7f; //громкость фоновой музыки

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        for (int i = 0; i < numberSoundSources; i++) //создание нескольких аудио источников
        {
            AudioSource source = gameObject.AddComponent<AudioSource>(); //очередной источник звука
            source.playOnAwake = false; //отключение автовоспроизведения
            audioSources.Add(source); //добавление источника в пул
        }

        musicSource = gameObject.AddComponent<AudioSource>(); //источник музыки
        musicSource.loop = true; //зацикливаю музыку
        musicSource.volume = bgVolume; //громкость фоновой музыки

        SceneManager.sceneLoaded += OnSceneLoaded; //подписка на событие загрузки сцены
    }
     

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) //обработка смены сцены 
    {
        currentScene = scene.name; //запоминаю имя текущей сцены
        PlaySceneMusic(); //запускаю нужную музыку
    }

    void PlaySceneMusic() //фоновая музыка для текущей сцены
    {
        if (musicSource == null) return;
        musicSource.Stop(); //предыдущая музыка - стоп

        switch (currentScene) //выбор музыки в соответсвии с текущей сценой
        {
            case "MenuScene":
                if (menuMusic != null)
                {
                    musicSource.clip = menuMusic;
                    musicSource.Play();
                }
                break;

            case "GameScene":
                if (gameMusic != null)
                {
                    musicSource.clip = gameMusic;
                    musicSource.Play();
                }
                break;
        }
    }

    public void StopMusic() //принудительная остановка музыки
    {
        if (musicSource != null)
            musicSource.Stop();
    }

    public void PlayRandomCoinSound() //запуск случайного звука подбора монеты
    {
        if (coinSounds.Count == 0) return;

        AudioClip clip = coinSounds[Random.Range(0, coinSounds.Count)]; //случайный звук из списка
        AudioSource currentSource = audioSources[currentSourceIndex]; //очередной источник
        currentSource.PlayOneShot(clip, coinVolume); //воспроизведение звука
        currentSourceIndex = (currentSourceIndex + 1) % audioSources.Count;//переключаю индекс - теперь будет использован следующий источник звука
    }


}
