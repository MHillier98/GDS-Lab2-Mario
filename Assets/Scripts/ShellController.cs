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
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity != new Vector2(0f, 0f))
        {
            isMoving = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.tag == "Mario" && isMoving == true)
        {
            rb.velocity = new Vector2(0f, 0f);
            collision.attachedRigidbody.AddForce(Vector2.up * 5f);
            isMoving = false;
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
