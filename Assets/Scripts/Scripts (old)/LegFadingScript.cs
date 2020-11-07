using UnityEngine;
using System.Collections;

public class LegFadingScript : MonoBehaviour
{

	void Start()
	{

	}

	void Update()
	{
		xa.tempColor = this.gameObject.GetComponent<Renderer>().material.color;
		xa.tempColor.a -= 0.1f * fa.deltaTime;
		if (xa.tempColor.a <= 0)
		{
			GameObject.Destroy(transform.parent.gameObject);
		}
		this.gameObject.GetComponent<Renderer>().material.color = xa.tempColor;
	}
}
