using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ChangeColor : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float timeToChange = 0.2f;
    private float timeSinceChange = 0f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        timeSinceChange += Time.deltaTime;
        if(spriteRenderer != null && timeSinceChange >= timeToChange)
        {
            Color newColor = new Color(
                Random.Range(0.5f, 1.0f),
                Random.Range(0.5f, 1.0f),
                Random.Range(0.5f, 1.0f)
                );

            spriteRenderer.color = newColor;
            timeSinceChange = 0f;
        }
    }
}
