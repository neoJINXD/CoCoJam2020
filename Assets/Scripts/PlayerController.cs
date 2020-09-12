using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;

        
    public float jumpForce = 10;
    public int jump = 0;
    public int jumpAmount = 1;

    private Rigidbody2D rb;
    public Transform feet;
    public Transform front;

    public bool facingRight = true;
    private float movement = 0;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        RaycastHit2D hitDown = Physics2D.Raycast(feet.transform.position, -Vector2.up, 0.005f);
        
        RaycastHit2D hitFront;
        if (facingRight)
            hitFront = Physics2D.Raycast(front.transform.position, Vector2.right, 0.05f);
        else
            hitFront = Physics2D.Raycast(front.transform.position, Vector2.left, 0.05f);

        if (hitDown.collider || (hitFront.collider && movement != 0)) //if grounded
        {
            jump = 0;
        }


        if (jump < jumpAmount && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
            jump++;
            print("yes");
        }


    }

    void FixedUpdate()
    {
        movement = Input.GetAxisRaw("Horizontal");
        // print(movement);
        rb.velocity = new Vector2(movement * speed * Time.deltaTime, rb.velocity.y);
        if (!facingRight && movement > 0)
            Flip();
        else if (facingRight && movement < 0)
            Flip();
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.transform.position.y <= transform.position.y)
            jump = 0;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
