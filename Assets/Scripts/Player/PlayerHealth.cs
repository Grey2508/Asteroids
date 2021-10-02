using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IBlinked
{
    [SerializeField] private int Health = 2;
    [SerializeField] private int MaxHealth = 5;

    [SerializeField] private float InvulnerableTime = 3;
    [SerializeField] private float SpawnDelay = 2;

    //public AudioSource TakeDamageSound;
    //public PitchAndPlay AddHealthSound;

    public HealthUI HealthUI;

    //public DamageScreen DamageScreen;
    [SerializeField] private Blink Blink;
    [SerializeField] private GameObject Ship;

    [SerializeField] private ParticleSystem DeathEffect;

    //public UnityEvent EventOnTakeDamage;
    //public UnityEvent EventOnDie;

    private bool _invulnerable = false; //неуязвимость

    private void Start()
    {
        HealthUI.Setup(MaxHealth);
        HealthUI.DisplayHealth(Health);

        Respawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_invulnerable || other.CompareTag("GameBoundary"))
            return;

        Health--;

        HealthUI.DisplayHealth(Health);

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        
        gameObject.SetActive(false);

        DeathEffect.transform.position = transform.position;
        DeathEffect.Play();

        if (Health <= 0)
        {
            Health = 0;
            Die();

            return;
        }

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

        HealthUI.DisplayHealth(Health);

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
        gameObject.SetActive(true);

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
