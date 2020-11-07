using UnityEngine;
using System.Collections;

public class SetRandomTransform : MonoBehaviour
{
	public GameObject GO = null;
	public Vector3 setPosMin = Vector3.zero;
	public Vector3 setPosMax = Vector3.zero;
	public Vector3 setRotationMin = Vector3.zero;
	public Vector3 setRotationMax = Vector3.zero;
	public Vector3 setScaleMin = Vector3.zero;
	public Vector3 setScaleMax = Vector3.zero;

	public bool dontSetZeros = false;
	public bool addInsteadOfSet = false;
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
				if (!dontSetZeros || (dontSetZeros && (setPosMin.x != 0 || setPosMax.x != 0))) { xa.glx.x = Random.Range(setPosMin.x, setPosMax.x); }
				if (!dontSetZeros || (dontSetZeros && (setPosMin.y != 0 || setPosMax.y != 0))) { xa.glx.y = Random.Range(setPosMin.y, setPosMax.y); }
				if (!dontSetZeros || (dontSetZeros && (setPosMin.z != 0 || setPosMax.z != 0))) { xa.glx.z = Random.Range(setPosMin.z, setPosMax.z); }

				if (addInsteadOfSet) { GO.transform.position += xa.glx; }
				else { GO.transform.position = xa.glx; }
			}
			if (useSetRotation)
			{
				xa.glx = GO.transform.localEulerAngles;
				if (!dontSetZeros || (dontSetZeros && (setRotationMin.x != 0 || setRotationMax.x != 0))) { xa.glx.x = Random.Range(setRotationMin.x, setRotationMax.x); }
				if (!dontSetZeros || (dontSetZeros && (setRotationMin.y != 0 || setRotationMax.y != 0))) { xa.glx.y = Random.Range(setRotationMin.y, setRotationMax.y); }
				if (!dontSetZeros || (dontSetZeros && (setRotationMin.z != 0 || setRotationMax.z != 0))) { xa.glx.z = Random.Range(setRotationMin.z, setRotationMax.z); }

				if (addInsteadOfSet) { GO.transform.localEulerAngles += xa.glx; }
				else { GO.transform.localEulerAngles = xa.glx; }
			}
			if (useSetScale)
			{
				xa.glx = GO.transform.localScale;
				if (!dontSetZeros || (dontSetZeros && (setScaleMin.x != 0 || setScaleMax.x != 0))) { xa.glx.x = Random.Range(setScaleMin.x, setScaleMax.x); }
				if (!dontSetZeros || (dontSetZeros && (setScaleMin.y != 0 || setScaleMax.y != 0))) { xa.glx.y = Random.Range(setScaleMin.y, setScaleMax.y); }
				if (!dontSetZeros || (dontSetZeros && (setScaleMin.z != 0 || setScaleMax.z != 0))) { xa.glx.z = Random.Range(setScaleMin.z, setScaleMax.z); }
				if (addInsteadOfSet) { GO.transform.localScale += xa.glx; }
				else { GO.transform.localScale = xa.glx; }
			}
			this.enabled = false;
		}
	}
}
