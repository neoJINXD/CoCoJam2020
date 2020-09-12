using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    private static StateManager instance;

    //Game properties

    private int maxHealth;
    private int health;
    private int weapon; // current weapon
    private Dictionary<string, bool> abilities; // boolean flags for current available abilities player is allowed
    private int level;
    
    void Awake() 
    {
        if (StateManager.instance != null && instance != this)
        {   
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Dictionary<string, bool> GetAbilities()
    {
        return abilities;
    }

    public bool PlayerCan(string ability)
    {
        return this.abilities[ability];
    }

    public void handleDeathReset()
    {
        // Reset State, convert max health to points to use for shop
        
        // 
    }

    
}
