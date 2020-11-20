﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PowerUpMenuController : MonoBehaviour
{
	public Text treasureText; 

	void Update()
	{
		treasureText.text = "Treasure Collected: " + PlayerMovement.collectedAmount;
	}

	
    // Start is called before the first frame update
    public void NextLevel ()
    {
    	SceneManager.LoadScene("BasementMain");
    }

    public void PowerUp1 ()
    {
    	if (PlayerMovement.collectedAmount >= 50)
    	{
    		PlayerMovement.collectedAmount -= 50;
    		GameController.MaxHealth += 25;
    	}
    }

    public void PowerUp2 ()
    {
    	if (PlayerMovement.collectedAmount >= 25)
    	{
	    	PlayerMovement.collectedAmount -= 25;
	    	GameController.Health += 10;
	    }
    }

    public void PowerUp3 ()
    {
    	if (PlayerMovement.collectedAmount >= 75)
    	{
	    	PlayerMovement.collectedAmount -= 75;
            GameController.PlayerDamage += 0.5f;
	    }
    }

}
