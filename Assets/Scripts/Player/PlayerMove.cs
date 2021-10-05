using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody Rigidbody;

    [SerializeField] private float MaxSpeed;
    [SerializeField] private float Acceleration;

    [SerializeField] private GameObject EngineFire;

    [SerializeField] private float MaxAngularVelocity;
    [SerializeField] private float TorqueSpeed;

    [SerializeField] private float RotationSpeed;
    [SerializeField] private Camera Camera;

    [SerializeField] private KeyCode PowerMouse = KeyCode.Mouse1;

    private float _power;
    private float _torque;

    private void Start()
    {
        Rigidbody.maxAngularVelocity = MaxAngularVelocity;
    }
    private void FixedUpdate()
    {
        float power = Input.GetAxisRaw("Vertical");

        if (Menu.CurrentControlType == ControlType.Mouse)
            if (Input.GetKey(PowerMouse))
                power = 1;

        _power = power * Acceleration;
        _power = Mathf.Clamp(_power, 0, MaxSpeed);

        if (_power > 0)
            EngineFire.SetActive(true);
        else
            EngineFire.SetActive(false);

        if (Menu.CurrentControlType == ControlType.Keyboard)
        {
            _torque = Input.GetAxis("Horizontal") * TorqueSpeed;

            Rigidbody.AddRelativeTorque(_torque, 0, 0, ForceMode.VelocityChange);
        }
        else
        {
            RotateWithMouse();
        }

        Rigidbody.AddRelativeForce(0, _power, 0, ForceMode.VelocityChange);
        Vector3 newVelocity = Vector3.ClampMagnitude(Rigidbody.velocity, MaxSpeed); // ограничение максимальной скорости
        Rigidbody.velocity = newVelocity;
    }

    private void RotateWithMouse()
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

        Plane plane = new Plane(Vector3.back, Vector3.zero);

        plane.Raycast(ray, out float distance);

        Vector3 point = ray.GetPoint(distance);

        Vector3 toPoint = point - transform.position;

        Quaternion targetRotation = Quaternion.Euler(-Vector3.SignedAngle(Vector3.up, toPoint, Vector3.forward), 90, 0);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);

    }
}
