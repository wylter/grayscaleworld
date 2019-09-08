using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {
	public GameObject FirstStartPanel;


	public void SetUp() {		//Sets up the game if first start of the game
		if (PlayerPrefs.GetInt ("FirstStart") != 1) {	//Checks if the game as ever been started (if it was never started FirstStart will return null)
			PlayerPrefs.SetInt ("FirstStart", 1);	
			PlayerPrefs.SetInt ("Tutorial01", 1);
			PlayerPrefs.SetInt ("Level01", 1);

			FirstStartPanel.SetActive (true);
			PlayerPrefs.SetInt ("Test_Scene", 1);	//Debug only
			this.gameObject.SetActive (false);
		} else {
			this.gameObject.SetActive (true);
		}	
	}


	// Use this for initialization
	public void SelectLevel(string levelName) {
        if (PlayerPrefs.GetInt(levelName) >= 1)
            GameController.gameController.loadLevel(levelName);
	}
}
