using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private SpawnManager spawnManager;
    void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("������������ ������ ������� � �������");
        if (other.CompareTag("Player")) //���� ����������� � �������
        {
            Debug.Log("������� �������!");
            spawnManager.RemoveCoin(other.gameObject);
            Destroy(gameObject);//������� ������
        }
        
    }
}
