using UnityEngine;
using System.Collections;

public class AniScript_TriggeredAni : MonoBehaviour
{
	/*
	 * Loops animation 1,
	 * but a GunScript will trigger Animation 2 to be played once
	 *
	 * Each frame of Ani 2 has it's own speed, so that it can match the firing pattern exactly
	 * */


	public int x1 = 0;
	public int y1 = 0;
	public int numOfFrames1 = 0;
	public float speed1 = 0;

	public int x2 = 0;
	public int y2 = 0;
	public int numOfFrames2 = 0;
	public float[] perFrameSpeed2 = new float[8];

	public bool forceUseAni3OnMusicTime = false;
	public float start = 0;
	public float stop = 0;
	public int x3 = 0;
	public int y3 = 0;
	public int numOfFrames3 = 0;
	public float[] perFrameSpeed3 = new float[8];
	bool setToThirdAni = false;

	int currAni = 0;
	int aniFrameX = 0;
	int aniFrameY = 0;
	float counter = 0;
	int aniIndex = 0;


	void Start()
	{
		currAni = 1;
		aniFrameX = x1;
		aniFrameY = y1;
		setTexture(aniFrameX, aniFrameY);

	}

	public void triggerAni2()
	{
		currAni = 2;
		aniFrameX = x2;
		aniFrameY = y2;
		counter = 0;
		aniIndex = 0;
		setTexture(aniFrameX, aniFrameY);
	}

	void Update()
	{
	   // if (Input.GetKeyDown(KeyCode.M))
	   // {
		//	triggerAni2();//dev
		//}

		if (forceUseAni3OnMusicTime && xa.music_Time > stop && setToThirdAni)
		{
			setToThirdAni = false;
			aniFrameX = x1;
			aniFrameY = y1;
			counter = 0;
			aniIndex = 0;
			setTexture(aniFrameX, aniFrameY);
		}
		if (forceUseAni3OnMusicTime && xa.music_Time >= start && xa.music_Time <= stop)
		{
			if (setToThirdAni == false)
			{
				setToThirdAni = true;
				aniFrameX = x3;
				aniFrameY = y3;
				counter = 0;
				aniIndex = 0;
				setTexture(aniFrameX, aniFrameY);
			}
			counter += 10 * fa.deltaTime;
			if (counter >= perFrameSpeed3[aniFrameX])
			{
				counter = 0;
				aniFrameX++;
				aniIndex++;
				if (aniIndex >= numOfFrames3)
				{
					aniFrameX = x3;
					aniIndex = 0;
				}
				setTexture(aniFrameX, aniFrameY);
			}


		}
		else
		{

			setToThirdAni = false;

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
					}
					setTexture(aniFrameX, aniFrameY);
				}
			}
			if (currAni == 2)
			{
				counter += 10 * fa.deltaTime;
				if (counter >= perFrameSpeed2[aniFrameX])
				{
					counter = 0;
					aniFrameX++;
					aniIndex++;
					if (aniIndex >= numOfFrames2)
					{
						//revert back to animation 1
						aniFrameX = x1;
						aniFrameY = y1;
						aniIndex = 0;
						counter = 0;
						currAni = 1;
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
