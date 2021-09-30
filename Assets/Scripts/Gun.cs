using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform Spawn;
    public GameObject BulletPrefab;
    public float ShotDelay;

    private float _nextShot;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > _nextShot)
        {
            Instantiate(BulletPrefab, Spawn.position, Spawn.rotation);
            _nextShot = Time.time + ShotDelay;
        }
    }

}
