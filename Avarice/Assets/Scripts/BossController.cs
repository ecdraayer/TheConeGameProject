using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BossController : MonoBehaviour
{

	GameObject player;

	public float range;
	public float speed;
    public float attackingRange;
    public float health;
    private bool coolDownAttack1 = false;
    public float coolDown1;
    public float coolDown2;
    private bool coolDownAttack2 = false;
    public int attackingDamage;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject bulletPrefab2;
    public AudioSource laserSound1;
    public AudioSource laserSound2;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        Attack1();
        Attack2();
    }


    void Attack1()
    {
        if(!coolDownAttack1)
        {
            laserSound1.Play();
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Bullet>().GetPlayer(player.transform);
            bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
            bullet.GetComponent<Bullet>().isEnemyBullet = true;
            bullet.GetComponent<Bullet>().bossBulletType = 1;
            StartCoroutine(CoolDown1());
        }
    }

    void Attack2()
    {
        if(!coolDownAttack2)
        {
            laserSound2.Play();
            GameObject bullet = Instantiate(bulletPrefab2, firePoint.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Bullet>().GetPlayer(player.transform);
            bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
            bullet.GetComponent<Bullet>().isEnemyBullet = true;
            bullet.GetComponent<Bullet>().bossBulletType = 2;
            StartCoroutine(CoolDown2());
        }
    }

 

    private IEnumerator CoolDown1()
    {
        coolDownAttack1 = true;
        yield return new WaitForSeconds(coolDown1);
        coolDownAttack1 = false;
    }

    private IEnumerator CoolDown2()
    {
        coolDownAttack2 = true;
        yield return new WaitForSeconds(coolDown2);
        coolDownAttack2 = false;
    }


    

    public void Damaged()
    {
        health = health - GameController.PlayerDamage;
        if(health <= 0){
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");

            // Transition to end game
            
        }
    }
}

