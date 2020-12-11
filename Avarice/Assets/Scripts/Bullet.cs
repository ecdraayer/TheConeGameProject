using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public bool isEnemyBullet = false;

	public GameObject hitEffect;

    private Vector2 lastPos;
    private Vector2 currPos;
    private Vector2 playerPos;

    public int bossBulletType = 0;

    void Start()
    {
    }

    void Update()
    {
        if(isEnemyBullet)
        {
            currPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPos, 10f * Time.deltaTime);
            if(currPos == lastPos)
            {
                Destroy(gameObject);
            }
            lastPos = currPos;
        }
    }

    public void GetPlayer(Transform player)
    {
        playerPos = player.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Bullet")
        {
        	if(collision.gameObject.tag == "Enemy" && !isEnemyBullet)
        	{
        		collision.gameObject.GetComponent<EnemyController>().Damaged();

        	}
            else if(collision.gameObject.tag == "Player" && isEnemyBullet)
            {
                if (bossBulletType == 1)
                {
                    GameController.DamagePlayer(150); 
                } 
                else if (bossBulletType == 2)
                {
                    GameController.DamagePlayer(50); 
                }
                
               
            }
            else if(collision.gameObject.tag == "Boss" && !isEnemyBullet)
            {
                collision.gameObject.GetComponent<BossController>().Damaged();
            }
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.4f);
            Destroy(gameObject);
        }

    	
    }
}
