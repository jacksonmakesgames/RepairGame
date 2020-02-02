using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapIndicator : MonoBehaviour
{
    public static ScrapIndicator Instance;

    public Sprite[] sprites;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        Instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateScrap(int amt) {
        spriteRenderer.sprite = sprites[amt];
    }

}
