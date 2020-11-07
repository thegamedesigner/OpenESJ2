using UnityEngine;
using System.Collections;

public class SetScale : MonoBehaviour
{
	public GameObject go = null;
	public Vector3 setTo = Vector3.zero;
    public bool dontTriggerOnEnabled = false;
	void Update()
	{
		if (this.enabled && !dontTriggerOnEnabled)
		{
			go.transform.localScale = setTo;
			this.enabled = false;
		}
	}

    public void SetScaleFunc()
    {
        go.transform.localScale = setTo;
    }
}
