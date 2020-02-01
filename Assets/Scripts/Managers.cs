using UnityEngine;
using UnityEngine.SceneManagement;

// a singleton component that provides global access to the game's managers (eg scene manager, audio manager, etc)
public class Managers : MonoBehaviour
{
    static Managers instance;

    private AudioManager audioManager;
    public static AudioManager AudioManager { get { return instance?.audioManager; } }

    // add more managers here

    private void Awake()
    {
        // very basic singleton pattern

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
