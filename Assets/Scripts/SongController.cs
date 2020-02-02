using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SongController : MonoBehaviour
{
    public static float maxVolume = 0.7f;

    private float currentMaxVolume;


    List<AudioSource> audioSources;


    public void Init() {
        currentMaxVolume = 0;
        audioSources = GetComponents<AudioSource>().ToList();    
    }


    public void UpdateIntensity(float intensity) {
        if (audioSources.Count > 1) {
            for (int i = 0; i < audioSources.Count; i++) {
                AudioSource audioSource = audioSources[i];

                if (Mathf.Abs(i - intensity) >= 1) {
                    audioSource.volume = 0;
                } else {
                    Debug.Log(audioSource.clip + " " + currentMaxVolume);
                    audioSource.volume = currentMaxVolume * (1 - (i - intensity) * (i - intensity));
                }

            }
        } else {
            // ignore intensity
            audioSources[0].volume = currentMaxVolume;
        }
    }

    public void TurnOff(float time) {
        StartCoroutine(FadeOut(time));
    }

    public void TurnOn() {
        currentMaxVolume = maxVolume;
        audioSources.ForEach(audioSource => audioSource.Play());
    }


    IEnumerator FadeOut(float time) {
        float timer = time;

        while (timer > 0) {
            currentMaxVolume = maxVolume * timer / time;
            yield return 0;
            timer -= Time.deltaTime;
        }

        currentMaxVolume = 0;
        audioSources.ForEach(audioSource => audioSource.Stop());
    }
}
