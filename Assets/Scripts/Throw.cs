using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public float ThrowFactor;
    Rigidbody Rigidbody;
    public int numberOfProjectiles = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && numberOfProjectiles > 0)
        {
            numberOfProjectiles--;
            ThrowObject();
        }
    }
    void ThrowObject()
    {
        Vector3 throwDirection = GetComponent<PlayerMovementController>().LastDirection.normalized;
        Debug.Log($"{throwDirection.x} {throwDirection.y} {throwDirection.z}");
        GameObject projectileObject = Instantiate(ProjectilePrefab, GetComponent<Rigidbody>().position + Vector3.up * 0.5f, Quaternion.identity);
        projectileObject.GetComponent<Rigidbody>().AddForce(throwDirection * ThrowFactor);
        projectileObject.GetComponentInChildren<Projectile>().Throw();
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Wood" && other.GetComponent<Projectile>().canPickUp)
        {
            GameObject.Destroy(other.gameObject.transform.parent.gameObject);
            numberOfProjectiles++;
        }
    }

}