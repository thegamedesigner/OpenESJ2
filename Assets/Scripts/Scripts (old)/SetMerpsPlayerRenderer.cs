using UnityEngine;
using System.Collections;

public class SetMerpsPlayerRenderer : MonoBehaviour
{
	void Start()
	{
		za.merpsPlayerRenderer = this.gameObject;
		this.enabled = false;
	}
}
