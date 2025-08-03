using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //private SpawnManager spawnManager;
    //void Start()
    //{
    //    spawnManager = FindObjectOfType<SpawnManager>();
    //}

    public Action OnCoinCollected; //событие - монету собрали

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Столкновение любого объекта с монетой");
        if (other.CompareTag("Player")) //если столкнулись с игроком
        {
            Debug.Log("Монетка собрана!");
            //spawnManager.RemoveCoin(gameObject);
            OnCoinCollected?.Invoke();


            Destroy(gameObject);//удаляем монету
        }
        
    }
}
