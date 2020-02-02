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

    public void Init() {
        if (inited) return;
        inited = true;

        if (openingSong != null && mainSong != null) {
            openingSong.Init();
            mainSong.Init();

            currentSong = openingSong;
            currentSong.TurnOn(0f, 0.1f);
        }
    }




    void Update() {
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

    

    public void PlaySoundEffect(AudioClip sound, float volume = 1f, float volumeVariation = 0f) {
        if (sound == null) {
            Debug.LogWarning("There is no AudioClip to play here!");
            return;
        }
        
        sfxAudio.PlayOneShot(sound, volume + Random.Range(-volumeVariation, volumeVariation));
    }


    public void PlayRandomSoundEffect(AudioClip[] sounds, float volume = 1f, float volumeVariation = 0f) {
        if (sounds == null || sounds.Length == 0) {
            Debug.LogWarning("There are no AudioClips to play here!");
            return;
        }

        PlaySoundEffect(sounds[Random.Range(0, sounds.Length)], volume, volumeVariation);
    }


    public void SwitchSong(SongController newSong, float fadeOutTime, float fadeInTime = 0f, float fadeInDelay = 0f) {
        currentSong.TurnOff(fadeOutTime);
        currentSong = newSong;
        currentSong.TurnOn(fadeInTime, fadeInDelay);
    }


    public void SwitchSongToOther() {
        if (currentSong == openingSong) {
            SwitchToMain(false);
        } else {
            SwitchToOpening();
        }
    }

    public void SwitchToMain(bool switchImmediately) {
        if (currentSong == mainSong) return;

        desiredIntensity = 0;
        
        if (switchImmediately) {
            SwitchSong(mainSong, 0f);
        } else {
            SwitchSong(mainSong, 1f, 0f, 1.4f);
            openingSong.PlayTransitionSound();
        }
    }

    public void SwitchToOpening() {
        if (currentSong == openingSong) return;

        SwitchSong(openingSong, 0f, 1f);
    }

    public void EndMain(bool endImmediately) {
        if (currentSong != mainSong) return;

        if (endImmediately) {
            currentSong.TurnOff(0f);
        } else {
            currentSong.TurnOff(1f);
        }
        mainSong.PlayTransitionSound();
    }
}
