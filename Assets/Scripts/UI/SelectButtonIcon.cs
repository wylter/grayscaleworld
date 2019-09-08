using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButtonIcon : MonoBehaviour {
	public string levelName;
	public Sprite LevelIcon;
    public GameObject achievementIcon;

	void Start(){
        int levelFlag = PlayerPrefs.GetInt(levelName);

        if (levelFlag >= 1) {
			Image img = GetComponent<Image> ();
			img.sprite = LevelIcon;
        }

        achievementIcon.SetActive(levelFlag >= 2);
    }
}
