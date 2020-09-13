using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    
    public GameObject bloodEff;
    // public ParticleSystem blood;


    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    public void TakeDmg(int dmg)
    {
        if (bloodEff)
            Instantiate(bloodEff, transform.position, Quaternion.identity);

        health -= dmg;
    }

    

}
