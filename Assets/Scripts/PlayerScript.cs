using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject PlayerExplosion;
    public GameObject EngineFire;
    //public GameObject RightEngineFire;
    public GameObject LazerGun;
    public GameObject LazerShot;
    public float ShotDelay;

    private float NextShot;

    Rigidbody Ship;
    
    public float Speed;
    public float Tilt;

    public float xMin, xMax, zMin, zMax;

    void Start()
    {
        Ship = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Ship.velocity = new Vector3(moveHorizontal, 0, moveVertical) * Speed;

        Ship.rotation = Quaternion.Euler(0, 0, -Ship.velocity.x * Tilt);

        float xPosition = Mathf.Clamp(Ship.position.x, xMin, xMax);
        float zPosition = Mathf.Clamp(Ship.position.z, zMin, zMax);

        Ship.position = new Vector3(xPosition, 0, zPosition);

        float fireScale = -Mathf.Clamp(Ship.velocity.z, 0, Ship.velocity.z / 9);
        EngineFire.transform.localScale = new Vector3(fireScale, 0, fireScale);
        //RightEngineFire.transform.localScale = new Vector3(fireScale, 0, fireScale);

        if (Ship.velocity.z==0)
        {
            EngineFire.transform.localScale = new Vector3(2, 2, 2);
            //RightEngineFire.transform.localScale = new Vector3(2, 2, 2);
        }

        if (Input.GetButton("Fire1") && Time.time > NextShot)
        {
            Instantiate(LazerShot, LazerGun.transform.position, Quaternion.identity);
            NextShot = Time.time + ShotDelay;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameBoundary")
            return;
            
            Instantiate(PlayerExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
    }
}
