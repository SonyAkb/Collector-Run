using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public List<AudioClip> coinSounds; //все звуки - в ре дакторе настроить
    private List<AudioSource> audioSources = new List<AudioSource>(); //источники звука
    private int currentSourceIndex = 0; //индекс следующего используемого источника звука

    private int numberSoundSources = 5;

    public float coinVolume = 0.5f; //громкость

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
            source.playOnAwake = false; //отклучение автовоспроизведения
            audioSources.Add(source); //добавление источника в пул
        }
    }

    public void PlayRandomCoinSound()
    {
        if (coinSounds.Count == 0) return;

        AudioClip clip = coinSounds[Random.Range(0, coinSounds.Count)]; //случайный звук из списка

        AudioSource currentSource = audioSources[currentSourceIndex]; //очередной источник
        currentSource.PlayOneShot(clip);//воспроизведение звука

        currentSourceIndex = (currentSourceIndex + 1) % audioSources.Count;//переключаю индекс - теперь будет использован следующий источник звука
    }


}
