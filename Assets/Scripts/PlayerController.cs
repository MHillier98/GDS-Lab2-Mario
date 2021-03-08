using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator animator;

    private SpriteRenderer Sprite;

    private readonly float speed = 5f;
    private readonly float jumpForce = 220f;

    private float horizontalMovement;
    private bool isGrounded = false, MushroomPickup = false, MarioDead = false;

    public bool BigMario = false;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Sprite = GetComponent<SpriteRenderer>();
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
        if(MushroomPickup)
        {
            animator.SetBool("MushroomGet", true);
            BigMario = true;
            StartCoroutine(MushroomAnim());
        }
        if(MarioDead)
        {
            ResetLevel();
        }

        GroundCheck();
    }

    private void FixedUpdate()
    {
        if (!MushroomPickup)
        {
            horizontalMovement = Input.GetAxis("Horizontal");
            Vector2 MovementDir = new Vector2(horizontalMovement * speed * 7f, rb.velocity.y);

            rb.AddForce(MovementDir);

            if(MovementDir.x > 0)
            {
                Sprite.flipX = false;
            }
            if(MovementDir.x < 0)
            {
                Sprite.flipX = true;
            }

            bool isRunning = horizontalMovement != 0 ? true : false;
            animator.SetBool("IsRunning", isRunning);
        }
    }

    private void GroundCheck()
    {
        float offset = 0.37f;
        Vector2 leftRayPos = new Vector2(transform.position.x + offset, transform.position.y);
        Vector2 rightRayPos = new Vector2(transform.position.x - offset, transform.position.y);

        bool onGround = Physics2D.Raycast(leftRayPos, -Vector3.up, 0.8f) || Physics2D.Raycast(rightRayPos, -Vector3.up, 0.8f);
        animator.SetBool("IsGrounded", onGround);
    }

    public void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Block")
        {
            isGrounded = true;
        }
        if(collider.tag == "Pickup")
        {
            MushroomPickup = true;
            Destroy(collider.gameObject);
        }
        if (collider.tag == "OutOfBounds")
        {
            MarioDead = true;
            //Debug.Log("Player dead");
        }
    }
private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.collider.tag == "Shell" && collision.collider is BoxCollider2D)
        {
            collision.rigidbody.velocity = new Vector2(-10f, collision.rigidbody.velocity.y);
            
        }
        else if (collision.collider.tag == "Shell" && collision.collider is CircleCollider2D)
        {
            collision.rigidbody.velocity = new Vector2(10f, collision.rigidbody.velocity.y);
        }
      }
    IEnumerator MushroomAnim()
    {
        yield return new WaitForSeconds(1.0f);
        MushroomPickup = false;
    }
   
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
