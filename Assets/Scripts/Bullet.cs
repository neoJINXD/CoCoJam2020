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
    public bool isRight;

    public GameObject Manager;

    void Start()
    {
        Manager = GameObject.Find("GameManager");
        Invoke("killme", life); // calls the killme function after life time has expired
    }

    void FixedUpdate()
    {
        // shoots a ray to detect collision with a collider
        RaycastHit2D hit;
        if (isRight)
            hit = Physics2D.Raycast(transform.position, transform.right, raycastDistance);
        else
            hit = Physics2D.Raycast(transform.position, -transform.right, raycastDistance);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy"))
            {   
                GameObject enemy = hit.collider.gameObject;

                enemy.GetComponent<Enemy>().TakeDmg(Manager.GetComponent<StateManager>().rangeDmg);

            }
            killme();
        }
        // Debug.DrawRay(transform.position, transform.right*0.5f, Color.red);
        // moves the bullet
        if (isRight)
            transform.Translate(transform.right * speed * Time.deltaTime);
        else
            transform.Translate(-transform.right * speed * Time.deltaTime);
    }

    private void killme()
    {
        Destroy(gameObject);
    }
}
