using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetControlsButtonsList : MonoBehaviour
{
	public Item[] items = new Item[0];

	[System.Serializable]
	public class Item
	{
		public string label;
		public int id = 1;
		public Controls.Type type;
		public Button button;
		public Text buttonText;
	}

}
