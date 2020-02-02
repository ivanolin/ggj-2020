using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenWipeItem : MonoBehaviour
{
    RectTransform rectTransform;

    Vector2 originalPosition;
    float originalRotation;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();

        originalPosition = rectTransform.anchoredPosition;
        originalRotation = rectTransform.eulerAngles.z;

        GetComponent<Image>().enabled = true;
    }


    public void LeaveScreen(float time) {
        StartCoroutine(Leave(time));
    }

    public void CoverScreen(float time) {
        StartCoroutine(Cover(time));
    }

    IEnumerator Leave(float time) {
        Vector2 newPosition = new Vector2(originalPosition.x + Random.Range(-50, 50), 800);
        float newRotation = Random.Range(0, 360);
        
        float timer = time;

        while (timer > 0) {
            rectTransform.anchoredPosition = Vector2.Lerp(newPosition, originalPosition, Mathf.Pow(timer / time, 2));
            rectTransform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(newRotation, originalRotation, Mathf.Pow(timer / time, 2)));

            yield return 0;
            timer -= Time.deltaTime;
        }
    }


    IEnumerator Cover(float time) {
        Vector2 newPosition = rectTransform.anchoredPosition;
        float newRotation = rectTransform.eulerAngles.z;
        
        float timer = time;

        while (timer > 0) {
            rectTransform.anchoredPosition = Vector2.Lerp(originalPosition, newPosition, Mathf.Pow(timer / time, 2));
            rectTransform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(originalRotation, newRotation, Mathf.Pow(timer / time, 2)));

            yield return 0;
            timer -= Time.deltaTime;
        }

        rectTransform.anchoredPosition = originalPosition;
        rectTransform.rotation = Quaternion.Euler(0, 0, originalRotation);
    }
}
