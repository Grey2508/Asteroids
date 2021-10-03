using UnityEngine;

public class SmallAsteroid : AsteroidBase
{
    public override void Create(Vector3 position, Vector3 velocity)
    {
        base.Create(position, velocity);

        _rigidbody.velocity = velocity.normalized * Random.Range(MinSpeed, MaxSpeed);
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
