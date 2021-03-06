﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool canHeal = false;
    public bool canPickUp = false;

    Rigidbody rigidbody;

    public float fastSpeed;
    
    // Case 1: throw: can heal immediately, but can't pick up for interval
    // Case 2: damage drop: can pickup immediately but can't heal for interval 

    // Start is called before the first frame update
    void Awake()
    {

    }

    private void Start() {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Throw()
    {
        canHeal = true;
        StartCoroutine(throwCoroutine());
    
        // start woosh loop
        // throwAudio.Play();
    }

    public void DamageDrop()
    {
        canPickUp = true;
        StartCoroutine(damageDropCoroutine());
    }

    IEnumerator throwCoroutine()
    {
        
        yield return new WaitForSeconds(0.5f);
        canPickUp = true;
        //Debug.Log("Ready for pick up");
    }

    IEnumerator damageDropCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        canHeal = true;
    }


    // private void OnCollisionEnter(Collision other) {
    //     // stop woosh loop
    //     throwAudio.Stop();

    //     // play hit sound
    //     float hitVolume = Mathf.Clamp(rigidbody.velocity.magnitude / fastSpeed, 0, 1);
    //     Managers.AudioManager?.PlaySoundEffect(hitSound, hitVolume);
    // }

}
