using UnityEngine;
using System.Collections;

public class CreateScript : MonoBehaviour
{
	public GameObject createThisGO = null;
	public GameObject creationPoint = null;

	void Update()
	{
		if (this.enabled)
		{
			xa.tempobj = (GameObject)(Instantiate(createThisGO,creationPoint.transform.position,createThisGO.transform.rotation));
			this.enabled = false;
		}
	}
}
