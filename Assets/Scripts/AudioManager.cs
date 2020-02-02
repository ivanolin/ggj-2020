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


    private void Start() {
        sfxAudio = GetComponent<AudioSource>();

        if (openingSong != null && mainSong != null) {
            openingSong.Init();
            mainSong.Init();

            currentSong = openingSong;
            currentSong.TurnOn();
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
        desiredIntensity = currentIntensity;
    }

    

    public void PlaySoundEffect(AudioClip sound) {
        sfxAudio.PlayOneShot(sound);
    }

    public void PlaySoundEffect(AudioClip sound, float volume) {
        sfxAudio.PlayOneShot(sound, volume);
    }


    public void SwitchSong(SongController newSong, float fadeOutTime) {
        currentSong.TurnOff(fadeOutTime);
        currentSong = newSong;
        currentSong.TurnOn();
    }


    public void SwitchSongToOther() {
        if (currentSong == openingSong) {
            SwitchSong(mainSong, 2f);
        } else {
            SwitchSong(openingSong, 1f);
        }
    }
}
