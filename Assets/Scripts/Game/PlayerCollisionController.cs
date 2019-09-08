using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour {

    private Collider2D myCollider;

    private void Start() {
        myCollider = GetComponents<Collider2D>()[0];
    }

    //Kill Player if Overlaps
    private void OnTriggerEnter2D(Collider2D collision) {

        int layerCol = collision.gameObject.layer;

        //TODO: Fix this solution, it's only momentanery
        if (layerCol == 8 || layerCol == 9 || layerCol == 10){
            GameController.gameController.KillPlayer(true);
            Debug.Log("Died becouse collision with" + collision.gameObject.name);
        }
    }


    private void OnTriggerExit2D(Collider2D collision) {

        //If the player goes out of the restrictios, it gets killed
        if (collision.tag == "Restriction") {
            float distance = myCollider.Distance(collision).distance;
            if (distance > 0) {
                GameController.gameController.KillPlayer(false);
                Debug.Log("Killed for going out of bounds");
            }
        }
    }
}
