using UnityEngine;
using System.Collections;

public class ZepplinScript : MonoBehaviour
{
    public float speed = 0;
    public float minX = 0;
    public float maxX = 0;

    void Start()
    {

    }

    void Update()
    {
        xa.glx = transform.localPosition;
        xa.glx.x -= speed * fa.deltaTime;
        transform.localPosition = xa.glx;


        xa.glx = transform.position;
        if (transform.position.x < minX)
        {
            xa.glx.x = maxX;
        }
        transform.position = xa.glx;


        
    }
}
