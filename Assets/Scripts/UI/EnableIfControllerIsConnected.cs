using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableIfControllerIsConnected : MonoBehaviour {

    // Enables if theres a controller connected
    void OnEnable() {

        if (Input.GetJoystickNames().Length == 0 || Input.GetJoystickNames()[0].Equals("")) {
            GetComponent<Image>().enabled = false;
        }
        else {
            GetComponent<Image>().enabled = true;
        }
    }

}