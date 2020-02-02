using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Throw : MonoBehaviour
{
    public float ThrowDelay = 0.25f;
    public float ThrowCooldown = 0.75f;
    public GameObject ProjectilePrefab;
    public string ThrowAxis;
    public float ThrowFactor;
    Rigidbody Rigidbody;
    public int numberOfProjectiles = 3;
    public TextMeshProUGUI Text;

    public AudioClip throwSound;

    public Animator animator;

    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        Rigidbody = GetComponent<Rigidbody>();
        UpdateText();
    }

    // Update is called once per frame
    private bool ThrowDown = false;
    void Update()
    {
        if (ThrowDown == false && Input.GetAxis(ThrowAxis) > 0 && numberOfProjectiles > 0)
        {
            animator.SetBool("Throwing", true);
            numberOfProjectiles--;
            ThrowObject();

            ThrowDown = true;
        }
        else if(Input.GetAxis(ThrowAxis) == 0)
        {
            animator.SetBool("Throwing", false);
            ThrowDown = false;
        }
    }
    void ThrowObject()
    {
        if(canThrow)
            StartCoroutine(ThrowRoutine());
    }

    bool canThrow = true;
    IEnumerator ThrowRoutine()
    {
        canThrow = false;

        yield return new WaitForSeconds(ThrowDelay);

        Vector3 throwDirection = GetComponent<PlayerMovementController>().LastDirection.normalized;
        Debug.Log($"{throwDirection.x} {throwDirection.y} {throwDirection.z}");
        GameObject projectileObject = Instantiate(ProjectilePrefab, GetComponent<Rigidbody>().position + Vector3.up * 0.5f, Quaternion.identity);
        projectileObject.GetComponent<Rigidbody>().velocity = throwDirection * ThrowFactor;
        projectileObject.GetComponentInChildren<Projectile>().Throw();

        Managers.AudioManager?.PlaySoundEffect(throwSound);

        UpdateText();

        yield return new WaitForSeconds(ThrowCooldown);

        canThrow = true;
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Wood" && other.GetComponent<Projectile>().canPickUp)
        {
            GameObject.Destroy(other.gameObject.transform.parent.gameObject);
            numberOfProjectiles++;
        }

        UpdateText();
    }

    private void UpdateText()
    {
        Text.text = "" + numberOfProjectiles;
    }

}