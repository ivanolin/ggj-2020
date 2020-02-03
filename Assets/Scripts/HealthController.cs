using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    private Animator animator;
    public int health;
    public int maxHealth;
    private float newSpeed;
    private float minSpeed;
    private float maxSpeed;
    public SpriteRenderer spriterender;
    public Image HealthBar;
    public GameObject woodPiece;
    public PlayerMovementController movementController;

    public AudioClip[] damageSounds;
    public AudioClip deathSound;
    public AudioClip[] healSounds;

    public Sprite UndamagedSprite;
    public Sprite Damage1Sprite;
    public Sprite Damage2Sprite;

    public bool hasTriggeredEnd;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        movementController = GetComponent<PlayerMovementController>();
        health = maxHealth;
        SetHealthBar();
        animator = GetComponentInChildren<Animator>();
    }

    void SetHealthBar()
    {
        HealthBar.fillAmount = (float)health / (float)maxHealth;
    }

    void Update()
    {
        if(!hasTriggeredEnd && health <= 0)
        {
            hasTriggeredEnd = true;

            //TRIGGER POOF ANIMATION


            Managers.AudioManager?.EndMain(true);
            Managers.Instance.LoadSceneWithDelay(2, 0f); // add delay here to view poof animation and then move on
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hazard")
        {
            TakeDamage();
            other.enabled = false;
            GameObject.Destroy(other.gameObject);
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
        SetHealthBar();

        // a different sound will play if the player has actually died
        if (health > 0) {
            Managers.AudioManager.PlayRandomSoundEffect(damageSounds, 0.95f, 0.05f);

            if (health >= maxHealth * (2f / 3f))
            {
                GetComponentInChildren<SpriteRenderer>().sprite = Damage1Sprite;
            }
            else if (health >= maxHealth / 3f && health < maxHealth * (2f / 3f))
            {
                GetComponentInChildren<SpriteRenderer>().sprite = Damage2Sprite;
            }
            else
            {
                GetComponentInChildren<SpriteRenderer>().sprite = UndamagedSprite;
            }

        } else {
            Managers.AudioManager.PlaySoundEffect(deathSound);
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

        Managers.AudioManager?.PlayRandomSoundEffect(healSounds, 0.95f, 0.05f);
    }
}
