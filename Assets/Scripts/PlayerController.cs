using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PlayerController : MonoBehaviour
{
    public float speedPlayer = 7f; //скорость игрока
    private Rigidbody2D rb; //отвечает за физику объекта
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");//A/D, ←/→
        float moveY = Input.GetAxis("Vertical");//W/S, ↑/↓

        rb.velocity = new Vector2(moveX * speedPlayer, moveY * speedPlayer);//задает скорость
    }
}
