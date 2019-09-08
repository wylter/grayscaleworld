using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour {
	public int tutorialLevels;
	public int levelsNum;

	public void Reset() {
		PlayerPrefs.SetInt ("FirstStart", 0);
		for (int i = 2; i <= tutorialLevels; i++) {
			PlayerPrefs.SetInt (string.Format("Tutorial{0:00}", i) , 0);
		}
		for (int i = 2; i <= levelsNum; i++) {
			PlayerPrefs.SetInt (string.Format("Level{0:00}", i), 0);
		}

		Debug.Log ("Game restarder \t" + PlayerPrefs.GetInt("FirstStart"));

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
