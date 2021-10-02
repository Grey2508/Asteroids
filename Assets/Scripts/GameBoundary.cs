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

    private void Awake ()
    {
        Width = transform.localScale.x;
        Height = transform.localScale.y;
    }

    private void OnTriggerExit(Collider other)
    {
        Vector3 contactPoint = other.transform.position;

        int signX = (Mathf.Abs(contactPoint.x) > (transform.localScale.x / 2) ? -1 : 1);
        int signY = (Mathf.Abs(contactPoint.y) > (transform.localScale.y / 2) ? -1 : 1);

        other.attachedRigidbody.transform.position = new Vector3(contactPoint.x * signX, contactPoint.y * signY, 0);
    }
}
