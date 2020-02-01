using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int health = 3;
    public int maxHealth;
    private float newSpeed;
    private float minSpeed;
    private float maxSpeed;
    public SpriteRenderer spriterender;
    public Sprite threeHealth;
    public Sprite twoHealth;
    public Sprite oneHealth;
    public Sprite zeroHealth;

    public GameObject woodPiece;
    public PlayerMovementController movementController;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        movementController = GetComponent<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hazard")
        {
            TakeDamage();
            Instantiate(woodPiece, transform.position, transform.rotation).GetComponentInChildren<Projectile>().DamageDrop();
        }

        if (other.tag == "Wood" && other.GetComponent<Projectile>().canHeal)
        {
            Heal();
            GameObject.Destroy(other.gameObject.transform.parent.gameObject);
        }
    }

    public void TakeDamage()
    {
        health--;
        maxSpeed = movementController.maxSpeed;
        minSpeed = movementController.minSpeed;
        newSpeed = Vector3.Lerp(new Vector3(minSpeed,0,0),new Vector3(maxSpeed,0,0),(float)health/maxHealth).x;
        movementController.Speed = newSpeed;
        if(health < 0)
        {
            health = 0;
        }

        ChangeSprite();
    }

    public void Heal()
    {
        health++;
        maxSpeed = movementController.maxSpeed;
        minSpeed = movementController.minSpeed;
        newSpeed = Vector3.Lerp(new Vector3(minSpeed, 0, 0), new Vector3(maxSpeed, 0, 0), (float)health / maxHealth).x;
        movementController.Speed = newSpeed;
        if (health > 3)
        {
            health = 3;
        }

        ChangeSprite();
    }

    public void ChangeSprite()
    {
        if(health == 3)
        {
            //spriterender.sprite = threeHealth;
            Debug.Log("3");
        }

        if(health == 2)
        {
            //spriterender.sprite = twoHealth;
            Debug.Log("2");
        }

        if(health == 1)
        {
            //spriterender.sprite = oneHealth;
            Debug.Log("1");
        }

        if(health == 0)
        {
            //spriterender.sprite = zeroHealth;
            Debug.Log("0");
        }
    }
}
