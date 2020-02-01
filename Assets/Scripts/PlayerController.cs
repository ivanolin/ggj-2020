using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float Speed;
    public Vector3 LastDirection;

    private CharacterController characterController;

    // Update is called once per frame

    void Start() {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {

        var moveDirection = new Vector3(-Input.GetAxis("Horizontal"), 0.0f, -Input.GetAxis("Vertical"));

        if(moveDirection.magnitude > 0)
            LastDirection = moveDirection;

        if(!characterController.isGrounded)
            moveDirection += new Vector3(0,-1,0);

        moveDirection *= Speed;

        GetComponent<CharacterController>().Move(moveDirection * Time.deltaTime);
    }
}
