using UnityEngine;
using System.Collections;

public class SnapToLayerScript : MonoBehaviour
{
    public xa.layers myLayer = xa.layers.None;
    public bool snapNormal = true;//Snaps in Awake
    public bool snapLate = false;//Snaps in Start 
    public bool snapVeryLate = false;//Snaps in Update

    void Awake()
    {
        if (snapNormal) { SnapToLayerFunc(); }
    }

    void Start()
    {
        if (snapLate) { SnapToLayerFunc(); }
    }

    void Update()
    {
        if (snapVeryLate) { snapVeryLate = false; SnapToLayerFunc(); }
        this.enabled = false;
    }

    void SnapToLayerFunc()
    {
        xa.glx = transform.position;
        xa.glx.z = xa.GetLayer(myLayer);
        transform.position = xa.glx;
    }

}
