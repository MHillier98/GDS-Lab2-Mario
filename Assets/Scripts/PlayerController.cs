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
    public bool isGrounded = false, MushroomPickup = false, MarioDead = false, IsBig = false, IsHit = false;
    private int coinCount;

    //public bool BigMario = false;
    public Text coinCountText;

    //private bool goingDown = false;
    public Animator goingDownPipe;

    public GameObject MushroomPrefab;
    public GameObject CoinPrefab;

    //public GameObject ParentObject;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Sprite = GetComponent<SpriteRenderer>();
        
        coinCount=0;
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

        if (MushroomPickup && !IsBig)
        {
            animator.SetBool("MushroomGet", true);
            //MushroomPickup = false;
            StartCoroutine(MushroomAnim());
        }

        if (MarioDead)
        {
            StartCoroutine(BeginReset());
            //ResetLevel();
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
            //collider.gameObject.GetComponentInParent<Animator>().SetBool("BlockHit", true);
            //if(collider.gameObject.GetComponentInChildren<CoinBlock>().Coin)
            if(collider.gameObject.GetComponentInChildren<CoinBlock>().Coin)
                coinCount++;
            //CoinBlock hitBlock = collider.GetComponent<CoinBlock>();
            //if(hitBlock.hitCount>0){
            //   coinCount++;
            //    coinCountText.text = "x"+coinCount;
            //    hitBlock.hitCount--;
            // }
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
            if (IsBig)
            {
                //PlayAnimation
                Destroy(collider.gameObject, 0.01f);
            }
            if(!IsBig)
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
                    coinCountText.text = "x" + coinCount;
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
        }
        if(collider.tag == "Coin")
        {
            coinCount++;
            coinCountText.text = "x"+coinCount;
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
            if (!IsBig && !IsHit)
            {
                //Debug.Log("Colliding");
                animator.SetBool("MarioDead", true);
                Vector2 StartPos = transform.position;
                Vector2 EndPos = transform.position;
                EndPos.y = EndPos.y + 1;
                StartCoroutine(MarioDeath(StartPos, EndPos, 1.0f));

                //StartCoroutine(MarioDeath());
                //ParentObject.GetComponentInParent<Animator>().SetBool("IsDead", true);
                //Animator ParentAnim = gameObject.GetComponentInParent<Animator>();
                //ParentAnim.SetBool("IsDead", true);
                //Begin death animation
                //MarioDead = true;
                //rb.
                //ParentObject.GetComponent<Animator>().SetBool("IsDead", true);
            }
            else if (IsBig)
            {
                IsBig = false;
                animator.SetBool("MarioHit", true);
                IsHit = true;
                StartCoroutine(MarioAnim());
            }
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
        //BigMario = true;
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
        animator.SetBool("IsBig", false);
        animator.SetBool("MarioHit", false);
        IsHit = false;
    }

    IEnumerator ResetCoin(Animator Anim, GameObject CoinObject)
    {
        yield return new WaitForSeconds(0.3f);
        Anim.SetBool("BlockHit", false);
        Destroy(CoinObject);
    }

    IEnumerator MarioDeath(Vector2 StartPos, Vector2 EndPos, float Duration)
    {
        float Timer = 0;
        //Vector2 StartPosition = transform.position;
        while (Timer < Duration)
        {
            transform.position = Vector2.Lerp(StartPos, EndPos, Timer / Duration);
            Timer += Time.deltaTime;
            yield return null;
        }
        transform.position = EndPos;
        DeathDown();
    }

    private void DeathDown()
    {
        Vector2 CurrentPos = transform.position;
        Vector2 EndPos = transform.position;
        EndPos.y = EndPos.y - 5;
        StartCoroutine(DeathDownAnim(CurrentPos, EndPos, 3.0f));
    }

    IEnumerator DeathDownAnim(Vector2 Start, Vector2 End, float Duration)
    {
        float Timer = 0;
        while(Timer < Duration)
        {
            transform.position = Vector2.Lerp(Start, End, Timer / Duration);
            Timer += Time.deltaTime;
            yield return null;
        }
        transform.position = End;
        MarioDead = true;
    }

    IEnumerator BeginReset()
    {
        yield return new WaitForSeconds(2.0f);
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
