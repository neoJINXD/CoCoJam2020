using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{

    public GameObject target;
    public float speed = 100;
    public int damage = 1;
    public bool idle = true;
    public float detectionRadius = 10;
    private Rigidbody2D rb;
    private int direction = 1;
    public bool facingLeft = true;

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
        
    }   

    void FixedUpdate()
    {
        if (idle)
        {
            return;
        }

        Vector3 scale = transform.localScale;
        //If player is left of enemy, move enemy left towards player
        if (target.transform.position.x < rb.position.x)
        {
            direction = -1;
            facingLeft = true;
        } else {
            direction = 1;
            facingLeft = false;
        }

        rb.velocity = new Vector2(direction * speed * Time.deltaTime, rb.velocity.y);
        
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
            
            target.GetComponent<PlayerController>().Damage(damage);

            //knockback
            Vector2 knockDir = transform.position - target.transform.position;
            StartCoroutine(
                target.GetComponent<PlayerController>().Knockback(0.05f, 5.0f, knockDir.x)
            );
        }
    }




    
    

    
}
