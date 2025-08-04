using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PlayerController : MonoBehaviour
{
    public float speedPlayer = 7f; //скорость игрока
    private Rigidbody2D rb; //отвечает за физику объекта

    private Vector2 lastNormalPosition; //последняя нормальная позицая - до выхода за границу

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastNormalPosition = transform.position;
    }
    
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");//A/D, ←/→
        float moveY = Input.GetAxis("Vertical");//W/S, ↑/↓

        rb.velocity = new Vector2(moveX * speedPlayer, moveY * speedPlayer);//перемещение
    }

    void OnTriggerExit2D(Collider2D other) //при входе в триггер
    {
        if (other.gameObject.name == "Boundaries")
        {
            Vector2 clampedPosition = new Vector2(
                Mathf.Clamp(transform.position.x, -other.bounds.extents.x, other.bounds.extents.x),
                Mathf.Clamp(transform.position.y, -other.bounds.extents.y, other.bounds.extents.y));
            transform.position = lastNormalPosition; //возврат в последнее нормальное положение
            rb.velocity = Vector2.zero; //сброс скорости
        }
    }

    void OnTriggerStay2D(Collider2D other) //внутри границ
    {
        if (other.gameObject.name == "Boundaries")
        {
            lastNormalPosition = transform.position;
        }
    }

}
