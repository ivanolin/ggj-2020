using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float AccelerationFactor;
    public float VelocityCap;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(new Vector2(-Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical")));
    }

    private void Move(Vector2 direction)
    {
        Debug.Log(direction.x + " " + direction.y);

        // Accelerate player in given direction
        GetComponent<Rigidbody>().AddForce(new Vector3(direction.x, 0, direction.y) * AccelerationFactor * Time.deltaTime);

        // Cap velocity

        Vector3 velocity = GetComponent<Rigidbody>().velocity;
        Debug.Log("VEL " + velocity.magnitude);
        if(velocity.magnitude > VelocityCap)
        {
            GetComponent<Rigidbody>().velocity = velocity.normalized * VelocityCap;
        }
    }
}
