using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private Transform Spawn;
    [SerializeField] private ObjectPool BulletPool;
    [SerializeField] private float ShotDelay;

    [SerializeField] private KeyCode FireKeyboard = KeyCode.Space;
    [SerializeField] private KeyCode FireMouse = KeyCode.Mouse0;

    private float _nextShot;

    void  Update()
    {
        bool shot = (Menu.CurrentControlType == ControlType.Keyboard ? Input.GetKeyDown(FireKeyboard) : (Input.GetKeyDown(FireMouse) || Input.GetKeyDown(FireKeyboard)));

        if (shot && Time.time > _nextShot)
        {
            Bullet newBullet = BulletPool.GetNextObject() as Bullet;
            newBullet.Create(Spawn.position, Spawn.rotation);

            _nextShot = Time.time + ShotDelay;
        }
    }
}
