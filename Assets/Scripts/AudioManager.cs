using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attach this component to a persistent game object
public class AudioManager : MonoBehaviour
{
    private float currentIntensity;
    public float desiredIntensity;


    public float intensityFadeSpeed;


    AudioSource sfxAudio;

    public SongController currentSong;


    private void Start() {
        sfxAudio = GetComponent<AudioSource>();
    }


    void Update() {
        // move current intensity towards desired intensity
        if (Mathf.Abs(desiredIntensity - currentIntensity) < intensityFadeSpeed * Time.deltaTime) {
            currentIntensity = desiredIntensity;
        } else if (currentIntensity < desiredIntensity) {
            currentIntensity += intensityFadeSpeed * Time.deltaTime;
        } else {
            currentIntensity -= intensityFadeSpeed * Time.deltaTime;
        }

        // adjust music volume to match the intensity
        currentSong.UpdateIntensity(currentIntensity);
    }

    void ChangeIntensity(float intensity) {
        desiredIntensity = currentIntensity;
    }

    

    public void PlaySoundEffect(AudioClip sound) {
        sfxAudio.PlayOneShot(sound);
    }
}
