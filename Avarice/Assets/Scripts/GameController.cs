using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

	public static GameController instance;
    // Start is called before the first frame update
    private static int health = 25;
    private static int maxHealth = 25;
    private static float moveSpeed = 5f;
    private static float fireRate = 0.5f;
    private static float playerDamage = 1.0f;
    

    public static int Health{get => health; set => health=value;}
    public static int MaxHealth{get => maxHealth; set => maxHealth=value;}
    public static float MoveSpeed{get => moveSpeed; set => moveSpeed=value;}
    public static float FireRate{get => fireRate; set => fireRate=value;}
    public static float PlayerDamage{get => playerDamage; set => playerDamage=value;}

    public Text healthText;
    public HealthBar healthBar;
    public Text countDownText;

    public float currentTime = 0f;
    public float startingTime = 300f;

    private void Awake()
    {
    	if(instance = null)
    	{
    		instance = this;
    	}
    }
    void Start()
    {

        healthBar.SetMaxHealth(maxHealth);
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetMaxHealth(maxHealth);
        healthText.text = health+" / "+maxHealth; 
        healthBar.SetHealth(health);
        currentTime -= 1 * Time.deltaTime;
        countDownText.text = currentTime.ToString("0");
        if( currentTime <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    

    public static void DamagePlayer(int damage)
    {
    	health -= damage;

        
    	if(health <= 0)
    	{
    		KillPlayer();
    	}
    }

    public static void HealPlayer(int healAmount)
    {
    	health = Mathf.Min(maxHealth, health+healAmount);
    }

    private static void KillPlayer()
    {
        SceneManager.LoadScene("GameOver");
    }
}
