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

    public Gumber goomba;
    public float bounceOnEnemy;

    private static bool goingDown = false;
    public Animator goingDownPipe;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Sprite = GetComponent<SpriteRenderer>();

        //Referencing Goomba
        GameObject g = GameObject.FindGameObjectWithTag("Goomba");
        goomba = g.GetComponent<Gumber>();

    }

    private void Update()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            isGrounded = false;
        }

        if (MushroomPickup)
        {
            animator.SetBool("MushroomGet", true);
            BigMario = true;
            StartCoroutine(MushroomAnim());
        }

        if (MarioDead)
        {
            ResetLevel();
        }

        GroundCheck();
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce * 120f);
    }

    private void FixedUpdate()
    {
        if (!MushroomPickup)
        {
            horizontalMovement = Input.GetAxis("Horizontal");
            Vector2 MovementDir = new Vector2(horizontalMovement * speed * 7f, rb.velocity.y);

            rb.AddForce(MovementDir);

            if (MovementDir.x > 0)
            {
                Sprite.flipX = false;
            }
            else if (MovementDir.x < 0)
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
        else if (collider.tag == "Pickup")
        {
            MushroomPickup = true;
            Destroy(collider.gameObject);
        }
        else if (collider.tag == "OutOfBounds")
        {
            MarioDead = true;
            //Debug.Log("Player dead");
        }

        //Goomba Kill Collision
        if (collider.tag == "EnemyHead")
        {
            goomba.stomped = true;
            //Debug.Log("Goomba Dead");
            Jump();
        }

        /*
        //Going Down Pipe
        if (collider.tag == "PipeDown" && Input.GetKeyDown('S'))
        {
            goingDown = true;
            StartCoroutine(PipeDown());
            
        }
        */
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Shell" && collision.collider is BoxCollider2D)
        {
            collision.rigidbody.velocity = new Vector2(-10f, 0f);
        }
        else if (collision.collider.tag == "Shell" && collision.collider is CircleCollider2D)
        {
            collision.rigidbody.velocity = new Vector2(10f, 0f);
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

    //Coroutine for the going down pipe 
    private IEnumerator PipeDown()
    {
        Debug.Log("Going Down Pipe");
        yield return new WaitForSeconds(1f);
        goingDown = false;
        Debug.Log("Loading Underground");
        //SceneManager.LoadScene(Underground);
    }
}
