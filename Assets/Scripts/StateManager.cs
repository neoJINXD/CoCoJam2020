using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{

    private static StateManager instance;

    public GameObject player;

    public GameObject deathScreen;
    public GameObject shopMenu;

    //Game properties

    public int maxHealth = 100;
    public int health = 100;
    // private int weapon; // current weapon
    public bool hasRange;
    public bool hasDash;
    public int numJumps;

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
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        health = maxHealth;
        healthText.text = health.ToString();
    }

    void Update() 
    {
        healthText.text = health.ToString();
        if (health <= 0)
            Death();
    }

   public void EnemyKilled()
   {
       maxHealth++;
   }

   public void PlayerTookDamage(int amount)
   {
       health -= amount;
   }

    public void Death()
    {
        // Reset State, convert max health to points to use for shop
        player.GetComponent<PlayerController>().Die();
        DeathScreenEnable();
    }

    public void DeathScreenEnable()
    {
        deathScreen.SetActive(true);

    }
    public void NoDeathScreen()
    {
        deathScreen.SetActive(false);

    }

    public void ResetHealth()
    {
        health = maxHealth;
    }

    public void BuyJump()
    {
        numJumps++;
        health -= 50;
    }
    public void BuyRange()
    {
        hasRange = true;
        health -= 70;
    }
    public void BuyDash()
    {
        hasDash = true;
        health -= 30;
    }
    public void Play()
    {
        //TODO
        maxHealth = 100;
        shopMenu.SetActive(false);
        SceneManager.LoadScene("World");
    }

    public void ResetStats()
    {
        hasDash = false;
        hasRange = false;
        numJumps = 1;
    }
    
}
