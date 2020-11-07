using UnityEngine;
using System.Collections;

public class MerpsUseKey : MonoBehaviour
{
	GameObject[] gos;
	float useDist = 3;

	void Start()
	{

	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			gos = GameObject.FindGameObjectsWithTag("merpsUseable");
			foreach (GameObject go in gos)
			{
				xa.glx = go.transform.position;
				xa.glx.z = transform.position.z;
				if (Vector3.Distance(xa.glx, transform.position) < useDist)
				{
					go.SendMessage("enableBehaviour");
				}
			}
		}
	}
}
