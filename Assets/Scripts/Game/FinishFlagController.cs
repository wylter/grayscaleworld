using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishFlagController : MonoBehaviour {
    //Particle system to play at end level
    [Space]
    [SerializeField]
    private GameObject finishLevelParticlesBlack;
    [SerializeField]
    private GameObject finishLevelParticlesWhite;

    private bool gameIsOver = false;

    //If it detects the player inside, the level is complete
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player" && !gameIsOver) {

            gameIsOver = true;

            Debug.Log("Level Complete");
            
            //Spawn particles
            GameObject fLPBClone = Instantiate(finishLevelParticlesBlack, transform.position, transform.rotation);
            GameObject fLPWClone = Instantiate(finishLevelParticlesWhite, transform.position, transform.rotation);
            Destroy(fLPBClone, 2f);
            Destroy(fLPWClone, 2f);

            //Calls Gameover Courutine from the Gamecontroller
            StartCoroutine(GameController.gameController.GameOver());

            //Makes the Finish Object disappear
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
