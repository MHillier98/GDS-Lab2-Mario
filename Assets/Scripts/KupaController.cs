using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KupaController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 2f;
    public GameObject shell;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(-speed, 0f);
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Mario")
        {
            gameObject.SetActive(false);
            collision.attachedRigidbody.velocity = new Vector2(0f, 5f);
            Instantiate(shell, new Vector2(gameObject.transform.position.x, -2.5f), Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Mario")
        {
            Destroy(collision.gameObject);
           
        }
    }
}