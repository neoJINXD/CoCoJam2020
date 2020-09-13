using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{

    private static StateManager instance;
    public AudioManager audioManager;
    public GameObject player;

    public GameObject deathScreen;
    public GameObject shopMenu;

    public GameObject helpMenu;
    public GameObject helpBut;

    //Game properties

    public int maxHealth = 100;
    public int health = 100;
    // private int weapon; // current weapon
    public bool hasRange;
    public bool hasDash;
    public int numJumps;

    public int rangeDmg = 2;

    public int meleeDmg = 2;

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

        audioManager = AudioManager.instance;
        audioManager.PlaySound("Shop");
        if (audioManager = null)
        {
            Debug.LogError("Could not find audio manager for scene");
        }

        health = maxHealth;
        healthText.text = health.ToString();
    }

    void Update() 
    {
        player = GameObject.Find("Player");
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

        if (audioManager == null)
        {
            Debug.LogWarning("Audio Manager not found");
        }

        maxHealth = 100;
        health = health > 100 ? 100 : health;
        shopMenu.SetActive(false);
        helpMenu.SetActive(false);
        helpBut.SetActive(false);
        audioManager.PlaySound("Battle");
        SceneManager.LoadScene("World");
    }

    public void HelpMe()
    {
        helpMenu.SetActive(!helpMenu.activeSelf);
    }

    public void ResetStats()
    {
        hasDash = false;
        hasRange = false;
        numJumps = 1;
        rangeDmg = 2;
        meleeDmg = 2;
    }
    
    public void UpgradeRange()
    {
        health -= 50;
        rangeDmg += 5;
    }
    public void UpgradeMelee()
    {
        health -= 50;
        meleeDmg += 5;
    }
}
