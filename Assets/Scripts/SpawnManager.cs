using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] coinPrefab; //шаблон для всех монет
    public float spawnTime = 2f; //новые монеты генерируются через это время
    public Vector2 spawnAreaMin = new Vector2(-9.5f, -4.0f); // область спавна
    public Vector2 spawnAreaMax = new Vector2(9.5f, 4.0f); // область спавна
    public int maxCoins = 10; //максимальное количество монет на карте 

    public List<GameObject> coinsActive = new List<GameObject>();//список всех активных монет

    void Start()
    {
        InvokeRepeating("TrySpawnCoin", 0f, spawnTime); //спавн монет
    }

    void TrySpawnCoin()
    {
        if (coinsActive.Count < maxCoins && coinPrefab.Length > 0)
        {

            if (coinPrefab == null || coinPrefab.Length == 0)
            {
                Debug.LogError("Нет шаблона для монеты!");
                return;
            }
            Debug.Log("+монета");
            GameObject randomPrefab = coinPrefab[UnityEngine.Random.Range(0, coinPrefab.Length)];

            Vector2 spawnPos = new Vector2(UnityEngine.Random.Range(spawnAreaMin.x, spawnAreaMax.x), UnityEngine.Random.Range(spawnAreaMin.y, spawnAreaMax.y));

            GameObject newCoin = Instantiate(randomPrefab, spawnPos, Quaternion.identity);
            coinsActive.Add(newCoin); //добавляю новую монету

            Coin coinScript = newCoin.GetComponent<Coin>(); //отслеживает удаление монеты
            if (coinScript != null)
            {
                coinScript.OnCoinCollected += () => RemoveCoin(newCoin);
            }
        }
    }

    public void RemoveCoin(GameObject coin) 
    {
        if (coin == null) return;


        Debug.Log("-монета" + " сейчас всего монет: " + coinsActive.Count);
        coinsActive.Remove(coin); // удаляю монету которую собрали
    }
}