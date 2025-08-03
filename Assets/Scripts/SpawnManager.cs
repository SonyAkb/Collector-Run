using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject coinPrefab; //шаблон для всех монет
    public float spawnTime = 2f; //новые монеты генерируются через это время
    public Vector2 spawnAreaMin; //область спавна (лево низ)
    public Vector2 spawnAreaMax; //область спавна (право верх)
    public int maxCoins = 10; //максимальное количество монет на карте 

    public List<GameObject> coinsActive = new List<GameObject>();//список всех монет которые активны (их еще не собрал игрок)

    void Start()
    {
        InvokeRepeating("TrySpawnCoin", 0f, spawnTime); //вызываю метод который спавнит монетки
    }

    void TrySpawnCoin()
    {
        if (coinsActive.Count < 10)
        {
            Vector2 spawnPos = new Vector2(Random.Range(spawnAreaMin.x, spawnAreaMax.x), Random.Range(spawnAreaMin.y, spawnAreaMax.y));
            GameObject newCoin = Instantiate(coinPrefab, spawnPos, Quaternion.identity);
            coinsActive.Add(newCoin); //добавляем новую активную монету
        } 
    }

    public void RemoveCoin(GameObject coin) //вызывается когда монету подбирают
    {
        coinsActive.Remove(coin); // удаляю монету которую нашли
    }
}
