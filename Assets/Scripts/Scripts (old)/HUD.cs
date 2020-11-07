using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
	public void OnGUI()
	{
		Texture2D localTexture = (xa.de)? xa.de.arrowCursor : null;
		if (localTexture != null)
		{
			GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 32, 32), localTexture);
		}
	}
}
