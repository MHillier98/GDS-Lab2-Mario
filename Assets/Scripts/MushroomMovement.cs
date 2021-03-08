using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool HittingWall;
    SpriteRenderer Sprite;

    private float Speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        //rb = gameObject.GetComponent<Rigidbody2D>();
        rb = this.GetComponent<Rigidbody2D>();
        Sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //HittingWall = Physics2D.OverlapCircle(WallCheck.position, WallCheckRadius, WallLayer);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!HittingWall)
            rb.velocity = new Vector2(Speed, rb.velocity.y);
        if (HittingWall)
            rb.velocity = new Vector2(-Speed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Pipes")
        {
            HittingWall = true;
        }
    }
}
