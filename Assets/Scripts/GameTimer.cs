using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float timer = 120f;
    private int convert;
    public Text display;


    AudioSource clockSource;

    // Start is called before the first frame update
    void Start()
    {
        Managers.AudioManager?.SwitchToMain(true);
        clockSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            convert = Mathf.CeilToInt(timer);
            display.text = convert.ToString();
        }
        else
        {
            //TRIGGER BIRD WIN ANIMATION
            SceneManager.LoadScene(3);

            Managers.AudioManager?.EndMain(false);
        }


        clockSource.volume = Mathf.Clamp((10 - timer) / 10 , 0, 1);
    }
}
