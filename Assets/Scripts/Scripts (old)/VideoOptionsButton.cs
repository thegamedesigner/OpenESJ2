using UnityEngine;
using System.Collections;

public class VideoOptionsButton : MonoBehaviour
{
	/*public int x              = 0;
	public int y              = 0;
	public bool use16x16Sheet = false;
	public bool use4x4Sheet   = true;
	float multi               = 0;
	bool greyedOut            = false;
	bool changesMade          = false;

	void Start()
	{
		multi = 1;
		if (use16x16Sheet) { multi = 0.5f; }
		if (use4x4Sheet) { multi = 2f; }
		setTexture(x, y);
	}

	void Update()
	{
		if(!changesMade && (xa.setResolutionIndex != xa.currentResolutionIndex || xa.vSync != QualitySettings.vSyncCount || xa.fullscreen != Screen.fullScreen))
		{
			changesMade = true;
		}
		else if(changesMade && xa.setResolutionIndex == xa.currentResolutionIndex && xa.vSync == QualitySettings.vSyncCount && xa.fullscreen == Screen.fullScreen)
		{
			changesMade = false;
		}
	
		if(changesMade && greyedOut)
		{
			greyedOut = false;
			setTexture(x, y);
		}
		else if(!changesMade && !greyedOut)
		{
			greyedOut = true;
			setTexture(x+2, y);
		}
	}

	void setTexture(int v1, int v2)
	{
		Vector2 scale  = new Vector2(0.125f * multi, 0.125f * multi);
		Vector2 offset = new Vector2(0.125f * multi * v1, 1 - ((0.125f * multi * v2) + (0.125f * multi)));

		this.gameObject.renderer.material.mainTextureScale  = scale;
		this.gameObject.renderer.material.mainTextureOffset = offset;
	}*/
}
