using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject creditsGameObject;
    public ScreenWipeCanvas screenWipe;
    private bool credsOn;

    void Awake()
    {
        //creditsGameObject.SetActive(false);
    }

    void Update()
    {

    }

    public void Play()
    {
        Managers.AudioManager?.SwitchToMain();
        screenWipe.WipeScreen();
        StartCoroutine(LoadSceneWithDelay(1, screenWipe.wipeTime + 0.5f));
    }

    public void ToggleCredits()
    {
        credsOn = !credsOn;
        creditsGameObject.SetActive(credsOn);
    }

    public void Quit()
    {
        Application.Quit();
    }


    IEnumerator LoadSceneWithDelay(int buildIndex, float delay) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(buildIndex);
    }
}
