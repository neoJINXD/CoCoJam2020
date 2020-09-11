using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;

        
    public float jumpForce = 0;
    public int jump = 0;
    public int jumpAmount = 1;

    private Rigidbody2D rb;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (jump < jumpAmount && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
            jump++;
            print("yes");
        }

    }

    void FixedUpdate()
    {
        float movement = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movement * speed * Time.deltaTime, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D other) {
        jump = 0;

    }
}
