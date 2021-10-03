using UnityEngine;

public class BigAsteroid : AsteroidBase
{
    public override void Create(Vector3 position, Vector3 velocity)
    {
        base.Create(position, velocity);

        _rigidbody.velocity = Random.insideUnitCircle * Random.Range(MinSpeed, MaxSpeed);
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
