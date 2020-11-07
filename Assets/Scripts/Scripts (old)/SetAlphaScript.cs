using UnityEngine;
using System.Collections;

public class SetAlphaScript : MonoBehaviour
{
	public float alpha;//Range is 0 to 1
	void Start()
	{
		xa.tempColor = transform.GetComponent<Renderer>().material.color;
		xa.tempColor.a = alpha;
		transform.GetComponent<Renderer>().material.color = xa.tempColor;
	}
}
