using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWipeCanvas : MonoBehaviour
{
    public bool dontWipeOnLoad;

    public float wipeTime;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform t in transform) {
            ScreenWipeItem item = t.GetComponent<ScreenWipeItem>();

            item?.LeaveScreen(dontWipeOnLoad ? 0f : wipeTime);
        }   
    }

    public void WipeScreen() {
        foreach (Transform t in transform) {
            ScreenWipeItem item = t.GetComponent<ScreenWipeItem>();

            item?.CoverScreen(wipeTime);
        }
    }
}
