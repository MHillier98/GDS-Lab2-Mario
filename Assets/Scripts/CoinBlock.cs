using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlock : MonoBehaviour
{
    public int hitCount;
    public Sprite disabledSprite;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        hitCount = 13;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hitCount<=0){
            spriteRenderer.sprite = disabledSprite;
        }
    }
}
