﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IBlinked
{
    public int Health = 2;
    public int MaxHealth = 5;

    public float InvulnerableTime = 3;
    public float SpawnDelay = 2;

    //public AudioSource TakeDamageSound;
    //public PitchAndPlay AddHealthSound;

    //public HealthUI HealthUI;

    //public DamageScreen DamageScreen;
    public Blink Blink;
    public GameObject Ship;

    public ParticleSystem DeathEffect;

    //public UnityEvent EventOnTakeDamage;
    //public UnityEvent EventOnDie;

    private bool _invulnerable = false; //неуязвимость

    private void Start()
    {
        Respawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_invulnerable || other.tag == "GameBoundary")
            return;

        Health--;

        //HealthUI.DisplayHealth(Health);

        if (Health <= 0)
        {
            Health = 0;
            Die();
        }

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        
        Ship.SetActive(false);

        DeathEffect.transform.position = transform.position;
        DeathEffect.Play();

        Invoke(nameof(Respawn), SpawnDelay);


        //EventOnTakeDamage.Invoke();
        //DamageScreen.StartEffect();
        //Blink.StartEffect();
        //TakeDamageSound.pitch = Random.Range(0.8f, 1.2f);
        //TakeDamageSound.Play();
    }

    public void AddHealth(int healthValue)
    {
        Health += healthValue;

        //HealthUI.DisplayHealth(Health);

        //AddHealthSound.Play();

        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }

    private void Die()
    {



        //EventOnDie.Invoke();

        Debug.Log("You lose");
    }

    private void StopInvulnerable()
    {
        _invulnerable = false;
    }

    private void Respawn()
    {
        Ship.SetActive(true);

        transform.position = Vector3.zero;

        _invulnerable = true;
        Invoke(nameof(StopInvulnerable), InvulnerableTime);

        Blink.StartEffect(InvulnerableTime, this);
    }

    public void Show()
    {
        Ship.SetActive(true);
    }

    public void Hide()
    {
        Ship.SetActive(false);
    }
}