using System.Collections;
using UnityEngine;

// a singleton component that provides global access to the game's managers (eg scene manager, audio manager, etc)
public class Managers : MonoBehaviour
{
    static Managers instance;

    public static Managers Instance { get { return instance; }}

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


        audioManager = GetComponent<AudioManager>();
        audioManager.Init();
    }


    public void LoadSceneWithDelay(int buildIndex, float delay) {
        StartCoroutine(Load(buildIndex, delay));
    }

    public void QuitWithDelay(float delay) {
        StartCoroutine(Load(-1, delay));
    }

    IEnumerator Load(int buildIndex, float delay) {
        yield return new WaitForSeconds(delay);

        if (buildIndex < 0) {
            Application.Quit();
        } else {
            UnityEngine.SceneManagement.SceneManager.LoadScene(buildIndex);
        }
    }
}
