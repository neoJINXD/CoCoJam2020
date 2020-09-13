using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;

        
    public float jumpForce = 10;
    private int jump = 0;
    public int jumpAmount = 1;

    //Raycasts
    private Rigidbody2D rb;
    public Transform frontTop;
    public Transform frontBottom;
    public Transform backBottom;

    //Shooting
    public Transform shootingLocation;
    public GameObject bullet;
    private int shootTimer = 0;
    public int ROF;

    //Melee
    public GameObject sword;
    public Animator swish;

    public float atkTimer;
    public float startAtkTimer;
    public Transform hiltPos;
    public LayerMask enemyLayer;
    public float atkRange;

    //Dash
    public float dashSpeed;
    private float dashDuration;
    public float initialTime;
    private int dir;
    public bool isDashing = false;


    private bool allowMove = true;


    public bool facingRight = true;
    private float movement = 0;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashDuration = initialTime;
    }

    void Update() {
        //raycast collision detection
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


        //jumping
        if (jump < jumpAmount && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, Vector2.up.y * jumpForce);
            jump++;
            // print("yes");
        }



        //shootting
        if (Input.GetMouseButtonUp(1) && shootTimer > ROF)
        {
            // print("pewpew");
            
            GameObject bul = Instantiate(bullet, shootingLocation.position, shootingLocation.rotation);
            Bullet property = bul.GetComponent<Bullet>();
            property.isRight = facingRight;
            shootTimer = 0;
        }
        shootTimer++;

        //melee animation and hit detection
        if (Input.GetMouseButtonUp(0))
        {
            // print("brrrrrrr");
            if (facingRight)
                sword.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 60.0f);
            else
                sword.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 300.0f);
            sword.SetActive(true);
            swish.Play("SwordSwish");

            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(hiltPos.position, atkRange, enemyLayer);

            foreach (Collider2D enemy in enemiesHit)
            {
                enemy.GetComponent<Enemy>().TakeDmg();
                // print("I hit" + enemy.name);
            }

        }

        

        //dashing
        if (dashDuration != 0 && Input.GetKeyDown(KeyCode.LeftShift))
        {
            isDashing = true;
        }

        if (dashDuration < 0)
        {
            isDashing = false;
        }

    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hiltPos.position, atkRange);    
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
            // print("speed");
        }
        else
        {
            isDashing = false;
            dashDuration = initialTime;
            // rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
        
        // print(movement);
        if (allowMove)
            rb.velocity = new Vector2(dash + movement * speed * Time.deltaTime, rb.velocity.y);

        if (!facingRight && movement > 0)
            Flip();
        else if (facingRight && movement < 0)
            Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void Damage(int dmg)
    {
        print("You Took Dmg!");
        //TODO remove health

        //flash sprite red
        gameObject.GetComponent<Animation>().Play("Player_Damage");
    }

    public void Die()
    {
        print(":c");
        gameObject.SetActive(false);

        //TODO death screen
    }

    public IEnumerator Knockback(float duration, float power, float hDir)
    {
        float timer = 0;
        allowMove = false;
        while (duration > timer)
        {
            timer += Time.deltaTime;
            // rb.AddForce(new Vector2(dir.x * 500, dir.y * power));
            // print(hDir*-100);
            rb.velocity = new Vector2(hDir * -100 * power, Vector2.up.y * power);
        }
        allowMove = true;
        yield return 0;
    }


}
