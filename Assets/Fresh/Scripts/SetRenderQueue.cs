using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRenderQueue : MonoBehaviour
{
	public Material mat;
	public int renderQueue = 3000;
	void Start()
	{
		mat.renderQueue = renderQueue;
	}

}
