using UnityEngine;
using System.Collections;

public class AniScript_playOnce : MonoBehaviour
{
	public int x1 = 0;
	public int y1 = 0;
	public int numOfFrames1 = 0;
	public float speed1 = 0;
	public float delay = 0;

	int aniFrameX = 0;
	int aniFrameY = 0;
	float counter = 0;
	int aniIndex = 0;
	float delayCounter = 0;
	public int playing = 0;

	void Start()
	{
		aniFrameX = x1;
		aniFrameY = y1;
		setTexture(aniFrameX, aniFrameY);
	}

	void Update()
	{
		if (playing > 0)
		{
			delayCounter -= 10 * fa.deltaTime;
			if (delayCounter <= 0)
			{
				counter += 10 * fa.deltaTime;
				if (counter >= speed1)
				{
					counter = 0;
					aniFrameX++;
					aniIndex++;
					if (aniIndex >= numOfFrames1)
					{
						aniFrameX = x1;
						aniIndex = 0;
						delayCounter = delay;
						playing--;
					}
					setTexture(aniFrameX, aniFrameY);
				}
			}
		}
	}

	void setTexture(int v1, int v2)
	{
		float x1 = 0;
		float y1 = 0;
		float x2 = 0;
		float y2 = 0;

		x1 = 0.125f;
		y1 = 0.125f;
		x2 = 0.125f * v1;
		y2 = 1 - ((0.125f * v2) + 0.125f);

		this.gameObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2(x1, y1);
		this.gameObject.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x2, y2);
	}
}
