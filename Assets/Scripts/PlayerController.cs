using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    private float horizontalMovement;
    private float speed = 7f;
    private float jumpForce = 200f;
    public bool isGrounded = false;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector2.up * jumpForce * 120f);
            }
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        horizontalMovement = Input.GetAxis("Horizontal");

        rb.AddForce(new Vector2(horizontalMovement * speed * 7f, rb.velocity.y));
    }

    public void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Block")
        {
            isGrounded = true;
        }
    }
}
