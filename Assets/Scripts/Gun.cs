using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform Spawn;
    [SerializeField] private ObjectPool BulletPool;
    [SerializeField] private float ShotDelay;

    private float _nextShot;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > _nextShot)
        {
            Bullet newBullet = BulletPool.GetNextObject() as Bullet;
            newBullet.Create(Spawn.position, Spawn.rotation);

            _nextShot = Time.time + ShotDelay;
        }
    }
}
