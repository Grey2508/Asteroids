using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody Rigidbody;
    [SerializeField] private float BulletSpeed = 100;

    void Start()
    {
        Rigidbody.velocity = transform.forward * BulletSpeed;
        
        float lifeTime = GameBoundary.Width / BulletSpeed;
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameBoundary"))
            return;

        DestroyBullet();
    }

    private void DestroyBullet()
    {
        //Instantiate(EffectPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}

