using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoader : MonoBehaviour
{
    [SerializeField] private GameObject audioManagerPrefab;

    void Awake()
    {
        if (!FindObjectOfType<AudioManager>()) //проверка наличия AudioManager во всех сценах - надо ли еще раз создавать (зависит от того с какой сцены запустили)
        {
            Instantiate(audioManagerPrefab);
        }
    }
}
