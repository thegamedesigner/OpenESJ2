using UnityEngine;
using System.Collections;

public class CutsceneCharacterScript : MonoBehaviour
{
	public int stage = 0;
	public string[] strings;
	public Vector4[] animations;//x,y,number,speed
	public GameObject animatingObject;
	public GameObject textPoint;
	public GameObject textPrefab;
	public bool use16x16Sheet = false;
	public bool use4x4Sheet = false;
	int createdText = -1;
	int setAni = -1;
	TextMesh textMesh;
	int aniX = 0;
	int aniY = 0;
	int curX = 0;
	int curY = 0;
	int aniIndex = 0;
	float aniSpeed = 0;
	int aniFrames = 0;
	float multi = 0;
	float timeSave = 0;
	GameObject createdTextObj = null;

	void Start()
	{
		multi = 1;
		if (use16x16Sheet) { multi = 0.5f; }
		if (use4x4Sheet) { multi = 2f; }
		timeSave = fa.time;
	}

	void Update()
	{
		//create text
		if (createdText < stage)
		{
			createdText++;
			if (createdTextObj) { iTweenEvent.GetEvent(createdTextObj, "fadeOutVeryFast").Play(); }
			if (stage < strings.Length)
			{
				xa.tempobj = (GameObject)(Instantiate(textPrefab, textPoint.transform.position, xa.null_quat));
				textMesh = xa.tempobj.GetComponentInChildren<TextMesh>();
				textMesh.text = strings[stage];
				createdTextObj = textMesh.gameObject;
				if (xa.createdObjects) { xa.tempobj.transform.parent = xa.createdObjects.transform; }
			}

		}

		//set animation
		if (setAni < stage)
		{
			setAni++;
			if (stage < animations.Length)
			{
				aniX = (int)(animations[stage].x);
				aniY = (int)(animations[stage].y);
				aniFrames = (int)(animations[stage].z);
				aniSpeed = animations[stage].w;
				curX = aniX;
				curY = aniY;
			}
		}

		//play animation


		if ((timeSave + aniSpeed) <= fa.time)
		{
			timeSave = fa.time;
			curX++;
			aniIndex++;
			if (aniIndex >= aniFrames)
			{
				aniIndex = 0;
				curX = aniX;
	
			}
			setTexture(curX, curY);
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

		animatingObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2(x1, y1);
		animatingObject.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x2, y2);
	}
}
