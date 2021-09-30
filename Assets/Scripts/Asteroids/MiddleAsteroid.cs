using UnityEngine;

public class MiddleAsteroid : AsteroidBase
{
    public override void Create(Vector3 position, Quaternion rotation)
    {
        base.Create(position, rotation);

        _rigidbody.velocity = transform.up * Random.Range(MinSpeed, MaxSpeed);
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameBoundary"))
            return;

        base.OnTriggerEnter(other);

        if (other.attachedRigidbody.GetComponent<Bullet>())
        {
            _rigidbody = GetComponent<Rigidbody>();

            AsteroidBase newAsteroid = NextPool.GetNextObject() as AsteroidBase;
            newAsteroid.Create(transform.position, Quaternion.Euler(_rigidbody.velocity.normalized + new Vector3(0, 45, 0)));

            newAsteroid = NextPool.GetNextObject() as AsteroidBase;
            newAsteroid.Create(transform.position, Quaternion.Euler(_rigidbody.velocity.normalized + new Vector3(0, -45, 0)));
        }
    }
}
