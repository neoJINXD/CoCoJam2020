using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float speed = 100;
    public int damage = 1;
    public bool idle = true;
    public float detectionRadius = 10;
    private Rigidbody2D rb;
    private int xDirection = 1;
    private int yDirection = 1;
    public bool facingLeft = true;

    public GameObject player = null;
    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Physics2D.OverlapCircle(this.transform.position, detectionRadius);

        // Vector2 targetDistance = Vector2.Distance(target.transform.position, rb.position);
        // Move towards player

        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);

        if (playerCollider) {
            player = playerCollider.gameObject;
            idle = false;
        } else {
            idle = true;
        }
        
        
    }   

    void FixedUpdate()
    {
        if (idle || !player)
        {
            rb.velocity = Vector2.zero;
            print("Birb is vibin");
            return;
        }

        Vector3 scale = transform.localScale;
        //If player is left of enemy, move enemy left towards player
        if (player.transform.position.x < rb.position.x)
        {
            xDirection = -1;
            facingLeft = true;
        } else {
            xDirection = 1;
            facingLeft = false;
        }
        
        //Moves up or down depending where player is on y axis
        if (player.transform.position.y < rb.position.y)
        {
            yDirection = -1;
        } else {
            yDirection = 1;
        }

        //if (target.transform.position.y < )

        rb.velocity = new Vector2(xDirection * speed * Time.deltaTime, yDirection * speed * Time.deltaTime);

        
        
    }

    void LateUpdate()
    {

        Vector3 scale = transform.localScale;
        if ((!facingLeft && scale.x > 0)|| (facingLeft && scale.x < 0))
        {
            scale.x *= -1;
        } 
        transform.localScale = scale;
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            //damages player
            
            other.gameObject.GetComponent<PlayerController>().Damage(damage);

            //knockback
            Vector2 knockDir = transform.position - other.transform.position;
            StartCoroutine(
                other.gameObject.GetComponent<PlayerController>().Knockback(0.05f, 5.0f, knockDir.x)
            );
        }
    }

    void OnDestroy() 
    {
        player.GetComponent<PlayerController>().Manager.GetComponent<StateManager>().EnemyKilled();    
    }
}
