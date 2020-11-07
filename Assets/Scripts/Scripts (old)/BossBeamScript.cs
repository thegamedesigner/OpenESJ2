using UnityEngine;
using System.Collections;

public class BossBeamScript : MonoBehaviour
{
	public float speed = 0;
	void Start()
	{

	}

	void Update()
	{
		xa.glx = transform.position;
		xa.glx.x += speed * fa.deltaTime;
		transform.position = xa.glx;
	}
}
