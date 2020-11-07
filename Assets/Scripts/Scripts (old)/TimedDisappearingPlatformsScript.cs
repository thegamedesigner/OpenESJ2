using UnityEngine;
using System.Collections;

public class TimedDisappearingPlatformsScript : MonoBehaviour 
{
	public GameObject objectToBlink;
	public float timeToDisappear = 5f;
	public float timeToReappear = 5f;
	public float startDelay = 0;
	public bool enableImmediatelyAfterDelay = true;

	//private bool enabled = false;
	private float timer = 0;
	
	void Start () 
	{
		if (objectToBlink == null)
		{
			//Debug.LogWarning("No object to blink. Destroying self!");
			Destroy(gameObject);
		}
		else
		{
			if (objectToBlink.activeSelf)
			{
				timer = timeToDisappear;
			}
			else
			{
				timer = timeToReappear;
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (startDelay > 0)
		{
			startDelay -= fa.deltaTime;

			if (startDelay <= 0 && enableImmediatelyAfterDelay)
			{
				objectToBlink.SetActive(true);
				timer = timeToDisappear;
			}

			return;
		}

		timer -= fa.deltaTime;
		if (timer <= 0)
		{
			objectToBlink.SetActive(!objectToBlink.activeSelf);
			if (objectToBlink.activeSelf)
			{
				timer = timeToDisappear;
			}
			else
			{
				timer = timeToReappear;
			}
		}
	}
}
