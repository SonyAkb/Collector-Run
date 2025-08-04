using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpawnManager;

public class SpawnManager : MonoBehaviour
{
    public CoinPool[] coinPools; //массив контейнеров всех доступных монет

    public float spawnTime = 2f; //новые монеты генерируются через это время
    public Vector2 spawnAreaMin = new Vector2(-9.5f, -4.0f); // область спавна
    public Vector2 spawnAreaMax = new Vector2(9.5f, 4.0f); // область спавна
    public int maxCoins = 10; //максимальное количество монет на карте 

    public List<GameObject> activeCoins = new List<GameObject>();//список всех активных монет

    void Start()
    {
        foreach (CoinPool pool in coinPools) //проход по всем контейнерам - типам монет
        {
            pool.pooledObjects = new List<GameObject>(); //список монет этого типа
            for (int i = 0; i < pool.poolSize; i++) //пока не добавлю все монетки этого типа
            {
                GameObject obj = Instantiate(pool.prefab); //создаю очередную монету
                obj.SetActive(false); //деактивирую монету
                pool.pooledObjects.Add(obj); //добавляю монету в пул
            }
        }

        InvokeRepeating("TrySpawnCoin", 0f, spawnTime); //спавн монет
    }

    void TrySpawnCoin()
    {
        if (activeCoins.Count >= maxCoins || coinPools.Length == 0) return;

        CoinPool randomPool = coinPools[UnityEngine.Random.Range(0, coinPools.Length)]; //случайная монета - шаблон
        GameObject coin = GetPooledCoin(randomPool); //выбор монеты из пула

        if (coin != null)
        {
            Vector2 spawnPos = new Vector2(
                UnityEngine.Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                UnityEngine.Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            coin.transform.position = spawnPos; //позиция
            coin.SetActive(true); //монета активна теперь
            activeCoins.Add(coin);//добавляю новую монету

            Coin coinScript = coin.GetComponent<Coin>();//отслеживает устранение монеты
            if (coinScript != null)
            {
                coinScript.OnCoinCollected += () => RemoveCoin(coin);
            }
        }
    }

    GameObject GetPooledCoin(CoinPool pool) //возврат монеты из пула
    {
        foreach (GameObject coin in pool.pooledObjects)
        {
            if (!coin.activeInHierarchy) //если монета не на поле
            {
                return coin;
            }
        }

        GameObject newCoin = Instantiate(pool.prefab); //все монеты заняты -> нужна новая - создаю
        newCoin.SetActive(false); //пока что монета неактивна
        pool.pooledObjects.Add(newCoin); //добавляю к доступным объектам
        return newCoin;
    }

    public void RemoveCoin(GameObject coin) 
    {
        if (coin == null) return;

        coin.SetActive(false); //монета неактивна теперь
        activeCoins.Remove(coin); // убираю монету которую собрали
    }

    [System.Serializable]
    public class CoinPool //контейнер для одного типа монет 
    {
        public GameObject prefab; //шаблон для монеты
        public int poolSize = 2; //сколько монет такого шаблона создать заранее
        public List<GameObject> pooledObjects; //все монеты такого шаблона
    }
}