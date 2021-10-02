using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepTrack : MonoBehaviour
{
    [SerializeField] Transform Target;

    void Update()
    {
        Vector3 toTarget = Target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(toTarget, Vector3.forward);
    }
}
