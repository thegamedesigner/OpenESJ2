using UnityEngine;
using System.Collections;

public class PopeHealthBarScript : MonoBehaviour
{
	void Start()
	{
		xa.popeHealth = 122;
	}
	void Update()
	{
		xa.glx = transform.localScale;
		xa.glx.y = xa.popeHealth;
		if (xa.glx.y <= 5) { xa.glx.y = 0; }
		if (xa.glx.y < 0) { xa.glx.y = 0; }
		transform.localScale = xa.glx;
	}
}
