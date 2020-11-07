using UnityEngine;
using System.Collections;

public class DestroyGOOnAlphaScript : MonoBehaviour
{
	public GameObject checkAlphaOfThisGO;
	public GameObject destoryThisGO;

	public float delay = 0;

	void Start()
	{

	}

	void Update()
	{
		if (delay > 0)
		{
			delay -= 10 * fa.deltaTime;
		}
		else
		{
			if (checkAlphaOfThisGO.GetComponent<Renderer>().material.color.a <= 0)
			{
				Destroy(destoryThisGO);
			}
		}
	}
}
