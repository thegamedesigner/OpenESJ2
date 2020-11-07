using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Structs;

public class InEditorDetailBlocks : MonoBehaviour
{
	public Material batchmat;
	public FreshDetailBlocks.Type type = FreshDetailBlocks.Type.Invisible;
	public DetailBlockInfo.DetailType style = DetailBlockInfo.DetailType.PinkMechanical;
	public bool Size16 = false;

	public static int pxWidth = 2048;
	Vector2 oldMainTextureScale = Vector2.zero;
	Vector2 oldMainTextureOffset = Vector2.zero;

	int x = 0;
	int y = 0;
	float frameSizeX = 8;
	float frameSizeY = 8;
	float sheetSizeX = 2048;
	float sheetSizeY = 2048;

	void Start()
	{
		if (style == DetailBlockInfo.DetailType.Slime) { Size16 = true; }
		if (style == DetailBlockInfo.DetailType.MegaHell) { Size16 = true; }
		if (style == DetailBlockInfo.DetailType.Jungle) { Size16 = true; }
		if (style == DetailBlockInfo.DetailType.BlueJungle) { Size16 = true; }
		transform.localScale = new Vector3(1.1f,1.1f,1.1f);
	}

	void Update()
	{
		if (Size16) { transform.SetScaleX(2); transform.SetScaleY(2); }
		GetComponent<Renderer>().material = batchmat;
		//set x & y, from detail block info

		if (DetailBlockInfo.info == null) { /*Debug.Log("Info was null. Haven't gone to level zero yet. Dont do anything.");*/ }
		else
		{
			Frame frame = DetailBlockInfo.GetFrameForType(style, type);

			if (type == FreshDetailBlocks.Type.NSWE && style == DetailBlockInfo.DetailType.Slime)
			{
				//Debug.Log("4-sided slime: x: " + frame.x + ", y: " + frame.y);
			}

			x = frame.x;
			y = frame.y;
			frameSizeX = frame.size;
			frameSizeY = frame.size;

			//frameSizeX += 2;//For spreading the frames out
			//frameSizeY += 2;//

			int numOfFramesInARow = (int)(sheetSizeX / frameSizeX);
			Vector2 chunk = new Vector2(1f / (sheetSizeX / frameSizeX), 1f / (sheetSizeY / frameSizeY));
			oldMainTextureScale = chunk;
			oldMainTextureOffset = new Vector2(chunk.x * x, chunk.y * (numOfFramesInARow - 1 - y));

			//get offsets
			//oldMainTextureScale = GetComponent<Renderer>().material.mainTextureScale;
			//oldMainTextureOffset = GetComponent<Renderer>().material.mainTextureOffset;

			//Set Material
			//setTexture(oldMainTextureScale, oldMainTextureOffset);
			MeshFilter filter = this.gameObject.GetComponent<MeshFilter>();
			SetTexture(x, y, this.gameObject, (int)frameSizeX, filter);

		}

		this.enabled = false;
	}



	static Vector2[] newUVs = new Vector2[4];
	public static void SetTexture(int x, int y, GameObject go, int frameSize, MeshFilter filter)
	{
		/* Unity Quad Lables
		 * 3 -- 1
		 * |	|
		 * 0 -- 2
		 */

		float cut = 0.0002f;//0.0002f;//0.001f;//0.00005f;\

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
	/*
	public void setTexture(Vector2 scale, Vector2 offset)
	{
		//Debug.Log("scale.x: " + scale.x + ", scale.y: " + scale.y + ", offset.x: " + offset.x + ", offset.y: " + offset.y);
		
		MeshFilter filter = this.gameObject.GetComponent<MeshFilter>();
		if (filter != null)
		{



			Mesh mesh = filter.mesh;
			Vector2[] newUVs = new Vector2[mesh.uv.Length];
			int i = 0;

			foreach (Vector2 coordinate in mesh.uv)
			{
				newUVs[i] = new Vector2(coordinate.x * scale.x + offset.x, coordinate.y * scale.y + offset.y);
				i++;
			}

			mesh.uv = newUVs;
		}
	}*/


}
