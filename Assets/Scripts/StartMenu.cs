using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject creditsGameObject;
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
        SceneManager.LoadScene(1);
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
}
