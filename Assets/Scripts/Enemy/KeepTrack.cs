using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepTrack : MonoBehaviour
{
    [SerializeField] Transform Target;

    void Update()
    {
        Vector3 toTarget = Target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(toTarget, new Vector3(0, 1, 1));
    }
}
