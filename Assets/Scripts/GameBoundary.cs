using UnityEngine;

public class GameBoundary : MonoBehaviour
{
    public static float Width
    {
        get;
        private set;
    }

    public static float Height
    {
        get;
        private set;
    }

    private void Awake()
    {
        Width = transform.localScale.x;
        Height = transform.localScale.y;
    }

    private void OnTriggerExit(Collider other)
    {
        Move(other.attachedRigidbody.transform);
    }

    public static void Move(Transform transform)
    {
        Vector3 contactPoint = transform.position;

        int signX = (Mathf.Abs(contactPoint.x) >= (Width / 2) ? -1 : 1);
        int signY = (Mathf.Abs(contactPoint.y) >= (Height / 2) ? -1 : 1);

        transform.position = new Vector3(contactPoint.x * signX, contactPoint.y * signY, 0);
    }
}
