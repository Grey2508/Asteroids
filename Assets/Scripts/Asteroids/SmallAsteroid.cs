using UnityEngine;

public class SmallAsteroid : AsteroidBase
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
    }
}
