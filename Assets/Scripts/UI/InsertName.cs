using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InsertName : MonoBehaviour {
	public TMP_Text UserInsertedText;
	public TMP_Text ConfirmationText;

	string playerName;

	public void askForConfirmation() {
		ConfirmationText.text = "Are you sure you want to be called " + UserInsertedText.text + "?";

		playerName = UserInsertedText.text; //I temporarly save the name choosen;
	}


	public void setPlayerName (){
		PlayerPrefs.SetString ("PlayerName", playerName.ToUpper());
	}
}
