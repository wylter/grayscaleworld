using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour {

    //Used to store the resolution choises of the pc where the game is being played
    private Resolution[] resolutions;

    //Reference to the resolutionDropdown
    [SerializeField]
    private TMP_Dropdown resolutionDropdown;

    //Reference to the Fullscreen Toggle
    [SerializeField]
    private Toggle fullscreenToggle;

    //Reference to the volume slider
    [SerializeField]
    private Slider volumeSlider;

    //AudioClip of the Menu
    [SerializeField]
    private AudioClip menuMusic;

    private void Start() {
        initResolutions();
        initFullscreenToggle();
        initVolumeSlider();
    }

    private void initResolutions() {
        //Getting of the resolutions
        resolutions = Screen.resolutions;

        //Initialization of the Resolutions Dropdown
        resolutionDropdown.ClearOptions();

        //we need to do some magic to put it as a list of strings
        List<string> options = new List<string>();
        int currentResolution = 0;
        for (int i = 0; i < resolutions.Length; i++) {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            //Looks for the current resolution to set as default value
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) {
                currentResolution = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        //Sets the default resolution and refresh the dropdown
        resolutionDropdown.value = currentResolution;
        resolutionDropdown.RefreshShownValue();
    }

    private void initFullscreenToggle() {
        fullscreenToggle.isOn = Screen.fullScreen;
    }

    private void initVolumeSlider() {
        volumeSlider.value = SoundManager.soundManager.GetVolume();
    }

    //Quits the game
    public void QuitGame() {
        Application.Quit();
    }

    //Change the volume
    public void SetVolume (float volume) {
        SoundManager.soundManager.SetVolume(volume);
    }
    
    //Change the quality of the Graphics
    public void SetQuality (int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //Sets the game to fullscreen
    public void SetFullscreen (bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }

    //Sets the game resolution
    public void SetResolution (int resolutionIndex) {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
    }
}
