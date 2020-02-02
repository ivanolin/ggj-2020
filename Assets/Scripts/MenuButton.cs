using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public AudioClip hoverSound;
    public AudioClip clickSound;

    public void PlayHoverSound() {
        Managers.AudioManager?.PlaySoundEffect(hoverSound, 0.2f);
    }

    public void PlayClickSound() {
        Managers.AudioManager?.PlaySoundEffect(clickSound);
    }
}
