using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private SpriteRenderer[] sprites; // Массив всех спрайтов слоя

    private float spriteWidth;

    private float overlapPixels = 5; //чтоб не было зазора между спрайтами когда спрайт перемещается после выхода за границу экрана

    void Start()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();//поиск всех спрайтов

        if (sprites == null || sprites.Length == 0)
        {
            Debug.LogError("Не хватает спрайта в " + gameObject.name);
            enabled = false;
            return;
        }

        spriteWidth = sprites[0].sprite.textureRect.width / sprites[0].sprite.pixelsPerUnit;
    }

    void Update()
    {
        foreach (var sprite in sprites) //проход по всем спрайтам
        {
            sprite.transform.Translate(Vector3.left * speed * Time.deltaTime); //сдвиг спрайта

            if (sprite.transform.position.x < -spriteWidth + overlapPixels / 100f) //если вышел за экран
            {
                Vector3 newPos = sprite.transform.position;
                newPos.x += spriteWidth * 2 - overlapPixels / 100f;
                sprite.transform.position = newPos;
            }
        }
    }
}
