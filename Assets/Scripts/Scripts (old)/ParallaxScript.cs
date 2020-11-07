using UnityEngine;
using System.Collections;

public class ParallaxScript : MonoBehaviour
{
    public float multiplier = 0.5f;
    public float xOffset = 0;
    public float yOffset = 0;
    public bool vertical = false;
    public bool verticalAndHor = false;

    public bool useSnapLooping = false;//currently only works on X, with no offset 
    public float width = 0;
    Vector3 startPos;
	void Start()
	{
        if (useSnapLooping) { startPos = transform.position; }

	}

	void Update()
	{
		xa.glx = transform.position;
        if (!vertical || verticalAndHor)
        {
            xa.glx.x = (Camera.main.GetComponent<Camera>().gameObject.transform.position.x + xOffset) * multiplier;
        }
        if(vertical || verticalAndHor)
        {
            xa.glx.y = (Camera.main.GetComponent<Camera>().gameObject.transform.position.y + yOffset) * multiplier;
        }
		transform.position = xa.glx;


        if (useSnapLooping)
        {
			while (Camera.main.GetComponent<Camera>().gameObject.transform.position.x > transform.position.x + width)
            {
                xa.glx = transform.position;
                xa.glx.x += width;
                transform.position = xa.glx;
            }
            while (Camera.main.GetComponent<Camera>().gameObject.transform.position.x < transform.position.x - width)
            {
                xa.glx = transform.position;
                xa.glx.x -= width;
                transform.position = xa.glx;
            }

        }
	}
}
