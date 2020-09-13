using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StateManager : MonoBehaviour
{

    private static StateManager instance;

    //Game properties

    public int maxHealth = 100;
    public int health = 100;
    // private int weapon; // current weapon
    public bool hasRange;

    public TextMeshProUGUI healthText;

    // private Dictionary<string, bool> abilities; // boolean flags for current available abilities player is allowed
    // private int level;
    
    void Awake() 
    {
        if (StateManager.instance != null && instance != this)
        {   
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    void Start()
    {
        healthText.text = health.ToString();
    }

   public void EnemyKilled()
   {
       maxHealth++;
   }

   public void PlayerTookDamage(int amount)
   {
       health -= amount;
   }

    // public Dictionary<string, bool> GetAbilities()
    // {
    //     return abilities;
    // }

    // public bool PlayerCan(string ability)
    // {
    //     return this.abilities[ability];
    // }

    public void handleDeathReset()
    {
        // Reset State, convert max health to points to use for shop
        
        // 
    }

    
}
