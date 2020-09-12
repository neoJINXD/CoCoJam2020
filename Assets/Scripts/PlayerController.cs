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

    public bool facingRight = true;
    private float movement = 0;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            rb.velocity = Vector2.up * jumpForce;
            jump++;
            print("yes");
        }

        if (Input.GetMouseButtonUp(1) && shootTimer > ROF)
        {
            print("pewpew");
            Instantiate(bullet, shootingLocation.position, shootingLocation.rotation);
            shootTimer = 0;
        }
        shootTimer++;
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
