using UnityEngine;
using System.Collections;

public class SetTransform : MonoBehaviour
{
	public GameObject GO = null;
	public Vector3 setPos = Vector3.zero;
	public Vector3 setRotation = Vector3.zero;
	public Vector3 setScale = Vector3.zero;

	public bool dontSetZeros = false;
	public bool useSetPos = false;
	public bool useSetRotation = false;
	public bool useSetScale = false;
	
	void Update()
	{
		if (this.enabled)
		{
			if (useSetPos)
			{
				xa.glx = GO.transform.position;
				if (!dontSetZeros || (dontSetZeros && setPos.x != 0)) { xa.glx.x = setPos.x; }
				if (!dontSetZeros || (dontSetZeros && setPos.y != 0)) { xa.glx.y = setPos.y; }
				if (!dontSetZeros || (dontSetZeros && setPos.z != 0)) { xa.glx.z = setPos.z; }
				GO.transform.position = xa.glx;
			}
			if (useSetRotation)
			{
				xa.glx = GO.transform.localEulerAngles;
				if (!dontSetZeros || (dontSetZeros && setRotation.x != 0)) { xa.glx.x = setRotation.x; }
				if (!dontSetZeros || (dontSetZeros && setRotation.y != 0)) { xa.glx.y = setRotation.y; }
				if (!dontSetZeros || (dontSetZeros && setRotation.z != 0)) { xa.glx.z = setRotation.z; }
				GO.transform.localEulerAngles = xa.glx;
			}
			if (useSetScale)
			{
				xa.glx = GO.transform.localScale;
				if (!dontSetZeros || (dontSetZeros && setScale.x != 0)) { xa.glx.x = setScale.x; }
				if (!dontSetZeros || (dontSetZeros && setScale.y != 0)) { xa.glx.y = setScale.y; }
				if (!dontSetZeros || (dontSetZeros && setScale.z != 0)) { xa.glx.z = setScale.z; }
				GO.transform.localScale = xa.glx;
			}
			this.enabled = false;
		}
	}
}
