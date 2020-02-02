using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hop : MonoBehaviour
{
    public bool jumpVaild;
    public bool cross = false;
    private float timer = 0f;

    private ChildJumpObject interactable;
    
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
                interactable.transform.parent.GetComponent<BoxCollider>().enabled = true;
        }

        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown("joystick button 0")) && jumpVaild)
        {
            Debug.Log("Preparing to move...");
            interactable.transform.parent.GetComponent<BoxCollider>().enabled = false;
            Debug.Log("Position before: " + transform.position);
            Debug.Log("Position to reach: " + interactable.siblingObject.transform.position);
            cross = true;
            GetComponent<PlayerMovementController>().cross = true;
            Debug.Log("Position after: " + transform.position);
        }

        if (cross)
        {
            Debug.Log("Inside cross statement");
            Debug.Log("Position to reach: " + interactable.siblingObject.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, interactable.siblingObject.transform.position, GetComponent<PlayerMovementController>().Speed * Time.deltaTime);
            if (Mathf.Approximately((transform.position - interactable.siblingObject.transform.position).x, 0))
            {
                if (Mathf.Approximately((transform.position - interactable.siblingObject.transform.position).y, 0))
                {
                    cross = false;
                    GetComponent<PlayerMovementController>().cross = false;
                }
            }
        }

        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Jump" && !cross)
        {
            Debug.Log("JUMP");
            jumpVaild = true;
            timer = 2f;
            interactable = other.gameObject.GetComponent<ChildJumpObject>();
            Debug.Log(jumpVaild);
        }
    }
}
