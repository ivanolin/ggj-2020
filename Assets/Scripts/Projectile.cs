using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rigidbody;
    
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Throw(Vector3 origin, Vector3 destination)
    {
        Debug.Log(destination);
        rigidbody.AddForce((destination - origin)*100);
    }
}
