using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public GameObject player;
    public int dmg = 5;

    void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            

            //damages player
            player.GetComponent<PlayerController>().Damage(dmg);

            //knockback
            Vector2 knockDir = transform.position - player.transform.position;
            StartCoroutine(
                player.GetComponent<PlayerController>().Knockback(0.05f, 5.0f, knockDir.x)
            );
        }
    }
}
