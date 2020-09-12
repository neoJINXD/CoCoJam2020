using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // attach this script to a bullet prefab

    public float speed = 5f;
    // seconds before the prefab gets destroyed
    public float life = 10f;

    public float raycastDistance = 0.5f;

    void Start()
    {
        Invoke("killme", life); // calls the killme function after life time has expired
    }

    void FixedUpdate()
    {
        // shoots a ray to detect collision with a collider
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, raycastDistance);
        if (hit.collider != null)
        {
            // if (hit.collider.CompareTag("Enemy"))
            // {
                // print("pew");

                //TODO should damage the enemy that gets hit
            // }
            killme();
        }

        // moves the bullet
        transform.Translate(transform.right * speed * Time.deltaTime);
    }

    private void killme()
    {
        Destroy(gameObject);
    }
}
