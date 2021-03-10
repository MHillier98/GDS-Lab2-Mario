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
    private bool hittingWall = false;


    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer Sprite;

    public Sprite deadGoomba;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Sprite = GetComponent<SpriteRenderer>();
    }

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
            this.rb.velocity = Vector2.zero;
            this.gameObject.GetComponent<Animator>().enabled = false;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = deadGoomba;
            StartCoroutine(StompedGoomba());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Mario")
        {
            collision.gameObject.GetComponent<PlayerController>().BeginReset();
        }

        /*if (collision.collider.tag == "Pipes")
        {
            hittingWall = true;
            Debug.Log("Collided with pipe");
        }*/
    }



    //Coroutine for the goomba death
    private IEnumerator StompedGoomba()
    {
        yield return new WaitForSeconds(.3f);
        Destroy(this.gameObject);
    }
}
