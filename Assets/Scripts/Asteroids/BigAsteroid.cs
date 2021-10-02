using UnityEngine;

public class BigAsteroid : AsteroidBase
{
    public override void Create(Vector3 position, Quaternion rotation)
    {
        base.Create(position, rotation);

        _rigidbody.velocity = Random.insideUnitCircle * Random.Range(MinSpeed, MaxSpeed);
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
