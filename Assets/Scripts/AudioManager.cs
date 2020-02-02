using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attach this component to a persistent game object
public class AudioManager : MonoBehaviour
{
    private float currentIntensity;
    public float desiredIntensity;


    public float intensityFadeSpeed;


    public AudioSource sfxAudio;


    public SongController openingSong;
    public SongController mainSong;


    public SongController currentSong;


    bool inited;

    private void Init() {
        inited = true;

        if (openingSong != null && mainSong != null) {
            openingSong.Init();
            mainSong.Init();

            currentSong = openingSong;
            currentSong.TurnOn(0.1f);
        }
    }




    void Update() {
        if (!inited)
            Init();

        if (openingSong != null && mainSong != null) {
            // move current intensity towards desired intensity
            if (Mathf.Abs(desiredIntensity - currentIntensity) < intensityFadeSpeed * Time.deltaTime) {
                currentIntensity = desiredIntensity;
            } else if (currentIntensity < desiredIntensity) {
                currentIntensity += intensityFadeSpeed * Time.deltaTime;
            } else {
                currentIntensity -= intensityFadeSpeed * Time.deltaTime;
            }

            // adjust music volume to match the intensity
            openingSong.UpdateIntensity(currentIntensity);
            mainSong.UpdateIntensity(currentIntensity);
        }
    }

    public void ChangeIntensity(float intensity) {
        desiredIntensity = intensity;
    }

    

    public void PlaySoundEffect(AudioClip sound) {
        PlaySoundEffect(sound, 1);
    }

    public void PlaySoundEffect(AudioClip sound, float volume) {
        if (sound == null) {
            Debug.LogWarning("This AudioClip does not exist!");
            return;
        }
        
        sfxAudio.PlayOneShot(sound, volume);
    }


    public void SwitchSong(SongController newSong, float fadeOutTime, float turnOnDelay) {
        currentSong.TurnOff(fadeOutTime);
        currentSong = newSong;
        currentSong.TurnOn(turnOnDelay);
    }


    public void SwitchSongToOther() {
        if (currentSong == openingSong) {
            SwitchToMain();
        } else {
            SwitchToOpening();
        }
    }

    public void SwitchToMain() {
        if (currentSong == mainSong) return;

        SwitchSong(mainSong, 1f, 1.4f);
        mainSong.PlayTransitionSound();
    }

    public void SwitchToOpening() {
        if (currentSong == openingSong) return;

        SwitchSong(openingSong, 0f, 2f);
    }
}
