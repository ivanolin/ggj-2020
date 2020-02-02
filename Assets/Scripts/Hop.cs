using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hop : MonoBehaviour
{
    public bool jumpVaild;
    private float timer = 0f;

    private GameObject interactable;
    
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            jumpVaild = false;
            if(interactable != null)
                interactable.GetComponent<BoxCollider>().enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Z) && jumpVaild)
        {
            Debug.Log(interactable.GetComponentInChildren<BoxCollider>().gameObject.name);
            interactable.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Jump")
        {
            Debug.Log("JUMP");
            jumpVaild = true;
            timer = 1.5f;
            interactable = other.gameObject;
        }
    }
}
