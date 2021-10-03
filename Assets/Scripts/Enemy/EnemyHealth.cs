using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int Health = 1;
    [SerializeField] private ParticleSystem DeathEffect;

    [SerializeField] private int Price;


    [SerializeField] private UnityEvent EventOnTakeDamage;
    [SerializeField] private UnityEvent EventOnDie;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameBoundary"))
            return;

        Health--;

        if (Health <= 0)
        {
            Health = 0;

            Die(other.CompareTag("Player"));
        }

        EventOnTakeDamage.Invoke();
    }

    private void Die(bool addScore)
    {
        EventOnDie.Invoke();

        gameObject.SetActive(false);

        if (addScore)
            Score.AddScore(Price);

        DeathEffect.transform.position = transform.position;
        DeathEffect.Play();
    }
}
