using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossPortalController : MonoBehaviour
{
    //public GameObject portalCollider;
    public string sceneToLoad;
    private GameObject player;

    private void Start()
    {
    	player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D other){
    	
    	if(other.CompareTag("Player") && !other.isTrigger)
    	{
	    	SceneManager.LoadScene("BossBattle");
    	}
    }
}
