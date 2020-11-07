using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Structs;

public class FreshAni : MonoBehaviour
{
	public int pxWidth = 2048;//set to size of sheet
	public GameObject go;
	public MeshFilter meshFilter;
	public int playOnStart = -1;
	public bool dontUseFaTime = false;
	public Animation[] animations = new Animation[0];
	[HideInInspector]
	public int currentAniId = -1;
	int currentAni = -1;

	public enum EndOfAnimation
	{
		None,
		Loop,
		Stick,//Stick on the last frame of the animation
		Switch,
		End
	}

	[System.Serializable]
	public class Animation
	{
		public string label = "";
		public int id = -1;
		public int switchToId = -1;
		public EndOfAnimation endOfAnimation;
		public Frame[] frames = new Frame[0];
		[HideInInspector]
		public int currentFrame = 0;
		[HideInInspector]
		public float timeSet = 0;
		[HideInInspector]
		public float delay = 0;

	}

	public void PlayAnimation(int id)
	{
		for (int i = 0; i < animations.Length; i++)
		{
			if (animations[i].id == id)
			{
				animations[i].currentFrame = 0;		
				animations[i].delay = -1;		
				animations[i].timeSet = 0;		
				currentAni = i;
				currentAniId = id;
				//Debug.Log("Found animation. Playing: " + id);
			}
		}
	}

	void Start()
	{
		if (playOnStart != -1)
		{
			PlayAnimation(playOnStart);
		}
	}

	void Update()
	{
		float localTime = fa.time;
		if(dontUseFaTime) {localTime = Time.time; }
		if (currentAni != -1)
		{
			Animation ani = animations[currentAni];
			Frame frame = animations[currentAni].frames[ani.currentFrame];

			if(frame.time < 0)
			{
				if(frame.time == -1)
				{
					ani.currentFrame = Random.Range(0,ani.frames.Length);
				}
				SetFrame(ani);
				this.enabled = false;

			}
			else
			{
				if (localTime >= (ani.timeSet + ani.delay))
				{
					//then advance the frame
					ani.currentFrame++;
					ani.timeSet = localTime;

					if (ani.currentFrame >= ani.frames.Length)
					{
						//how do I handle the end of the animation?
						switch (ani.endOfAnimation)
						{
							case EndOfAnimation.Stick:
								ani.delay = -1;
								ani.currentFrame--;
								break;
							case EndOfAnimation.Loop:
								ani.currentFrame = 0;
								ani.delay = frame.time;
								break;
							case EndOfAnimation.Switch:
								ani.delay = -1;
								ani.currentFrame = 0;
								PlayAnimation(ani.switchToId);
								break;
						}
					}
					ani.delay = frame.time;

					SetFrame(ani);
				}
			}

		}
	}

	
	public void SetFrame(Animation ani)
	{
		if(ani.currentFrame >= ani.frames.Length) {Debug.Log("Ani error: Length: " + ani.frames.Length + ", index: " + ani.currentFrame); }
		SetTexture(ani.frames[ani.currentFrame].x, ani.frames[ani.currentFrame].y, go, ani.frames[ani.currentFrame].size, meshFilter);
	}
	
	Vector2[] newUVs = new Vector2[4];
	public void SetTexture(int x, int y, GameObject go, int frameSize, MeshFilter filter)
	{
		/* Unity Quad Lables
		 * 3 -- 1
		 * |	|
		 * 0 -- 2
		 */

		float cut = 0.0002f;//0.0002f;//Changed to 0.001 to fix Seb's black lines.
		//Old ones: //0.001f;//0.00005f;

		float origin_x = (float)x * frameSize;
		float origin_y = pxWidth - ((float)y + 1) * frameSize;

		newUVs[0].x = origin_x / pxWidth;
		newUVs[0].y = origin_y / pxWidth;

		newUVs[1].x = (origin_x + frameSize) / pxWidth;
		newUVs[1].y = (origin_y + frameSize) / pxWidth;

		newUVs[2].x = (origin_x + frameSize) / pxWidth;
		newUVs[2].y = origin_y / pxWidth;

		newUVs[3].x = origin_x / pxWidth;
		newUVs[3].y = (origin_y + frameSize) / pxWidth;

		//This fixes that texture overlapping problem. It cuts a tiny tiny amount of the all of the edges of the texture.
		newUVs[0].x += cut;
		newUVs[0].y += cut;
		newUVs[1].x -= cut;
		newUVs[1].y -= cut;
		newUVs[2].x -= cut;
		newUVs[2].y += cut;
		newUVs[3].x += cut;
		newUVs[3].y -= cut;

		Mesh mesh = filter.mesh;
		mesh.uv = newUVs;
	}

}
