using UnityEngine;
using System.Collections;

public class ExploSpawnerScript : MonoBehaviour
{
	public float timer = 0;
	float counter;
	public GameObject explo1;
	void Start()
	{

	}

	void Update()
	{
		counter += 5 * fa.deltaTime;
		if (counter > timer)
		{
			counter = 0;

			xa.glx = transform.position;
			xa.tempobj = (GameObject)(Instantiate(explo1, xa.glx, transform.localRotation));
			xa.tempobj.transform.parent = xa.createdObjects.transform;
		}
	}
}
