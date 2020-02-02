using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public int health = 3;
    public int maxHealth;
    private float newSpeed;
    private float minSpeed;
    private float maxSpeed;
    public SpriteRenderer spriterender;
    public Image HealthBar;
    public GameObject woodPiece;
    public PlayerMovementController movementController;

    public AudioClip damageSound;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        movementController = GetComponent<PlayerMovementController>();
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
        if(health <= 0)
        {
            //TRIGGER POOF ANIMATION
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hazard")
        {
            TakeDamage();
            other.enabled = false;
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

        // a different sound will play if the player has actually died
        if (health > 0) {
            Managers.AudioManager.PlaySoundEffect(damageSound);
        }

        // use the current health to set the intensity of the background music
        Managers.AudioManager?.ChangeIntensity(health <= 3 ? 2 : 1);
    }

    public void Heal()
    {
        health++;
        maxSpeed = movementController.maxSpeed;
        minSpeed = movementController.minSpeed;
        newSpeed = Vector3.Lerp(new Vector3(minSpeed, 0, 0), new Vector3(maxSpeed, 0, 0), (float)health / maxHealth).x;
        movementController.Speed = newSpeed;
        SetHealthBar();
    }
}
