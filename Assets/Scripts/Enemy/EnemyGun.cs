using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField] private Transform Spawn;

    [SerializeField] private ObjectPool BulletPool;

    [SerializeField] private float MinShotDelay = 2;
    [SerializeField] private float MaxShotDelay = 5;

    private float _nextShot;

    void Update()
    {
        if (Time.time > _nextShot)
        {
            Bullet newBullet = BulletPool.GetNextObject() as Bullet;
            newBullet.Create(Spawn.position, Spawn.rotation);

            _nextShot = Time.time + Random.Range(MinShotDelay, MaxShotDelay);
        }
    }
}
