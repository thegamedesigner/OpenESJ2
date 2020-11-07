using UnityEngine;
using System.Collections;

public class PopeDestroyWhenOffscreen : MonoBehaviour
{

	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (transform.position.x < Camera.main.GetComponent<Camera>().transform.position.x - 20)
		{
			Destroy(this.gameObject);
		}
		else if (transform.position.x > Camera.main.GetComponent<Camera>().transform.position.x + 20)
		{
			Destroy(this.gameObject);
		}
	}
}
