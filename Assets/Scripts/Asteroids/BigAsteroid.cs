using UnityEngine;

public class BigAsteroid : AsteroidBase
{

    public override void Create(Vector3 position, Quaternion rotation)
    {
        base.Create(position, rotation);

        _rigidbody.angularVelocity = Random.insideUnitSphere * AngularSpeed;
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameBoundary"))
            return;

        base.OnTriggerEnter(other);

        if (other.attachedRigidbody.GetComponent<Bullet>())
        {
            AsteroidBase newAsteroid = NextPool.GetNextObject() as AsteroidBase;
            newAsteroid.Create(transform.position, Quaternion.Euler(_rigidbody.velocity.normalized + new Vector3(0, 0, 45)));

            newAsteroid = NextPool.GetNextObject() as AsteroidBase;
            newAsteroid.Create(transform.position, Quaternion.Euler(_rigidbody.velocity.normalized + new Vector3(0, 0, -45)));
        }
    }
}
