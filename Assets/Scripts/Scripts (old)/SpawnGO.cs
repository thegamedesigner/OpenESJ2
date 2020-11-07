using UnityEngine;
using System.Collections;

public class SpawnGO : MonoBehaviour
{
	public GameObject go;
	public GameObject forceSpawnAtThisTransform = null;

	void Update()
	{
		if (this.enabled)
		{
			//spawn a prefab
			xa.glx = transform.position;
			if (forceSpawnAtThisTransform) { xa.glx = forceSpawnAtThisTransform.transform.position; }
			xa.tempobj = (GameObject)(Instantiate(go, xa.glx, xa.null_quat));
			if (xa.createdObjects) { xa.tempobj.transform.parent = xa.createdObjects.transform; }

			this.enabled = false;
		}
	}
}
