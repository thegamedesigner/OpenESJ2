using UnityEngine;
using System.Collections;

public class ApplyOffsetOnEnabled : MonoBehaviour
{
    public Vector3 offset = Vector3.zero;

    void Update()
    {
        transform.position += offset;
        this.enabled = false;
    }
}
