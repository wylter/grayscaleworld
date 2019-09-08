using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour {

    [HideInInspector]
    public static GameController gameController;
    [SerializeField]
    private Transform playerTransfrom;
    [Space]
    [Header("GUI")]
    [SerializeField]
    private TextMeshProUGUI timerText;
    [Space]
    //Particle system to play at end level
    [SerializeField]
    private GameObject playerDeathParticles;
    [Space]
    [Header("Audio Settings")]
    [SerializeField]
    private AudioClip deathClip;
    [SerializeField]
    private AudioClip victoryClip;
    [Space]
    [Header("Other Settings")]
    [SerializeField]
    private GameObject myGUI;//Stores the GUI objec
    [SerializeField]
    private GameObject gameplayGUI;//Stores the GUI gameplay object for deactivation
    [SerializeField]
    private GameObject transitionPanel;//Stores the GUI gameplay object for deactivation
    [SerializeField]
    private float transAnimationTime = 1.5f; //Havent found a way to automatise this. Describes the animation time of the transition for now.

    //Allows to not search continusly
    private float nextTimeToSearch;

    //Timer variable and Timer Text reference
    private float timer = 0f;
    //Stops the timer
    private bool stopTimer = true;
    //Allow the pause or not
    private bool canPause = false;
    //True if the player finished the level.
    public bool isGameOver = true;
    //Stores the respowm position
    private Vector3 respownPosition;
    //GUI animator
    private Animator gUIAnimator;
    


    void Awake () {
        if (gameController == null) {
            gameController = this;
        }
        else if (gameController != this) {
            Destroy(gameObject);
        }

        gUIAnimator = myGUI.GetComponent<Animator>();
    }

    private void Start() {
        DontDestroyOnLoad(gameObject);

        //Starts up a level from the editor without having to load it. This is only for debug.
        if (Application.isEditor && SceneManager.GetActiveScene().buildIndex != 0) {

            Debug.Log("In editor play start");

            setUpPlayer();

            //Changes the level music
            SoundManager.soundManager.ChangeSong(LevelInfo.levelInfo.levelMusic);

            //Resets values
            stopTimer = false;
            timer = 0;
            canPause = true;
            isGameOver = false;
        }

        if (SceneManager.GetActiveScene().buildIndex == 0) {
            //Changes the level music
            SoundManager.soundManager.ChangeSong(LevelInfo.levelInfo.levelMusic);

            gameplayGUI.SetActive(false);
        }

    }

    private void Update() {

        if (!stopTimer) {
            UpdateTimer();
        }
    }

    public float getTimer() {
        return timer;
    }

    public bool getCanPause() {
        return canPause;
    }

    public void setCanPause(bool canPause) {
        this.canPause = canPause;
    }

    private void UpdateTimer() {

        timer += Time.deltaTime;

        if (timer < 10) {
            timerText.text = string.Format("{0:00.00}", timer);
        }
        else {
            timerText.text = string.Format("{0:.00}", timer);
        }
    }

    //Kill the player by deactivating it
    public void KillPlayer(bool onScreenDeath) {
        stopTimer = true; //Stops the time when the player dies.

        //Spawn particles
        GameObject particlesClone = Instantiate(playerDeathParticles, playerTransfrom.position, playerTransfrom.rotation);
        if (!onScreenDeath) particlesClone.GetComponent<ParticleSystem>().emission.SetBurst(0, new ParticleSystem.Burst(0, 20)); // If out of screen doubles the death particles. This is done cause you'll see half the particles sinse its a radius. TODO: Make a better death particles system.
        Destroy(particlesClone, 1f);

        //Deactivates the player
        playerTransfrom.gameObject.SetActive(false);

        SoundManager.soundManager.PlaySingle(deathClip);

        StartCoroutine(KillingPlayer());
    }

    //Handle Gameover
    public IEnumerator KillingPlayer() {

        //Waits 1 second before respawning the player
        yield return new WaitForSeconds(1f);

        //Respawns the player
        RespawnPlayer();
    }

    //Put the position of the player at the RespawnPosition again, and Reactivates it
    public void RespawnPlayer() {
        if (!isGameOver) {
            playerTransfrom.position = respownPosition;
            playerTransfrom.gameObject.SetActive(true);

            //Resets the timer to 00.00 and restarts it.
            timer = 0;
            stopTimer = false;

            //Initializate the Player again with the color he had before Dying.
            PlayerController playerController = playerTransfrom.GetComponent<PlayerController>();
            playerController.initColor(false);
        }
    }

    //Handle Gameover
    public IEnumerator GameOver() {

        SetGameOverState();

        SoundManager.soundManager.PlaySingle(victoryClip);

        //Waits tot seconds and restarts the level
        yield return new WaitForSeconds(1.5f);

        //Unlocks next level
        if (LevelInfo.levelInfo.nextLevelName != null) {
            PlayerPrefs.SetInt(LevelInfo.levelInfo.nextLevelName, 1);
        }

        

        //Starts VictoryMenu
        GetComponent<VictoryMenu>().Toogle(timer);

        //TODO: Do a better Finish Level
    }

    //Set's up the player in the position of the placeholder, and sets up the camera target
    private void setUpPlayer() {
        GameObject playerHolder = GameObject.Find("PlayerPositionHolder");

        GameObject player = Instantiate(Resources.Load("Players/" + "Player"), playerHolder.transform.position, playerHolder.transform.rotation) as GameObject;

        playerTransfrom = player.transform;

        respownPosition = playerTransfrom.position;

        Destroy(playerHolder.gameObject);

        GameObject.FindGameObjectWithTag("Virtual Camera").GetComponent<CameraFollow>().setFollow(playerTransfrom);

    }



    //Loads the level, set up the player in the correct position and starts the music of the level
    public void loadLevel(string levelName) {
        StartCoroutine(loadLevelAsync(levelName));
    }

    private void initLevel(bool isPlayableLevel) {

        //Changes the level music
        SoundManager.soundManager.ChangeSong(LevelInfo.levelInfo.levelMusic);

        //Resets the level if not in the menu
        if (isPlayableLevel) {

            setUpPlayer();

            //Resets values
            stopTimer = false;
            timer = 0;
            canPause = true;
            isGameOver = false;

            //sets up the GUI if we are in a playable level
            gameplayGUI.SetActive(true);
        }
        
    }

    //Unables gameplay options
    private void SetGameOverState() {

        //Disable The player Movement
        playerTransfrom.GetComponent<PlayerController>().enabled = false;

        //Stops the timer
        stopTimer = true;
        //Unables pause
        canPause = false;
        //Sets the status gameover.
        isGameOver = true;
        //Disables GUI
        gameplayGUI.SetActive(false);
        
    }

    //load level async.
    IEnumerator loadLevelAsync (string levelName) {

        if (!isGameOver) {
            SetGameOverState();
        }

        transitionPanel.SetActive(true);

        //Start transition animation
        gUIAnimator.SetTrigger("FadeIn");

        //waits for the animation to end before going on
        yield return new WaitForSeconds(transAnimationTime);

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);
        //operation.allowSceneActivation = false;

        while (!operation.isDone) {
            yield return null;
        }

        //Init the level
        initLevel(levelName != "Menu");

        //Starts the entering animation
        gUIAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(transAnimationTime);

        transitionPanel.SetActive(false);

        //allowas the level to start
        //operation.allowSceneActivation = true;

    }

    public void ReloadLevel() {
        StartCoroutine(loadLevelAsync(SceneManager.GetActiveScene().name));
    }

    public bool IsNextLevel() {
        return (LevelInfo.levelInfo.nextLevelName != "NOLEVEL");
    }

    public void LoadNextLevel() {
        StartCoroutine(loadLevelAsync(LevelInfo.levelInfo.nextLevelName));
    }



}
