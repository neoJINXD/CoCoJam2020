using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dASHmOVE : MonoBehaviour
{
    public Rigidbody2D rb;
    public float dashSpeed;
    public float dashDuration;
    public float initialTime;
    public int dir;
    public bool isDashing = false;

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        dashDuration = initialTime;
    }

    void Update()
    {
        dir = gameObject.GetComponent<PlayerController>().facingRight ? 1 : -1;
        
        if (dashDuration > 0 && Input.GetKeyDown(KeyCode.LeftShift))
        {
            isDashing = true;
        }

        if (dashDuration < 0)
        {
            isDashing = false;
        }

        if (isDashing)
        {
            dashDuration -= Time.deltaTime;
            rb.velocity = new Vector2(rb.velocity.x + dir * dashSpeed, rb.velocity.y);
            print("speed");
        }
        else
        {
            isDashing = false;
            dashDuration = initialTime;
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
            // dir = 0;
        }
        print(rb.velocity.x);
    }
    



}
