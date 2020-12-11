using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOverMenu : MonoBehaviour
{
	// Reset all stats
	
    // Start is called before the first frame update
    public void Restart ()
    {
    	GameController.Health = 25;
    	GameController.MaxHealth = 25;
    	GameController.PlayerDamage = 1.0f;
        GameController.Level = 1;
    	//GameController.startingTime = 300.0f;
    	PlayerMovement.collectedAmount = 0;
    	SceneManager.LoadScene("BasementMain");
    }
}
