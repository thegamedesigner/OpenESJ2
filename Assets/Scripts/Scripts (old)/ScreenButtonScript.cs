using UnityEngine;
using System.Collections;

public class ScreenButtonScript : MonoBehaviour
{
	public int buttonNumber = 0;
#if ANDROID || IOS
	bool oldOnGround = false;
#endif
	
	void Start()
	{
#if !ANDROID && !IOS
		transform.GetComponent<Renderer>().enabled = false;
#endif
	}

	void Update()
	{
#if ANDROID || IOS
		if (buttonNumber == 3)
		{
			if (oldOnGround != xa.playerOnGround)
			{
				oldOnGround = xa.playerOnGround;

				if (xa.playerOnGround)
				{
					renderer.material.mainTextureOffset = new Vector2(0.5f, 0);
				}
				else
				{
					renderer.material.mainTextureOffset = new Vector2(0.5f, 0.5f);
				}
			}
		}
#else
		return;
#endif // ANDROID || IOS
	}
}
