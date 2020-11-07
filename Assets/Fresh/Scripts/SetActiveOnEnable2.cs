using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveOnEnable2 : MonoBehaviour
{

	public Item[] items;

	[System.Serializable]
	public class Item
	{
		public string label;
		public GameObject go;
		public bool setTo = false;
	}
	void Update()
	{
		for (int i = 0; i < items.Length; i++)
		{
			if (items[i].go != null)
			{
				items[i].go.SetActive(items[i].setTo);
			}
		}
		this.enabled = false;

	}
}
