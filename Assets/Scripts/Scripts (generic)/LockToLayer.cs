using UnityEngine;
using System.Collections;

public class LockToLayer : MonoBehaviour
{

    public xa.layers myLayer = xa.layers.None;

    void Update()
    {
        xa.glx = transform.position;
        xa.glx.z = xa.GetLayer(myLayer);
        transform.position = xa.glx;
    }
}
