using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject coinPrefab; //������ ��� ���� �����
    public float spawnTime = 2f; //����� ������ ������������ ����� ��� �����
    public Vector2 spawnAreaMin; //������� ������ (���� ���)
    public Vector2 spawnAreaMax; //������� ������ (����� ����)
    public int maxCoins = 10; //������������ ���������� ����� �� ����� 

    public List<GameObject> coinsActive = new List<GameObject>();//������ ���� ����� ������� ������� (�� ��� �� ������ �����)

    void Start()
    {
        InvokeRepeating("TrySpawnCoin", 0f, spawnTime); //������� ����� ������� ������� �������
    }

    void TrySpawnCoin()
    {
        if (coinsActive.Count < 10)
        {
            Vector2 spawnPos = new Vector2(Random.Range(spawnAreaMin.x, spawnAreaMax.x), Random.Range(spawnAreaMin.y, spawnAreaMax.y));
            GameObject newCoin = Instantiate(coinPrefab, spawnPos, Quaternion.identity);
            coinsActive.Add(newCoin); //��������� ����� �������� ������
        } 
    }

    public void RemoveCoin(GameObject coin) //���������� ����� ������ ���������
    {
        coinsActive.Remove(coin); // ������ ������ ������� �����
    }
}
