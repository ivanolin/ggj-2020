using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SongController : MonoBehaviour
{
    public static float maxVolume = 0.7f;

    private float currentMaxVolume;


    List<AudioSource> audioSources;

    public AudioClip transitionSound;
    public AudioSource transitionSource;

    Coroutine currentFadeRoutine;


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
                    audioSource.volume = currentMaxVolume * (1 - (i - intensity) * (i - intensity));
                }

            }
        } else {
            // ignore intensity
            audioSources[0].volume = currentMaxVolume;
        }
    }

    public void TurnOff(float time) {
        if (currentFadeRoutine != null) {
            StopCoroutine(currentFadeRoutine);
        }

        currentFadeRoutine = StartCoroutine(FadeOut(time));
    }

    public void TurnOn(float fadeInTime, float turnOnDelay) {
        if (currentFadeRoutine != null) {
            StopCoroutine(currentFadeRoutine);
        }

        currentFadeRoutine = StartCoroutine(FadeInWithDelay(fadeInTime, turnOnDelay));
    }


    public void PlayTransitionSound() {
        transitionSource.PlayOneShot(transitionSound);
    }

    IEnumerator FadeInWithDelay(float fadeInTime, float turnOnDelay) {
        yield return new WaitForSeconds(turnOnDelay);
        
        float timer = fadeInTime;
        audioSources.ForEach(audioSource => audioSource.Play());

        while (timer > 0) {
            currentMaxVolume = maxVolume * (1 - timer / timer);
            yield return 0;
            timer -= Time.deltaTime;
        }

        currentMaxVolume = maxVolume;
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
