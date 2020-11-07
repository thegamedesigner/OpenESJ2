using UnityEngine;
using System.Collections;

public class FlipTwoRenderers : MonoBehaviour
{
	public GameObject go1 = null;
	public GameObject go2 = null;
	public bool setInsteadOfFlip = false;
	public bool set1 = false;
	public bool set2 = false;
	void Update()
	{
		if (this.enabled)
		{
			if (setInsteadOfFlip)
			{
				go1.GetComponent<Renderer>().enabled = set1;
				go2.GetComponent<Renderer>().enabled = set2;
			}
			else
			{
				if (go1.GetComponent<Renderer>().enabled)
				{
					go1.GetComponent<Renderer>().enabled = false;
					go2.GetComponent<Renderer>().enabled = true;
				}
				else
				{
					go1.GetComponent<Renderer>().enabled = true;
					go2.GetComponent<Renderer>().enabled = false;
				}
			}
			this.enabled = false;
		}
	}
}
