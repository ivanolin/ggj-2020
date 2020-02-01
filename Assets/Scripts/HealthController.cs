using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
<<<<<<< HEAD
    public int health = 3;
    public int maxHealth;
    private float newSpeed;
    private float minSpeed;
    private float maxSpeed;
=======
    public int health = 15;
    private int maxHealth;

>>>>>>> 5f71239ef52ed7c524075120696da54242b901d8
    public SpriteRenderer spriterender;
    public Image HealthBar;
    public GameObject woodPiece;
    public PlayerMovementController movementController;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
<<<<<<< HEAD
        movementController = GetComponent<PlayerMovementController>();
=======
        health /= 2;
        SetHealthBar();
    }

    void SetHealthBar()
    {
        HealthBar.fillAmount = (float)health / (float)maxHealth;
>>>>>>> 5f71239ef52ed7c524075120696da54242b901d8
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
        Debug.Log("TOOK DAMAGE");

        health--;
        maxSpeed = movementController.maxSpeed;
        minSpeed = movementController.minSpeed;
        newSpeed = Vector3.Lerp(new Vector3(minSpeed,0,0),new Vector3(maxSpeed,0,0),(float)health/maxHealth).x;
        movementController.Speed = newSpeed;
        if(health < 0)
        {
            health = 0;
        }
        SetHealthBar();
    }

    public void Heal()
    {
        health++;
<<<<<<< HEAD
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
=======
        if(health > maxHealth)
>>>>>>> 5f71239ef52ed7c524075120696da54242b901d8
        {
            health = maxHealth;
        }
        SetHealthBar();
    }
}
