using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public int health = 15;
    private int maxHealth;

    public SpriteRenderer spriterender;
    public Image HealthBar;
    public GameObject woodPiece;

    public AudioClip damageSound;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        health /= 2;
        SetHealthBar();
    }

    void SetHealthBar()
    {
        HealthBar.fillAmount = (float)health / (float)maxHealth;
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

        Managers.AudioManager?.PlaySoundEffect(damageSound);

        health--;
        if(health < 0)
        {
            health = 0;
        }
        SetHealthBar();
    }

    public void Heal()
    {
        health++;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        SetHealthBar();
    }
}
