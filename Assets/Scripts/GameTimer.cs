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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            convert = (int)timer;
            display.text = convert.ToString();
        }
        else
        {
            //TRIGGER BIRD WIN ANIMATION
            SceneManager.LoadScene(3);
        }
    }
}
