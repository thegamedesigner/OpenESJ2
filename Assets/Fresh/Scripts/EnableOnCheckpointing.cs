using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnCheckpointing : MonoBehaviour
{
	public GameObject go;

	void Start()
	{
	}

	void Update()
	{
		if (xa.hasCheckpointed)
		{
			go.SetActive(true);
			this.enabled = false;
		}
	}
}
