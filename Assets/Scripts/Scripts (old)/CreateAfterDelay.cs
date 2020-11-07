using UnityEngine;
using System.Collections;

public class CreateAfterDelay : MonoBehaviour
{
	public GameObject go;
	public float delayInSeconds = 0;
	float timeSave = 0;

	void Start()
	{
		timeSave = fa.time;
	}

	void Update()
	{
		if (fa.time >= (timeSave + delayInSeconds))
		{
			Instantiate(go, transform.position, xa.null_quat);
		}
	}
}
