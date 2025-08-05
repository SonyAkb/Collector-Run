using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionInitializer : MonoBehaviour
{
    [SerializeField] private GameObject sceneTransitionPrefab;

    void Awake()
    {
        if (SceneTransitionManager.Instance == null)
        {
            Instantiate(sceneTransitionPrefab);
        }
    }
}
