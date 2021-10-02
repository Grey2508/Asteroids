using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody Rigidbody;

    [SerializeField] private float MaxSpeed;
    [SerializeField] private float Acceleration;

    [SerializeField] private GameObject EngineFire;

    [SerializeField] private float MaxAngularVelocity;
    [SerializeField] private float TorqueSpeed;

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
