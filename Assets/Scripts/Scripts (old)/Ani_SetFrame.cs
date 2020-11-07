using UnityEngine;
using System.Collections;

public class Ani_SetFrame : MonoBehaviour 
{

	public int x = 0;
	public int y = 0;
	public int xRan = 0;
	public int xRanMin = 0;
	public bool use32x32Sheet = false;
	public bool use16x16Sheet = false;
	public bool use4x4Sheet = false;
	float multi = 0;
	public bool updateFrame = false;

	void Start()
	{
		multi = 1;
		if (use32x32Sheet) { multi = 0.25f; }
		if (use16x16Sheet) { multi = 0.5f; }
		if (use4x4Sheet) { multi = 2f; }
		if (xRan != 0)
		{
			Random.InitState((int)(xa.fakeRandom + xa.fakeRandom * xa.fakeRandom + Time.realtimeSinceStartup));
			int index = 100;
			while (index > 0)
			{
				index -= 1;
				xa.fakeRandom += (int)(1 * Time.realtimeSinceStartup);
				index--;
			}
			x = (int)(Random.Range(xRanMin, xRan));
		}
		setTexture(x, y);
	}

	void Update()
	{
		if (updateFrame) { setTexture(x, y); }
	}
	public void resetTexture(Vector3 v)
	{
		setTexture((int)v.x, (int)v.y);
	}

	void setTexture(int v1, int v2)
	{
		float x1 = 0;
		float y1 = 0;
		float x2 = 0;
		float y2 = 0;

		x1 = 0.125f * multi;
		y1 = 0.125f * multi;
		x2 = 0.125f * multi * v1;
		y2 = 1 - ((0.125f * multi * v2) + (0.125f * multi));

		this.gameObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2(x1, y1);
		this.gameObject.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x2, y2);
	}
}
