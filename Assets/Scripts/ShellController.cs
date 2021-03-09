using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    public bool isMoving = false;
    public Rigidbody2D rb;
    public float speed = 8f;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rb.velocity != new Vector2(0f, 0f))
        {
            isMoving = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Mario")
        {
            collision.attachedRigidbody.velocity = new Vector2(collision.attachedRigidbody.velocity.x, 5f);
            rb.velocity = new Vector2(0f, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Mario" && isMoving == true)
        {
            collision.gameObject.GetComponent<PlayerController>().BeginReset();
        }
        if (collision.collider.tag == "Pipes")
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }
}
