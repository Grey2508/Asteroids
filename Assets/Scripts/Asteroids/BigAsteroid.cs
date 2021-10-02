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
        base.OnTriggerEnter(other);
    }
}
