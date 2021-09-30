using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody Rigidbody;

    public float MaxSpeed;
    public float Acceleration;

    public GameObject EngineFire;

    public float MaxAngularVelocity;
    public float TorqueSpeed;

    private float _power;
    private float _torque;

    //private GameManager _manager;

    //public AudioSource Sound;

    private void Start()
    {
        Rigidbody.maxAngularVelocity = MaxAngularVelocity;

        //_manager = FindObjectOfType<GameManager>();
    }
    private void FixedUpdate()
    {
        _power = Input.GetAxisRaw("Vertical") * Acceleration;
        _power = Mathf.Clamp(_power, 0, MaxSpeed);

        if (_power > 0)
            EngineFire.SetActive(true);
        else
            EngineFire.SetActive(false);

        _torque = Input.GetAxis("Horizontal") * -TorqueSpeed;

        Rigidbody.AddRelativeForce(0, _power, 0, ForceMode.VelocityChange);
        Vector3 newVelocity = Vector3.ClampMagnitude(Rigidbody.velocity, MaxSpeed); // ограничение максимальной скорости
        Rigidbody.velocity = newVelocity;

        Rigidbody.AddRelativeTorque(0, 0, _torque, ForceMode.VelocityChange);

        //Sound.pitch = 0.5f + powerLevel;
    }

    private void OnEnable()
    {
        Rigidbody.isKinematic = false;
        //Sound.enabled = true;
    }

    private void OnDisable()
    {
        //Rigidbody.isKinematic = true;
        //Sound.enabled = false;
    }
}
