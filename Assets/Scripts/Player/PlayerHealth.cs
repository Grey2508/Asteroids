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

    [SerializeField] private HealthUI HealthUI;

    [SerializeField] private Blink Blink;
    [SerializeField] private GameObject Ship;

    [SerializeField] private GameObject DeathEffect;

    private int _defaultHealth;

    private bool _invulnerable = false; //неуязвимость

    private void Start()
    {
        HealthUI.Setup(MaxHealth);
        HealthUI.DisplayHealth(Health);
        _defaultHealth = Health;

        Respawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_invulnerable || other.CompareTag("GameBoundary"))
            return;

        Health--;

        HealthUI.DisplayHealth(Health);

        gameObject.SetActive(false);

        DeathEffect.SetActive(true);
        DeathEffect.transform.position = transform.position;

        if (Health <= 0)
        {
            Health = 0;
            Die();

            return;
        }

        Invoke(nameof(Respawn), SpawnDelay);
    }

    public void AddHealth(int healthValue)
    {
        Health += healthValue;

        HealthUI.DisplayHealth(Health);

        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }

    private void Die()
    {
        GameOver.Show();
    }

    private void StopInvulnerable()
    {
        _invulnerable = false;
    }

    private void Respawn()
    {
        DeathEffect.SetActive(false);

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

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

    public void Restart()
    {
        Health = _defaultHealth;
        HealthUI.DisplayHealth(Health);

        Respawn();
    }
}
