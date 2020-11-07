using UnityEngine;
using System.Collections;

public class FloatingTextScript : MonoBehaviour
{
	public string line1 = "";
	public string line2 = "";
	public string line3 = "";
	TextMesh textMesh;

	void Start()
	{
		textMesh = this.gameObject.GetComponent<TextMesh>();
		textMesh.text = line1 + "\n" + line2 + "\n" + line3;
	}
}
