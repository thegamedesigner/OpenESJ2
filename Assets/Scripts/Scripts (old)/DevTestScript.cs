using UnityEngine;
using System.Collections;

public class DevTestScript : MonoBehaviour
{
	public bool amOnScreen = false;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		amOnScreen = Setup.checkVecOnScreen(transform.position, true);
	}
}
