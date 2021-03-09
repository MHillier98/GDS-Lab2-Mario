using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlock : MonoBehaviour
{
    //public GameObject MushroomPrefab;
    //public int hitCount;
    public bool BlockHit, Coin, Mushroom;

    public Animator Anim;

    public Sprite disabledSprite;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        Anim = gameObject.GetComponent<Animator>();
        Coin = true;
        //hitCount = 13;
        BlockHit = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Mushroom)
        {
            Coin = false;
        }

        //if (hitCount <= 0 && Coin)
        //{
        //    spriteRenderer.sprite = disabledSprite;
        //}
        //else if (hitCount == 12 && !Coin)
        //{
        //    spriteRenderer.sprite = disabledSprite;
        //}

        if (Coin && BlockHit)
        {
            //spriteRenderer.sprite = disabledSprite;
            //gameObject.GetComponentInParent<Animator>().SetBool("BlockHit", true);
            Anim.SetBool("Disabled", true);
        }

        if (Mushroom && BlockHit)
        {
            Anim.SetBool("Disabled", true);
        }
    }
}
