using UnityEngine;
using System.Collections;

public class SwitchMatOnStart : MonoBehaviour
{
	public Material material;
	
	void Start()
	{
		GetComponent<Renderer>().material = material;
		this.enabled = false;
	}
}
