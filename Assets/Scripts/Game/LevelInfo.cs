using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour {

    public static LevelInfo levelInfo;

    //AudioClip of the Menu
    [Space]
    public AudioClip levelMusic;
    //Name of the next level]
    [Space]
    public string nextLevelName;
    [Space]
    public float recordTime = 0f;

    void Awake() {
        if (levelInfo == null)
            levelInfo = this;
        else if (levelInfo != this)
            Destroy(gameObject);
    }
}