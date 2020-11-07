using UnityEngine;

public class BossDiamondControllerScript : MonoBehaviour
{
	float counter = 0;
	public float creationSpeed = 0;
	public GameObject createThis;
	void Start()
	{

	}

	void Update()
	{
		counter += 10 * fa.deltaTime;
		if (counter > creationSpeed)
		{
			counter = 0;

			xa.glx = transform.position;
			xa.glx.z = xa.GetLayer(xa.layers.Explo1);
			xa.tempobj = (GameObject)(Instantiate(createThis, xa.glx, transform.localRotation));
			xa.tempobj.transform.parent = xa.createdObjects.transform;
		}
	}
}
