using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour
{

	public float speed = 0; //lower is faster
	public bool flipped = false;
	public int frameNumber = 0;
	public bool reverseAnimationDirectionOnEachLoop = false;



	float timer = 0;
	int frame = 0;
	public bool aniDirection = false;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		animateMe();
	}


	void animateMe()
	{

		timer += 10 * fa.deltaTime;
		if (timer > speed)
		{
			timer = 0;

			frame += 1;
			if (frame >= frameNumber)
			{
				frame = 0;
				if (reverseAnimationDirectionOnEachLoop)
				{
					aniDirection = !aniDirection;
				}
			}

			int result = 0;
			result = frame;

			if (aniDirection && reverseAnimationDirectionOnEachLoop)//if we're doing this, and aniDirection is true, reverse frame number (0 becomes 7, for an 8 frame animation)
			{
			   result = frameNumber - frame - 1;
			}

			//of course, if reverseAnimationDirectionetcetc hasn't been check'd, then the result is still EXACTLY equal to frame right now. Good.

			setTexture(result, 0, this.gameObject);
		}
	}


	void setTexture(int v1, int v2, GameObject go)
	{
		float x1 = 0;
		float y1 = 0;
		float x2 = 0;
		float y2 = 0;

		x1 = 0.125f;
		y1 = 0.125f;
		x2 = 0.125f * v1;
		y2 = 1 - ((0.125f * v2) + 0.125f);

		if (!flipped)
		{
			go.GetComponent<Renderer>().material.mainTextureScale = new Vector2(x1, y1);
			go.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x2, y2);
		}
		else
		{
			go.GetComponent<Renderer>().material.mainTextureScale = new Vector2(-x1, y1);
			go.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x2 + 0.125f, y2);
		}
	}


}
