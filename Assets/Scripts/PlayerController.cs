using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private readonly float thrust = 14000.0f;
    private readonly float movementSpeed = 5.0f;

    private void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");

        transform.Translate(transform.right * horizontalMovement * movementSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && OnGround())
        {
            rb2d.AddForce(transform.up * thrust);
        }
    }

    private bool OnGround()
    {
        float offset = 0.37f;

        Vector2 leftRayPos = new Vector2(transform.position.x + offset, transform.position.y);
        Vector2 rightRayPos = new Vector2(transform.position.x - offset, transform.position.y);

        //Debug.DrawRay(leftRayPos, -Vector3.up);
        //Debug.DrawRay(rightRayPos, -Vector3.up);

        return Physics2D.Raycast(leftRayPos, -Vector3.up, 1.2f) || Physics2D.Raycast(rightRayPos, -Vector3.up, 1.2f);
    }
}
