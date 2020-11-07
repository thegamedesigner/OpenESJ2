using UnityEngine;
using System.Collections;

public class InfLoveSpawner : MonoBehaviour
{
	public GameObject spawnThis = null;
	public int minPortalsToSpawn = 0;
	public int maxPortalsToSpawn = -1; // -1 means no maximum

	void Start()
	{
		this.gameObject.SetActive(false);
		if(GetComponent<Renderer>())
			GetComponent<Renderer>().enabled = false;
	}

	void Update()
	{
		if (this.gameObject.activeSelf && xa.portalStep >= minPortalsToSpawn && (maxPortalsToSpawn == -1 || xa.portalStep <= maxPortalsToSpawn))
		{
			xa.glx = transform.position;
			xa.glx.z = xa.GetLayer(xa.layers.Invisible);
			xa.tempobj = Instantiate(spawnThis, transform.position, transform.rotation) as GameObject;
			xa.tempobj.transform.parent = xa.createdObjects.transform;
			xa.onScreenObjectsDirty = true;
	
			this.gameObject.SetActive(false);
		}
	}
}
