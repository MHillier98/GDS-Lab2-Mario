using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gumber : MonoBehaviour
{

    public float moveSpeed;
    public bool moveRight;
    public bool stomped;

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    private bool hittingWall;


    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer Sprite;

    public Sprite deadGoomba;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Sprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);
        if (hittingWall)
        {
            moveRight = !moveRight;
        }

        if (moveRight)
        {
            transform.localScale = new Vector3(-5f, 5f, 5f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            transform.localScale = new Vector3(5f, 5f, 5f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }


        //Death
        if (stomped)
        {
            this.rb.velocity = Vector3.zero;
            this.gameObject.GetComponent<Animator>().enabled = false;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = deadGoomba;
            StartCoroutine(StompedGoomba());
        }
    }

    //Coroutine for the goomba death
    private IEnumerator StompedGoomba()
    {
        yield return new WaitForSeconds(.3f);
        Destroy(this.gameObject);
    }
}
