using UnityEngine;
using System.Collections;

public class SetMatColourScript : MonoBehaviour
{
	public Color colour;

	void Start()
	{
		this.gameObject.GetComponent<Renderer>().material.color = colour;
	}
}
