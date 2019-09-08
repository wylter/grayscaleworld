using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButtonIcon : MonoBehaviour {
	public string levelName;
	public Sprite LevelIcon;

	void Start(){
		if (PlayerPrefs.GetInt (levelName) == 1) {
			Image img = GetComponent<Image> ();
			img.sprite = LevelIcon;
		}
	}
}
