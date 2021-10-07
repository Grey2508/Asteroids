using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private float BulletSpeed = 100;

    private Rigidbody _rigidbody;
    private float _lifeTime;

    public bool Free { get; set; }

    public ObjectPool NextPool { get; }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _lifeTime = GameBoundary.Width / BulletSpeed;

        SetActive(false);
    }

    public virtual void Create(Vector3 position, Quaternion rotation)
    {
        gameObject.SetActive(true);

        Free = false;

        transform.position = position;
        transform.rotation = rotation;

        _rigidbody.velocity = transform.forward * BulletSpeed;

        Invoke(nameof(DestroyBullet), _lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameBoundary"))
            return;

        DestroyBullet();
    }

    private void DestroyBullet()
    {
        gameObject.SetActive(false);
        Free = true;
    }

    public void SetNextPool(ObjectPool pool) { }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
}