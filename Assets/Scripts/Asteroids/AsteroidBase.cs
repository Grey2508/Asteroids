using UnityEngine;

public class AsteroidBase : MonoBehaviour, IPoolable
{
    [SerializeField] protected float AngularSpeed;
    [SerializeField] protected float MinSpeed;
    [SerializeField] protected float MaxSpeed;
    [SerializeField] protected float DeflectionAngle;

    [SerializeField] private int Price;

    [SerializeField] private GameObject AsteroidExplosion;

    protected Rigidbody _rigidbody;

    public bool Free { get; set; }
    public ObjectPool NextPool { get; set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        Free = true;
        gameObject.SetActive(false);
    }
    public virtual void Create(Vector3 position, Vector3 velocity)
    {
        MonitorAsteroids.CountAsteroids++;

        Free = false;
        gameObject.SetActive(true);

        transform.position = position;

        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.angularVelocity = Random.insideUnitSphere * AngularSpeed;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameBoundary"))
            return;

        MonitorAsteroids.CountAsteroids--;

        if (other.CompareTag("Player"))
            Score.AddScore(Price);

        //Instantiate(AsteroidExplosion, transform.position, Quaternion.identity);
        gameObject.SetActive(false);

        Free = true;

        if (NextPool != null && other.attachedRigidbody?.GetComponent<Bullet>())
            CutAsteroid();
    }

    protected void CutAsteroid()
    {
        AsteroidBase newAsteroid = NextPool.GetNextObject() as AsteroidBase;

        float x = _rigidbody.velocity.x * Mathf.Cos(DeflectionAngle) - _rigidbody.velocity.y * Mathf.Sin(DeflectionAngle);
        float y = _rigidbody.velocity.x * Mathf.Sin(DeflectionAngle) + _rigidbody.velocity.y * Mathf.Cos(DeflectionAngle);

        Vector3 newVelocity = new Vector3(x, y, 0);

        newAsteroid.Create(transform.position, newVelocity);

        newAsteroid = NextPool.GetNextObject() as AsteroidBase;
        x = _rigidbody.velocity.x * Mathf.Cos(-DeflectionAngle) - _rigidbody.velocity.y * Mathf.Sin(-DeflectionAngle);
        y = _rigidbody.velocity.x * Mathf.Sin(-DeflectionAngle) + _rigidbody.velocity.y * Mathf.Cos(-DeflectionAngle);

        newVelocity = new Vector3(x, y, 0);

        newAsteroid.Create(transform.position, newVelocity);
    }

    public void SetNextPool(ObjectPool pool)
    {
        NextPool = pool;
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
}
