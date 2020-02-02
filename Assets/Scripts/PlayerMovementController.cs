using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    public string HorizontalAxis;
    public string VerticalAxis;

    public float Speed;
    public float minSpeed;
    public float maxSpeed;
    public Vector3 LastDirection;

    private CharacterController characterController;


    public AudioClip[] footstepSounds;
    public float footstepSoundSpeed;
    public float footstepVolume;

    bool isMoving;


    // Update is called once per frame

    void Start() {
        characterController = GetComponent<CharacterController>();
        Speed = maxSpeed;

        StartCoroutine(PlayFootstepSounds());
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {

        var moveDirection = new Vector3(-Input.GetAxis(HorizontalAxis), 0.0f, -Input.GetAxis(VerticalAxis));

        isMoving = moveDirection.magnitude > 0.2f;
        if (isMoving) {
            LastDirection = moveDirection;
        }

        if(!characterController.isGrounded)
            moveDirection += new Vector3(0,-1,0);

        moveDirection *= Speed;

        GetComponent<CharacterController>().Move(moveDirection * Time.deltaTime);
    }

    IEnumerator PlayFootstepSounds() {
        while (true) {
            if (isMoving) {
                Managers.AudioManager?.PlayRandomSoundEffect(footstepSounds, footstepVolume, 0.15f);
            }

            yield return new WaitForSeconds(footstepSoundSpeed);
        }
    }
}
