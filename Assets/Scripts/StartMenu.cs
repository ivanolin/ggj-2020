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
        Managers.AudioManager?.SwitchToMain(false);
        screenWipe.WipeScreen();
        Managers.Instance.LoadSceneWithDelay(1, screenWipe.wipeTime + 0.5f);
    }

    public void ToggleCredits()
    {
        credsOn = !credsOn;
        creditsGameObject.SetActive(credsOn);
    }

    public void Quit()
    {
        screenWipe.WipeScreen();
        Managers.Instance.QuitWithDelay(screenWipe.wipeTime + 0.5f);
    }


    IEnumerator LoadSceneWithDelay(int buildIndex, float delay) {
        yield return new WaitForSeconds(delay);

        if (buildIndex == -1) {
            SceneManager.LoadScene(buildIndex);
        }
    }
}
