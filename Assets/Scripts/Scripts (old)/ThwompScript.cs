using UnityEngine;
using System.Collections;

public class ThwompScript : MonoBehaviour
{
	public float force = 0;
	public float grav = 0;
	public float airFriction = 10;
	
	//float startingY = 0;
	void Start()
	{
		//startingY = transform.position.y;
		//Setup.GC_DebugLog("Thwomped");
	}

	void Update()
	{
		force += grav * fa.deltaTime;

		xa.glx = transform.position;
		xa.glx.y -= force * fa.deltaTime;
		transform.position = xa.glx;
		//check if I've hit the ground
	}
}
