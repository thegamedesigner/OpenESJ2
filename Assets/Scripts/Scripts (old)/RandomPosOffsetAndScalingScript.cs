using UnityEngine;
using System.Collections;

public class RandomPosOffsetAndScalingScript : MonoBehaviour
{
	public Vector3 posOffset = Vector3.zero;
	public Vector3 ranScale = Vector3.zero;
	public Vector3 minScale = Vector3.zero;

	public bool useXScalingForAll = false;
	public bool useMinScale = false;
	public bool dontScaleZeros = false;
	public bool setScale = false;//set scale or add to it
	public bool MinOrMaxOnly = false;
	public bool useIntForRandomPosX = false;
	public bool useIntForRandomPosY = false;
	public bool useIntForRandomPosZ = false;
    public bool localSpace = false;


	void Start()
	{
		if(!localSpace){xa.glx = transform.position;}
        else { xa.glx = transform.localPosition; }
		xa.glx.x += Random.Range(-posOffset.x, posOffset.x);
		xa.glx.y += Random.Range(-posOffset.y, posOffset.y);
		xa.glx.z += Random.Range(-posOffset.z, posOffset.z);
		if (useIntForRandomPosX) { xa.glx.x = (int)(xa.glx.x); }
		if (useIntForRandomPosY) { xa.glx.y = (int)(xa.glx.y); }
        if (useIntForRandomPosZ) { xa.glx.z = (int)(xa.glx.z); }
        if (!localSpace) { transform.position = xa.glx; }
        else { transform.localPosition = xa.glx; }
		

		xa.glx = transform.localScale;
		if (useMinScale)
		{
			if (!dontScaleZeros || (dontScaleZeros && ranScale.x != 0)) { if (setScale) { xa.glx.x = Random.Range(minScale.x, ranScale.x); } else { xa.glx.x += Random.Range(minScale.x, ranScale.x); } }
			if (!dontScaleZeros || (dontScaleZeros && ranScale.y != 0)) { if (setScale) { xa.glx.y = Random.Range(minScale.y, ranScale.y); } else { xa.glx.y += Random.Range(minScale.y, ranScale.y); } }
			if (!dontScaleZeros || (dontScaleZeros && ranScale.z != 0)) { if (setScale) { xa.glx.z = Random.Range(minScale.z, ranScale.z); } else { xa.glx.z += Random.Range(minScale.z, ranScale.z); } }
		}
		else
		{
			if (!dontScaleZeros || (dontScaleZeros && ranScale.x != 0)) { xa.glx.x += Random.Range(-ranScale.x, ranScale.x); }
			if (!dontScaleZeros || (dontScaleZeros && ranScale.y != 0)) { xa.glx.y += Random.Range(-ranScale.y, ranScale.y); }
			if (!dontScaleZeros || (dontScaleZeros && ranScale.z != 0)) { xa.glx.z += Random.Range(-ranScale.z, ranScale.z); }
		}

		if (MinOrMaxOnly && setScale)
		{
			if (!dontScaleZeros || (dontScaleZeros && ranScale.x != 0)) { if (Random.Range(0, 100) >= 50) { xa.glx.x = minScale.x; } else { xa.glx.x = ranScale.x; } }
			if (!dontScaleZeros || (dontScaleZeros && ranScale.y != 0)) { if (Random.Range(0, 100) >= 50) { xa.glx.y = minScale.y; } else { xa.glx.y = ranScale.y; } }
			if (!dontScaleZeros || (dontScaleZeros && ranScale.z != 0)) { if (Random.Range(0, 100) >= 50) { xa.glx.z = minScale.z; } else { xa.glx.z = ranScale.z; } }

		}

		if (useXScalingForAll) { xa.glx.y = xa.glx.x; xa.glx.z = xa.glx.x; }
		transform.localScale = xa.glx;


	}

	void Update()
	{

	}
}
