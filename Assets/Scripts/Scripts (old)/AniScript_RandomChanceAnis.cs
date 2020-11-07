using UnityEngine;
using System.Collections;

public class AniScript_RandomChanceAnis : MonoBehaviour
{
	/*
	 * Loops animation 1,
	 * but has a random chance out of 100
	 * to play Animation 2 or 3, each time
	 * animation 1 loops
	 * */
	public int x1 = 0;
	public int y1 = 0;
	public int numOfFrames1 = 0;
	public float speed1 = 0;

	public bool useAni2 = false;
	public int x2 = 0;
	public int y2 = 0;
	public int numOfFrames2 = 0;
	public float speed2 = 0;
	public float randomChance2 = 0;

	public bool useAni3 = false;
	public int x3 = 0;
	public int y3 = 0;
	public int numOfFrames3 = 0;
	public float speed3 = 0;
	public float randomChance3 = 0;

	public bool use16x16Sheet = false;
	public bool use4x4Sheet = false;
	float multi = 0;

	public bool dontTriggerIfGlorgLoveIsTrue = false;

	int currAni = 0;
	int aniFrameX = 0;
	int aniFrameY = 0;
	float counter = 0;
	int aniIndex = 0;
	bool justLooped = false;


	void Start()
	{
		multi = 1;
		if (use16x16Sheet) { multi = 0.5f; }
		if (use4x4Sheet) { multi = 2f; }

		currAni = 1;
		aniFrameX = x1;
		aniFrameY = y1;
		setTexture(aniFrameX, aniFrameY);

	}

	void Update()
	{
		if (justLooped)
		{
			justLooped = false;
			counter = 0;
			if (currAni != 1)
			{
				currAni = 1; //go back to ani1
			}
			else
			{
				currAni = 1;
				if (useAni2 && (!dontTriggerIfGlorgLoveIsTrue || (dontTriggerIfGlorgLoveIsTrue && !xa.haltForSecondGlorg)))
				{
					if (Random.Range(0, 100) <= randomChance2)
					{
						currAni = 2;
					}
				}
				if (useAni3 && (!dontTriggerIfGlorgLoveIsTrue || (dontTriggerIfGlorgLoveIsTrue && !xa.haltForSecondGlorg)))
				{
					if (Random.Range(0, 100) <= randomChance3)
					{
						currAni = 3;
					}
				}
			}

			if (currAni == 1)
			{
				aniFrameX = x1;
				aniFrameY = y1;
			}
			if (currAni == 2)
			{
				aniFrameX = x2;
				aniFrameY = y2;
			}
			if (currAni == 3)
			{
				aniFrameX = x3;
				aniFrameY = y3;
			}
			setTexture(aniFrameX, aniFrameY);
		}


		if (currAni == 1)
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
					justLooped = true;
				}
				setTexture(aniFrameX, aniFrameY);
			}
		}
		if (currAni == 2)
		{
			counter += 10 * fa.deltaTime;
			if (counter >= speed2)
			{
				counter = 0;
				aniFrameX++;
				aniIndex++;
				if (aniIndex >= numOfFrames2)
				{
					aniFrameX = x2;
					aniIndex = 0;
					justLooped = true;
				}
				setTexture(aniFrameX, aniFrameY);
			}
		}
		if (currAni == 3)
		{
			counter += 10 * fa.deltaTime;
			if (counter >= speed3)
			{
				counter = 0;
				aniFrameX++;
				aniIndex++;
				if (aniIndex >= numOfFrames3)
				{
					aniFrameX = x3;
					aniIndex = 0;
					justLooped = true;
				}
				setTexture(aniFrameX, aniFrameY);
			}
		}
	}

	void setTexture(int v1, int v2)
	{
		float x1 = 0;
		float y1 = 0;
		float x2 = 0;
		float y2 = 0;

		x1 = 0.125f * multi;
		y1 = 0.125f * multi;
		x2 = (0.125f * multi) * v1;
		y2 = 1 - (((0.125f * multi) * v2) + (0.125f * multi));

		this.gameObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2(x1, y1);
		this.gameObject.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x2, y2);
	}
}
