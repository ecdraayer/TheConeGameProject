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
    	SceneManager.LoadScene("BasementMain");
    }
}
