using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private GameObject creditsGameObject;

    void Awake()
    {
        creditsGameObject = FindObjectOfType<Credits>().gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (creditsGameObject.activeInHierarchy)
            {
                ToggleCredits(false);
            }
            else
            {
                ToggleCredits(true);
            }
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void ToggleCredits(bool enabled)
    {
        creditsGameObject.SetActive(enabled);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
