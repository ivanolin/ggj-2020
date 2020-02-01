using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTester : MonoBehaviour
{
    private void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            Managers.AudioManager.SwitchSongToOther();
        }
    }
}
