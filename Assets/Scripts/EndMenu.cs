using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{

    public ScreenWipeCanvas screenWipe;

    public void MainMenu()
    {
        screenWipe.WipeScreen();
        Managers.AudioManager?.SwitchToOpening();
        Managers.Instance.LoadSceneWithDelay(0, screenWipe.wipeTime + 0.5f);
    }

    public void Quit()
    {
        screenWipe.WipeScreen();
        Managers.Instance.QuitWithDelay(screenWipe.wipeTime + 0.5f);
    }
}
