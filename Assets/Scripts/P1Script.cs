using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Script : MonoBehaviour
{
    public GameObject projectilePrefab;
    Rigidbody rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ThrowObject();
        }
    }
    void ThrowObject()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody.position + Vector3.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        Debug.Log(rigidbody.position);
        Vector3 destination = new Vector3(5, 0, 5);
        projectile.Throw(rigidbody.position,destination);
    }
}