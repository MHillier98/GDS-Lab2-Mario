using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    public bool isMoving = false;
    public Rigidbody2D rb;

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
            collision.gameObject.GetComponent<PlayerController>().MarioDead = true;
        }
    }
}
