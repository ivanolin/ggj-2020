using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hop : MonoBehaviour
{
    public string HorizontalAxis;
    public string VerticalAxis;
    public int HopFactor = 1000;
    
    Rigidbody Rigidbody;
    Vector3 LastDirection;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var moveDirection = new Vector3(-Input.GetAxis(HorizontalAxis), 0.0f, -Input.GetAxis(VerticalAxis));

        if (moveDirection.magnitude > 0)
            LastDirection = moveDirection;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            bool hit = Physics.Raycast(transform.position, LastDirection, 1.0f, LayerMask.GetMask("Interactable Objects"));
            if (hit)
            {
                Rigidbody.AddForce((LastDirection + new Vector3(0, 5, 0)) * HopFactor);
            }
        }
    }
}
