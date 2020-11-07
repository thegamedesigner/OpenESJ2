using UnityEngine;
using System.Collections;

public class MoveJimShip : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		transform.Translate(0, 0, 5 * fa.deltaTime);
	}
}
