using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool HittingWall;
    SpriteRenderer Sprite;

    private float Speed = 2.0f;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        Sprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (HittingWall)
        {
            rb.velocity = new Vector2(-Speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(Speed, rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pipes")
        {
            HittingWall = true;
        }
    }
}
