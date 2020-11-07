using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSlash : MonoBehaviour
{
	public GameObject[] blackSqs;
	public static ScreenSlash self;
	public static GameObject screenSlashObj;

	void Awake()
	{
		screenSlashObj = this.gameObject;
		self = this;
		LocalScreenSlashOff();
	}

	public void LocalScreenSlashOn(float y)
	{
		transform.SetY(y);
		for (int i = 0; i < blackSqs.Length; i++)
		{
			blackSqs[i].GetComponent<MeshRenderer>().enabled = true;
		}
	}

	public void LocalScreenSlashOff()
	{
		for (int i = 0; i < blackSqs.Length; i++)
		{
			blackSqs[i].GetComponent<MeshRenderer>().enabled = false;
		}
	}

	public static void ScreenSlashOn(float y)
	{
		if (self != null)
		{
			self.LocalScreenSlashOn(y);
		}
	}

	public static void ScreenSlashOff()
	{
		if (self != null)
		{
			self.LocalScreenSlashOff();
		}
	}

	public static void Reset()
	{
		if (self != null)
		{
			self.LocalScreenSlashOff();
		}
	}
}
