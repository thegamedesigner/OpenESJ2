using UnityEngine;
using System.Collections;

public class SpawnSomethingOnce : MonoBehaviour
{
	public GameObject something = null;
	public bool takeAngleFromParent = false;
	public bool takeAngleFromSpawnPoint = false;
	public GameObject spawnPoint = null;
	void Update()
	{
		xa.glx = transform.position;
		if (spawnPoint != null) { xa.glx = spawnPoint.transform.position; }
		xa.tempobj = (GameObject)(Instantiate(something, xa.glx, xa.null_quat));
		if (takeAngleFromParent)
		{
			xa.tempobj.transform.localEulerAngles = transform.localEulerAngles;
		}
		if (takeAngleFromSpawnPoint)
		{
			xa.tempobj.transform.localEulerAngles = spawnPoint.transform.localEulerAngles;
		}
		//xa.tempobj.transform.parent = xa.createdObjects.transform;
		this.enabled = false;
	}

}
