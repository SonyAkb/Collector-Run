using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public Action OnCoinCollected; //событие - монету собрали

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //если столкнулись с игроком
        {
            //Debug.Log("Монетка собрана!");
  
            GameManager.Instance.AddScore(1); //+1 очко
            OnCoinCollected?.Invoke();

            gameObject.SetActive(false);
            //Destroy(gameObject);//удаляем монету
        }
        
    }
}
