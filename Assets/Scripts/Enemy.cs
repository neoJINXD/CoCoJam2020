using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    
    
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
    public void TakeDmg()
    {
        health--;
    }

}
