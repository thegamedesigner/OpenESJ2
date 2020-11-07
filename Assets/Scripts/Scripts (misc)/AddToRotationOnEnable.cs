using UnityEngine;
using System.Collections;

public class AddToRotationOnEnable : MonoBehaviour
{
    public Vector3 addAmount = Vector3.zero;
    void Update()
    {
        this.transform.localEulerAngles += addAmount;
        this.enabled = false;
    }
}
