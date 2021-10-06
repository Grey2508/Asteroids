using UnityEngine;

public class Borders : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.attachedRigidbody.velocity = -other.attachedRigidbody.velocity;
    }
}
