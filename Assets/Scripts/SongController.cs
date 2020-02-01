using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongController : MonoBehaviour
{
    public static float maxVolume = 0.7f;


    AudioSource[] audioSources;


    private void Start() {
        audioSources = GetComponents<AudioSource>();    
    }


    public void UpdateIntensity(float intensity) {
        for (int i = 0; i < audioSources.Length; i++) {
            AudioSource audioSource = audioSources[i];

            if (Mathf.Abs(i - intensity) >= 1) {
                audioSource.volume = 0;
            } else {
                audioSource.volume = maxVolume * (1 - (i - intensity) * (i - intensity));
            }

        }
    }
}
