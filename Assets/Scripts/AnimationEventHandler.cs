using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEventHandler : MonoBehaviour
{
    public void LoadTreeWin()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadBirdWin()
    {
        SceneManager.LoadScene(3);
    }
}
