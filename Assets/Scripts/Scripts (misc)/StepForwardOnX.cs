using UnityEngine;
using System.Collections;

public class StepForwardOnX : MonoBehaviour
{
    public float width = 0;
    void Start()
    {

    }

    void Update()
    {
        while (Camera.main.GetComponent<Camera>().transform.position.x > transform.position.x + width)
        {
            xa.glx = transform.position;
            xa.glx.x += width * 2;
            transform.position = xa.glx;
        }
        while (Camera.main.GetComponent<Camera>().transform.position.x < transform.position.x - width)
        {
            xa.glx = transform.position;
            xa.glx.x -= width * 2;
            transform.position = xa.glx;
        }
    }
}
