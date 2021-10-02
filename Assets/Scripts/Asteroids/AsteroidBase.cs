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
    public virtual void Create(Vector3 position, Quaternion rotation)
    {
        MonitorAsteroids.CountAsteroids++;

        Free = false;
        gameObject.SetActive(true);

        transform.position = position;
        transform.rotation = rotation;

        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.angularVelocity = Random.insideUnitSphere * AngularSpeed;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameBoundary"))
            return;

        MonitorAsteroids.CountAsteroids--;
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
        newAsteroid.Create(transform.position, Quaternion.Euler(_rigidbody.velocity.normalized + new Vector3(0, 0, DeflectionAngle)));

        newAsteroid = NextPool.GetNextObject() as AsteroidBase;
        newAsteroid.Create(transform.position, Quaternion.Euler(_rigidbody.velocity.normalized + new Vector3(0, 0, -DeflectionAngle)));
    }

    public void SetNextPool(ObjectPool pool)
    {
        NextPool = pool;
    }
}
