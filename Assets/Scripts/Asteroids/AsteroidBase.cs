using UnityEngine;

public class AsteroidBase : MonoBehaviour, IPoolable
{
    [SerializeField] protected float AngularSpeed;
    [SerializeField] protected float MinSpeed;
    [SerializeField] protected float MaxSpeed;

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
        Free = false;
        gameObject.SetActive(true);

        transform.position = position;
        transform.rotation = rotation;

        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        //Instantiate(AsteroidExplosion, transform.position, Quaternion.identity);
        gameObject.SetActive(false);

        Free = true;
    }

    public void SetNextPool(ObjectPool pool)
    {
        NextPool = pool;
    }
}
