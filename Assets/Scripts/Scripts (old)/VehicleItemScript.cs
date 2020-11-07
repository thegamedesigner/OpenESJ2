using UnityEngine;
using System.Collections;

public class VehicleItemScript : MonoBehaviour
{
	public float triggerDist = 0;
	public GameObject vehiclePrefab = null;
	public GameObject playerVanishExplo = null;

	void Start()
	{

	}

	void Update()
	{
		if (xa.player)
		{
			if (Vector3.Distance(xa.player.transform.position, new Vector3(transform.position.x, transform.position.y, xa.player.transform.position.z)) < triggerDist)
			{
				Destroy(xa.player);
				xa.glx = transform.position;
				xa.glx.z = xa.GetLayer(xa.layers.Invisible);
				Instantiate(playerVanishExplo, xa.glx, xa.null_quat);
				xa.player = (GameObject)(Instantiate(vehiclePrefab, xa.glx, xa.null_quat));
				Destroy(this.gameObject);
			}
		}
	}
}
