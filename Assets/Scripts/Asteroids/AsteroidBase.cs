using UnityEngine;

public class AsteroidBase : MonoBehaviour, IPoolable
{
    [SerializeField] protected float AngularSpeed;
    [SerializeField] protected float MinSpeed;
    [SerializeField] protected float MaxSpeed;
    [SerializeField] protected float DeflectionAngle;

    [SerializeField] private int Price;

    private ObjectPool _asteroidExplosionsPool;

    protected Rigidbody _rigidbody;

    public bool Free { get; set; }
    public ObjectPool NextPool { get; set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _asteroidExplosionsPool = GameObject.FindWithTag("AsteroidExplosionsPool").GetComponent<ObjectPool>();

        SetActive(false);
    }

    public virtual void Create(Vector3 position, Vector3 velocity)
    {
        MonitorAsteroids.CountAsteroids++;

        Free = false;
        gameObject.SetActive(true);

        transform.position = position;

        if (Mathf.Abs(position.x) >= GameBoundary.Width / 2 || Mathf.Abs(position.y) >= GameBoundary.Height / 2)
            GameBoundary.Move(transform);

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

        AsteroidEffect effect = _asteroidExplosionsPool.GetNextObject() as AsteroidEffect;
        effect.transform.position = transform.position;
        effect.Play();

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
