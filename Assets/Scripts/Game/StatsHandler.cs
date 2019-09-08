using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class StatsHandler : MonoBehaviour {

    [SerializeField]
    private int maxScores;

    public static StatsHandler statsHandler;

    // Inizialization
    void Start() {
        if (statsHandler == null) {
            statsHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<StatsHandler>(); ;
        }
    }

    //Imposta il nome del giocatore
    public void setPlayerName(string playerName) {
        PlayerPrefs.SetString("PlayerName", playerName.ToUpper());
    }

    //Ottiene il nome del giocatore 
    public string getPlayerName() {
        return PlayerPrefs.GetString("PlayerName", "NONAME");
    }

    //Returns the name of the current scene
    public string getLevelName() {
        return SceneManager.GetActiveScene().name;
    }

    //Save Score for the current level
    public void saveScoreOnLevel(string playerName, float scorePoints) {
        //We assign the name of the level
        string levelName = SceneManager.GetActiveScene().name;
        saveScoreOnLevel(playerName, scorePoints, levelName);
    }

    //Save score for any level
    public void saveScoreOnLevel(string playerName, float scorePoints, string levelName) {

        string scoreSavesString = PlayerPrefs.GetString("Score" + levelName, "NOTFOUND");

        //we create the queue
        SortedList<float, string> highScoreQueue;

        //If we found something, we deserialize the scores
        if (scoreSavesString.Equals("NOTFOUND")) {
            //if we didnt have a score yet, We create the queue for it
            highScoreQueue = new SortedList<float, string>();
            Debug.Log("First Entry of score for the level");
        }
        else {
            //Deserialize of the high score
            highScoreQueue = JsonConvert.DeserializeObject<SortedList<float, string>> (scoreSavesString);
        }

        //highScoreQueue.Capacity = maxScores;

        int playerIndex;
        //We check if we already have an entry of the player. If we dont, we add it with his points.
        if ((playerIndex = highScoreQueue.IndexOfValue(playerName)) == -1){
            highScoreQueue.Add(scorePoints, playerName);
        }
        //If we already have an entry, we choose which entry to mantain dependentats on which is lower
        else {
            if (scorePoints < highScoreQueue.Keys[playerIndex]) {
                highScoreQueue.RemoveAt(playerIndex);
                highScoreQueue.Add(scorePoints, playerName);
            }
        }
        //If we have more than maxScores scores, we delete them.

        if (highScoreQueue.Count > maxScores) {
            highScoreQueue.RemoveAt(maxScores);
        }

        //We save the updated queue
        PlayerPrefs.SetString("Score" + levelName, JsonConvert.SerializeObject(highScoreQueue));
    }

    //Get score of the current level
    public string getScoreOnLevel() {
        //We assign the name of the level
        string levelName = SceneManager.GetActiveScene().name;
        return getScoreOnLevel(levelName);
    }

    //Get score of any choosen level
    public string getScoreOnLevel(string levelName) {
        //Retrive the serialized Score from the PlayerPrefs saves
        string scoreSavesString = PlayerPrefs.GetString("Score" + levelName, "NOTFOUND");
        //If we found something, we deserialize the scores
        if (!scoreSavesString.Equals("NOTFOUND")){
            //Deserialize of the high score
            SortedList<float, string> highScoreQueue = JsonConvert.DeserializeObject<SortedList<float, string>>(scoreSavesString);
            //We transform the queue in a string that makes sense
            string scoreString = "";
            for (int i=0; i < highScoreQueue.Count; i++) {
                scoreString += string.Format("{0,10}: {1:000.00}\n", highScoreQueue.Values[i], highScoreQueue.Keys[i]);
            }
            //We return the high score as a string
            return scoreString;
        }
        //If we dont find anything, we return that we still dont have an high score
        return "NO HIGH SCORE YET";
    }

    public static void setTimeAchievement()
    {
        string levelName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetInt(levelName, 2);
    }

}
