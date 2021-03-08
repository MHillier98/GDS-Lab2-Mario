using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    bool isMoving = false;
    Rigidbody2D rb;
    float speed = 8f;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0f);
    }

    void FixedUpdate()
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
            if (isMoving == false)
            {
                Destroy(gameObject);
            }
            else
            {
                rb.velocity = new Vector2(0f, 0f);
                collision.attachedRigidbody.velocity = new Vector2(0f, 5f);
                isMoving = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Mario" && isMoving == true)
        {
            Destroy(collision.gameObject);
        }
    }
}
