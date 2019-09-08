using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class SoundManager : MonoBehaviour {
    [SerializeField]
    private AudioMixer audioMixer;                  //Reference to the audio mixer of the project
    [SerializeField]
    private AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.
    [SerializeField]
    private AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.

    public static SoundManager soundManager = null;     //Allows other scripts to call functions from SoundManager.             
    [SerializeField]
    private float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
    [SerializeField]
    private float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.


    void Awake() {
        if (soundManager == null)
            soundManager = this;
        else if (soundManager != this)
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        //DontDestroyOnLoad(gameObject);
        //Not needed anymore if it's gonna be a child of the game controller
    }

    public void SetVolume (float volume) {
        audioMixer.SetFloat("Volume", volume);
    }

    public float GetVolume() {
        float volume = 0;
        audioMixer.GetFloat("Volume", out volume);
        return volume;
    }


    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip, float volumeMultiplier) {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSource.clip = clip;

        //Set the pitch of the audio source to the randomly chosen pitch.
        efxSource.pitch = Random.Range(lowPitchRange, highPitchRange) * volumeMultiplier;

        //Play the clip.
        efxSource.Play();
    }

    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip) {
        PlaySingle(clip, 1);
    }

    //Changes the songs between levels (Only if the next level has a different music)
    public void ChangeSong(AudioClip clip) {
        if (musicSource.clip != clip) {
            musicSource.clip = clip;

            //Play the clip.
            musicSource.Play();
        }
    }
}