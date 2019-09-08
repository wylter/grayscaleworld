using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayerOnContact : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Player") {
            GameController.gameController.KillPlayer(true);
        }
        else {
            Debug.Log(collision.ToString());
        }
    }
}
