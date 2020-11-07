using UnityEngine;
using System.Collections;

public class StopAllItweensWithName : MonoBehaviour
{
	new public string name = "";
	void Update()
	{
		iTween.StopByName(name);
		this.enabled = false;
	}
}
