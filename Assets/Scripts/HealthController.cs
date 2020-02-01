using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int health = 3;

    public SpriteRenderer spriterender;
    public Sprite threeHealth;
    public Sprite twoHealth;
    public Sprite oneHealth;
    public Sprite zeroHealth;

    public GameObject woodPiece;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if(health < 0)
        {
            health = 0;
        }

        ChangeSprite();
    }

    public void Heal()
    {
        health++;
        if(health > 3)
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
