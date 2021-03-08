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
    // Start is called before the first frame update
    void Start()
    {
        Anim = gameObject.GetComponent<Animator>();
        Coin = true;
        //hitCount = 13;
        BlockHit = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mushroom)
        {
            Coin = false;
        }

        /*if(hitCount<=0 && Coin)
        {
            spriteRenderer.sprite = disabledSprite;
        }
        else if (hitCount == 12 && !Coin)
        {
            spriteRenderer.sprite = disabledSprite;
        }*/

        if(Coin && BlockHit)
        {
            //spriteRenderer.sprite = disabledSprite;
            //gameObject.GetComponentInParent<Animator>().SetBool("BlockHit", true);
            Anim.SetBool("Disabled", true);
            //gameObject.GetComponentInParent<Animator>();
            //Coin anim + block going up
        }
        if (Mushroom && BlockHit)
        {
            
            //Vector2 CurrentPos = gameObject.transform.position;
            //Vector2 MushroomPos = new Vector2(CurrentPos.x, CurrentPos.y);
            //MushroomPos.y = CurrentPos.y + 3;
            //GameObject Mush = Instantiate(MushroomPrefab, MushroomPos, Quaternion.identity) as GameObject;
            Anim.SetBool("Disabled", true);
            //Instantiate mushroom into scene
            //block anim going up
        }
    }
}
