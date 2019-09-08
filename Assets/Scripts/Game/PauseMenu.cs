using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    [SerializeField]
    private GameObject ui;

    //True if the game is paused
    private bool isPaused = false;

    //On click pause button, pauses the game
    private void Update() {

        //If the Pause buttons i pressed, the game goes on.
        if (Input.GetButtonDown("Pause") && GameController.gameController.getCanPause()) {
            Toogle();
        }

        //Jump makes the game contines, Run makes the game restart.
        if (isPaused) {

            if (Input.GetButtonDown("Retry")) {
                Retry();
            }

            if (Input.GetButtonDown("Continue")) {
                Toogle();
            }
        }
    }

    //When clicking the pause button, stops the game and toogles the menu (or vice versa if you were on the pause menu already)
    public void Toogle() {

        isPaused = !isPaused;

        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf) {
            Time.timeScale = 0;
            SoundManager.soundManager.SetVolume(Mathf.Clamp(SoundManager.soundManager.GetVolume() - 10, -80, 0)); //Halves the volume
        }
        else {
            Time.timeScale = 1;
            SoundManager.soundManager.SetVolume(Mathf.Clamp(SoundManager.soundManager.GetVolume() + 10, -80, 0)); //doubles again the volume
        }
    }

    //Restarts level
    public void Retry() {
        Toogle();
        GameController.gameController.ReloadLevel();
    }

    //Go back to main menu
    public void GoToMenu() {
        Toogle();
        GameController.gameController.loadLevel("Menu");
    }
}
