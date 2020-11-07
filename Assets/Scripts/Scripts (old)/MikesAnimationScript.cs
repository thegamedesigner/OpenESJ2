using UnityEngine;
using System.Collections;

public class MikesAnimationScript : MonoBehaviour
{

	public int x1 = 0;
	public int y1 = 0;
	public int numOfFrames1 = 0;
	public float speedOfAni1 = 0;
	public bool usePerFrameSpeeds1 = false;
	public float[] perFrameSpeeds1 = new float[8];

	public bool useAni2 = false;
	public int x2 = 0;
	public int y2 = 0;
	public int numOfFrames2 = 0;
	public float speedOfAni2 = 0;
	public bool useRandomChanceOfAni2PlayingOnce = false;
	public float randomChanceAni2 = 0;
	public bool usePerFrameSpeeds2 = false;
	public float[] perFrameSpeeds2 = new float[8];
	
	public bool useAni3 = false;
	public int x3 = 0;
	public int y3 = 0;
	public int numOfFrames3 = 0;
	public float speedOfAni3 = 0;
	public bool useRandomChanceOfAni3PlayingOnce = false;
	public float randomChanceAni3 = 0;
	public bool usePerFrameSpeeds3 = false;
	public float[] perFrameSpeeds3 = new float[8];

	bool needAni = true;
	//int currAni = 1;
	int aniFrameX = 0;
	int aniFrameY = 0;
	float counter = 0;
	//float result = 0;
	//bool hit = false;

	int currStartingPointX = 0;
	int currStartingPointY = 0;
	//int currMaxFrames = 0;
	float currAniSpd = 0;
	//int aniIndex = 0;


	void Start()
	{
		aniFrameX = x1;
		aniFrameY = y1;
		setTexture(aniFrameX, aniFrameY);
	}

	void Update()
	{
		if (needAni)
		{
			needAni = false;
			//hit = false;
			//aniIndex = 0;

			//default to playing animation 1
			//currAni = 1;
			currStartingPointX = x1;
			currStartingPointY = y1;
			//currMaxFrames = numOfFrames1;
			currAniSpd = speedOfAni1;

			aniFrameX = currStartingPointX;
			aniFrameY = currStartingPointY;
		}
			/*
			//random chance of ani2 playing
			if (useRandomChanceOfAni2PlayingOnce && !hit && useAni2)
			{
				result = Random.Range(0, 100);
				if (result < randomChanceAni2)
				{
					currAni = 2;
					currMaxFrames = numOfFrames2;
					currAniSpd = speedOfAni2;
					currStartingPointX = x2;
					currStartingPointY = y2;
					hit = true;
				}
			}

			//random change of ani3 playing
			if (useRandomChanceOfAni3PlayingOnce && !hit && useAni3)
			{
				result = Random.Range(0, 100);
				if (result < randomChanceAni3)
				{
					currAni = 3;
					currMaxFrames = numOfFrames3;
					currAniSpd = speedOfAni3;
					currStartingPointX = x3;
					currStartingPointY = y3;
					hit = true;
				}
			}
			*/

		counter += 10 * fa.deltaTime;
		if (counter >= currAniSpd)
		{
			counter = 0;
			aniFrameX++;
			setTexture(aniFrameX, aniFrameY);


			if (aniFrameX >= 2)
			{
				//done. Set need new ani
				needAni = true;
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
