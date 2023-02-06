using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraX : MonoBehaviour
{
    public Transform target;

    // Use this for initialization
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 offset = -1 * target.forward + 2 * target.up;
        transform.position = target.position + offset;
        transform.LookAt(target.position + 1.6f * target.up);

    }

}
