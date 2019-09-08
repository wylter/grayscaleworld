using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class VictoryMenu : MonoBehaviour {

    //Ui Menu
    [SerializeField]
    private GameObject ui;
    [Space]
    [SerializeField]
    private TextMeshProUGUI victoryText;
    [Space]
    [SerializeField]
    private GameObject continueButton;
    //Times texts
    [Header("Time Texts")]
    [SerializeField]
    private TextMeshProUGUI yourTimeText;
    [SerializeField]
    private TextMeshProUGUI timesText;

    //True if the game is in gameover screen.
    private bool isGameOverScreen;

    //On click pause button, pauses the game
    private void Update() {

        //Jump makes the game contines, Run makes the game restart.
        if (isGameOverScreen) {

            if (Input.GetButtonDown("Retry")) {
                GetComponent<PauseMenu>().Retry();
            }

            if (Input.GetButtonDown("Continue")) {
                nextLevel();
            }
        }
    }

    //Toogles the victory menu
    public void Toogle(float timer) {

        ui.SetActive(!ui.activeSelf);

        isGameOverScreen = !isGameOverScreen;

        continueButton.SetActive(GameController.gameController.IsNextLevel());

        if (ui.activeSelf) {
            Time.timeScale = 0;
            SoundManager.soundManager.SetVolume(Mathf.Clamp(SoundManager.soundManager.GetVolume() - 10, -80, 0)); //Halves the volume

            victoryText.text = StatsHandler.statsHandler.getLevelName() + " Complete";
            writeTimes(timer);
        }
        else {
            Time.timeScale = 1;
            SoundManager.soundManager.SetVolume(Mathf.Clamp(SoundManager.soundManager.GetVolume() + 10, -80, 0)); //doubles again the volume
        }

        
    }

    //write the time of the player
    private void writeTimes(float timer) {
 
        string timerText;

        if (timer < 10) {
            timerText = string.Format("{0:00.00}", timer);
        }
        else {
            timerText = string.Format("{0:.00}", timer);
        }

        yourTimeText.text = "YOUR TIME: " + timerText;

        StatsHandler.statsHandler.saveScoreOnLevel(StatsHandler.statsHandler.getPlayerName(), timer);

        timesText.text = StatsHandler.statsHandler.getScoreOnLevel();
    }

    public void nextLevel() {
        if (GameController.gameController.IsNextLevel()) {
            Toogle(0);
            GameController.gameController.LoadNextLevel();
        }
        else {
            Debug.Log("No more levels");
        }
	}

    //Restarts level
    public void Retry() {
        Toogle(0);
        GameController.gameController.ReloadLevel();
    }

    //Go back to main menu
    public void GoToMenu() {
        Toogle(0);
        GameController.gameController.loadLevel("Menu");
    }
}
