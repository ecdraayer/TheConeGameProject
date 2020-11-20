using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	public GameObject hitEffect;

    void OnCollisionEnter2D(Collision2D collision)
    {
    	if(collision.gameObject.tag == "Enemy")
    	{
    		collision.gameObject.GetComponent<EnemyController>().Damaged();
    	}
    	GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
    	Destroy(effect, 0.4f);
    	Destroy(gameObject);
    }
}
