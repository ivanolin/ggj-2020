using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a singleton component that provides global access to the game's managers (eg scene manager, audio manager, etc)
public class Managers : MonoBehaviour
{
    static Managers instance;

    private AudioManager audioManager;
    public static AudioManager AudioManager { get { return instance?.audioManager; }}
    
    // add more managers here

    

    private void Awake() {
        // very basic singleton pattern
        
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    
}
