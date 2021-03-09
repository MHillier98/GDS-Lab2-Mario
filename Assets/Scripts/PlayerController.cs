using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    private SpriteRenderer Sprite;

    private readonly float speed = 5f;
    private readonly float jumpForce = 220f;

    private float horizontalMovement;
    public bool isGrounded = false, MushroomPickup = false, MarioDead = false, IsBig = false;
    private static int coinCount;

    public bool BigMario = false;
    public Text coinCountText;

    //private bool goingDown = false;
    public Animator goingDownPipe;

    public GameObject MushroomPrefab;
    public GameObject CoinPrefab;

    public Text Score, Lives;
    public static int lifeRemaining = 3;
    public int scoreCount;


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
                Jump();
            }
            isGrounded = false;
        }

        if (MushroomPickup && !BigMario)
        {
            animator.SetBool("MushroomGet", true);
            //MushroomPickup = false;
            StartCoroutine(MushroomAnim());
        }

        if (MarioDead)
        {
            StartCoroutine(BeginReset());
            
        }
        coinCountText.text = coinCount.ToString();
        Lives.text = lifeRemaining.ToString();
        Score.text = scoreCount.ToString();
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

    private void DeathScreen()
    {
        if (lifeRemaining == 0)
        {
            lifeRemaining = 3;
            coinCount = 0;

        }
    } 

    private void GroundCheck()
    {
        float offsetX = 0.37f;
        float offsetY = IsBig ? -0.5f : 0.0f;
        
        Vector2 leftRayPos = new Vector2(transform.position.x + offsetX, transform.position.y + offsetY);
        Vector2 rightRayPos = new Vector2(transform.position.x - offsetX, transform.position.y + offsetY);

        bool onGround = Physics2D.Raycast(leftRayPos, -Vector3.up, 0.8f) || Physics2D.Raycast(rightRayPos, -Vector3.up, 0.8f);
        animator.SetBool("IsGrounded", onGround);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
/*        if (collider.tag == "CoinBlock")
        {
            collider.gameObject.GetComponentInChildren<CoinBlock>().BlockHit = true;
            collider.gameObject.GetComponentInParent<Animator>().SetBool("BlockHit", true);
            collider.gameObject.GetComponentInParent<Animator>().SetBool("BlockHit", true);
            if(collider.gameObject.GetComponentInChildren<CoinBlock>().Coin)
            if(collider.gameObject.GetComponentInChildren<CoinBlock>().Coin)
                coinCount++;
            CoinBlock hitBlock = collider.GetComponent<CoinBlock>();
            if(hitBlock.hitCount>0){
               coinCount++;
                coinCountText.text = "x"+coinCount;
                hitBlock.hitCount--;
             }
        }*/
    }

    public void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Block" || collider.tag == "Pipes")
        {
            isGrounded = true;
        }
        if (collider.tag == "BreakableBlock")
        {
            if (BigMario)
            {
                //PlayAnimation
                Destroy(collider.gameObject, 0.01f);
               
            }
            if(!BigMario)
            {
                Animator Anim = collider.gameObject.GetComponentInParent<Animator>();
                collider.gameObject.GetComponentInParent<Animator>().SetBool("BlockHit", true);
                StartCoroutine(ResetBlock(Anim));
            }
        }
        if(collider.tag == "CoinBlock")
        {
            if (collider.gameObject.GetComponentInChildren<CoinBlock>().BlockHit == false)
            {
                collider.gameObject.GetComponentInChildren<CoinBlock>().BlockHit = true;
                collider.gameObject.GetComponent<Animator>().SetBool("BlockHit", true);
                Animator Anim = collider.gameObject.GetComponentInParent<Animator>();
                Vector2 CurrentPos = collider.gameObject.transform.position;
                Vector2 SpawnPos = new Vector2(CurrentPos.x, CurrentPos.y);
                SpawnPos.y = CurrentPos.y + 1;
                SpawnPos.x = CurrentPos.x - 0.5f;

                if (collider.gameObject.GetComponentInChildren<CoinBlock>().Coin)
                {
                    GameObject Coin = Instantiate(CoinPrefab, SpawnPos, Quaternion.identity) as GameObject;
                    StartCoroutine(ResetCoin(Anim, Coin));
                    coinCount++;
                    scoreCount += 200;
                }
                else if (collider.gameObject.GetComponentInChildren<CoinBlock>().Mushroom)
                {
                    StartCoroutine(ResetBlock(Anim));
                    GameObject Mushroom = Instantiate(MushroomPrefab, SpawnPos, Quaternion.identity) as GameObject;
                }
            }
        }
        if (collider.tag == "Pickup")
        {
            MushroomPickup = true;
            //animator.SetBool("MushroomGet", false);
            //animator.SetBool("IsBig", MushroomPickup);
            Destroy(collider.gameObject);
            scoreCount += 1000;
        }
        if(collider.tag == "Coin")
        {
            coinCount++;
            Destroy(collider.gameObject);
        }
        if (collider.tag == "OutOfBounds")
        {
            MarioDead = true;
            //Debug.Log("Player dead");
        }

        //Goomba Kill Collision
        if (collider.tag == "EnemyHead")
        {
            Gumber gumba = collider.gameObject.GetComponentInParent<Gumber>();
            gumba.stomped = true;
            Jump();
        }

        if(collider.tag == "Goomba")
        {
            if (!BigMario)
            {
                //Begin death animation
                //MarioDead = true;
            }
            else if (BigMario)
            {
                BigMario = false;
                animator.SetBool("MarioHit", true);
                StartCoroutine(MarioAnim());
                //Anim of mario turning small
                scoreCount += 200;
            }
            Debug.Log("Player hit");
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
        animator.SetBool("MushroomGet", false);
        MushroomPickup = false;
        BigMario = true;
        IsBig = true;
        animator.SetBool("MushroomGet", false);
        animator.SetBool("IsBig", true);
    }

    IEnumerator ResetBlock(Animator Anim)
    {
        yield return new WaitForSeconds(0.3f);
        Anim.SetBool("BlockHit", false);
    }

    IEnumerator MarioAnim()
    {
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("MarioHit", false);
    }

    IEnumerator ResetCoin(Animator Anim, GameObject CoinObject)
    {
        yield return new WaitForSeconds(0.3f);
        Anim.SetBool("BlockHit", false);
        Destroy(CoinObject);
    }

    public IEnumerator BeginReset()
    {
        yield return new WaitForSeconds(2.0f);
        lifeRemaining -= 1;
        ResetLevel();
    }

    public void ResetLevel()
    {
        //life - 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Coroutine for the going down pipe 
    private IEnumerator PipeDown()
    {
        Debug.Log("Going Down Pipe");
        yield return new WaitForSeconds(1f);
        //goingDown = false;
        Debug.Log("Loading Underground");
        //SceneManager.LoadScene(Underground);
    }
}
