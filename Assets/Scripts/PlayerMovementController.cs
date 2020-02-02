using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    public Animator animator;
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
        animator = GetComponentInChildren<Animator>();
        StartCoroutine(PlayFootstepSounds());
    }

    void Update()
    {
        Move();
        SetAnimationState();
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
                Managers.AudioManager?.PlaySoundEffect(footstepSounds[Random.Range(0, footstepSounds.Length)], footstepVolume);
            }

            yield return new WaitForSeconds(footstepSoundSpeed);
        }
    }

    void SetAnimationState()
    {
        animator.SetInteger("Direction", GetIntDirection());
        Debug.Log(GetComponent<Rigidbody>().velocity.magnitude);
        animator.SetFloat("Speed", characterController.velocity.magnitude);
    }

    int GetIntDirection()
    {
        float directionDistance = float.MaxValue;
        float testDistance = 0;
        int direction = 0;

        testDistance = Vector3.Distance(Vector3.back, LastDirection);
        if(testDistance < directionDistance)
        {
            directionDistance = testDistance;
            direction = 0;
        }

        testDistance = Vector3.Distance(Vector3.forward, LastDirection);
        if(testDistance < directionDistance)
        {
            directionDistance = testDistance;
            direction = 1;
        }

        testDistance = Vector3.Distance(Vector3.right, LastDirection);
        if(testDistance < directionDistance)
        {
            directionDistance = testDistance;
            direction = 3;
        }

        testDistance = Vector3.Distance(Vector3.left, LastDirection);
        if(testDistance < directionDistance)
        {
            directionDistance = testDistance;
            direction = 2;
        }

        return direction;
    }
}
