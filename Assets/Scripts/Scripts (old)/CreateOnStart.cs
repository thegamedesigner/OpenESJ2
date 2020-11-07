using UnityEngine;
using System.Collections;

public class CreateOnStart : MonoBehaviour
{
	public GameObject[] createThese;
	public bool makeChild = false;
	public GameObject parentObject;
	public bool useSpawnAngle = false;
	public float spawnAngle = 0;

	void Update()
	{
		if (this.enabled)
		{
			foreach (GameObject go in createThese)
			{
				xa.glx = transform.position;
				xa.glx.z = -500;//behind the camera

				xa.tempobj = (GameObject)(Instantiate(go, xa.glx, transform.rotation));
				if (useSpawnAngle)
				{
					xa.glx = xa.tempobj.transform.localEulerAngles;
					xa.glx.z = spawnAngle;
					xa.tempobj.transform.localEulerAngles = xa.glx;

				}
				if (makeChild)
				{
					if(parentObject) xa.tempobj.transform.parent = parentObject.transform;
					else xa.tempobj.transform.parent = transform;
				}
			}
			this.enabled = false;
		}
	}

}
