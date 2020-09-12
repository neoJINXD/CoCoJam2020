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
    public Transform frontTop;
    public Transform frontBottom;
    public Transform backBottom;

    public Transform shootingLocation;
    public GameObject bullet;
    private int shootTimer = 0;
    public int ROF;


    public GameObject sword;
    public Animator swish;


    public float dashSpeed;
    public float dashDuration;
    public float initialTime;
    public int dir;
    public bool isDashing = false;



    public bool facingRight = true;
    private float movement = 0;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashDuration = initialTime;
    }

    void Update() {
        RaycastHit2D hitFrontBottom = Physics2D.Raycast(frontBottom.transform.position, -Vector2.up, 0.005f);
        RaycastHit2D hitBackBottom = Physics2D.Raycast(backBottom.transform.position, -Vector2.up, 0.005f);

        RaycastHit2D hitTopFront;
        RaycastHit2D hitBottomFront;

        if (facingRight) {
            hitTopFront = Physics2D.Raycast(frontTop.transform.position, Vector2.right, 0.05f);
            hitBottomFront = Physics2D.Raycast(frontBottom.transform.position, Vector2.right, 0.05f);
        } else {
            hitTopFront = Physics2D.Raycast(frontTop.transform.position, Vector2.left, 0.05f);
            hitBottomFront = Physics2D.Raycast(frontBottom.transform.position, Vector2.left, 0.05f);
        }

        if ((hitFrontBottom.collider || hitBackBottom.collider) || ((hitTopFront.collider || hitBottomFront.collider) && movement != 0))
        {
            jump = 0; 
        }

        if (jump < jumpAmount && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, Vector2.up.y * jumpForce);
            jump++;
            print("yes");
        }

        if (Input.GetMouseButtonUp(1) && shootTimer > ROF)
        {
            print("pewpew");
            
            GameObject bul = Instantiate(bullet, shootingLocation.position, shootingLocation.rotation);
            Bullet property = bul.GetComponent<Bullet>();
            property.isRight = facingRight;
            shootTimer = 0;
        }
        shootTimer++;

        if (Input.GetMouseButtonUp(0))
        {
            // print("brrrrrrr");
            if (facingRight)
                sword.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 60.0f);
            else
                sword.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 300.0f);
            sword.SetActive(true);
            swish.Play("SwordSwish");
        }

        if (dashDuration != 0 && Input.GetKeyDown(KeyCode.LeftShift))
        {
            isDashing = true;
        }

        if (dashDuration < 0)
        {
            isDashing = false;
        }

    }

    void FixedUpdate()
    {

        movement = Input.GetAxisRaw("Horizontal");
        
        dir = facingRight ? 1 : -1;
        
        

        float dash = 0f;

        if (isDashing)
        {
            dashDuration -= Time.deltaTime;
            
            dash = dir * dashSpeed;
            print("speed");
        }
        else
        {
            isDashing = false;
            dashDuration = initialTime;
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
        
        // print(movement);
        rb.velocity = new Vector2(dash + movement * speed * Time.deltaTime, rb.velocity.y);
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
