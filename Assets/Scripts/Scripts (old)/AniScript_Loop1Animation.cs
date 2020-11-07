using UnityEngine;
using System.Collections;

public class AniScript_Loop1Animation : MonoBehaviour
{
	/*
	 * Loops one animation over and over
	 * */
	public int x1             = 0;
	public int y1             = 0;
	public int numOfFrames1   = 0;
	public float speedInSeconds      = 0;
	public bool use16x16Sheet = false;
	public bool use4x4Sheet   = false;

	float multi               = 0;
	int aniFrameX             = 0;
	int aniFrameY             = 0;
	int aniIndex              = 0;

    float timeSet = 0;


	void Start()
	{
		multi = 1;
		if (use16x16Sheet) { multi = 0.5f; }
		if (use4x4Sheet) { multi = 2f; }
		aniFrameX = x1;
		aniFrameY = y1;
		setTexture(aniFrameX, aniFrameY);
	}

	void Update()
	{
		if (fa.time >= (timeSet + speedInSeconds))
		{
            timeSet = fa.time;
			aniFrameX++;
			aniIndex++;
			if (aniIndex >= numOfFrames1)
			{
				aniFrameX = x1;
				aniIndex = 0;
			}
			setTexture(aniFrameX, aniFrameY);
		}
	}
	
	public void changeAnimation(int x, int y, int frames)
	{
		x1 = aniFrameX = x;
		y1 = aniFrameY = y;
		numOfFrames1 = frames;
		aniIndex = 0;
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
