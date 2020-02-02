using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapIndicator : MonoBehaviour
{
    public static ScrapIndicator Instance;

    public Sprite[] sprites;

    SpriteRenderer spriteRenderer;

    public Animator anim;

    private void Awake()
    {
        Instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public void UpdateScrap(int amt) {
        anim.speed = .1f;
        anim.SetBool("Show", true);
        spriteRenderer.sprite = sprites[amt];
    }

}
