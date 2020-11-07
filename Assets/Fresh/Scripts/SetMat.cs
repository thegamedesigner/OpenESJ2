using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SetMat : MonoBehaviour
{
	public int x = 0;
	public int y = 0;
	public float frameSizeX = 16;
	public float frameSizeY = 16;
	public float sheetSizeX = 2048;
	public float sheetSizeY = 2048;
	public int numOfFramesInARow = 0;

	public bool cutEdge = false;

	void Update()
	{
		if(!cutEdge)
		{
			SetMaterial();
		}
		else
		{
			SetMatWithCutEdge();
		}
	}

	void SetMaterial()
	{
		if (GetComponent<Renderer>().sharedMaterial)
		{
			numOfFramesInARow = (int)(sheetSizeX / frameSizeX);
			Vector2 chunk = new Vector2(1f / (sheetSizeX / frameSizeX), 1f / (sheetSizeY / frameSizeY));
			GetComponent<Renderer>().sharedMaterial.mainTextureScale = chunk;
			GetComponent<Renderer>().sharedMaterial.mainTextureOffset = new Vector2(chunk.x * x, chunk.y * (numOfFramesInARow - 1 - y));
		}
	}


	void SetMatWithCutEdge()
	{
		float cut = 0.0001f;//0.0002f;//0.001f;//0.00005f;\
		if (GetComponent<Renderer>().sharedMaterial)
		{
			numOfFramesInARow = (int)(sheetSizeX / frameSizeX);
			Vector2 chunk = new Vector2(1f / (sheetSizeX / frameSizeX), 1f / (sheetSizeY / frameSizeY));


			GetComponent<Renderer>().sharedMaterial.mainTextureScale = new Vector2(chunk.x - (cut * 2), chunk.y - (cut * 2));
			GetComponent<Renderer>().sharedMaterial.mainTextureOffset = new Vector2((chunk.x * x) + (cut * 1), (chunk.y * (numOfFramesInARow - 1 - y)) + (cut * 1));
		}
	}
	/*
	Vector2[] newUVs = new Vector2[4];
	public void SetTexture(int x, int y, GameObject go, int frameSize, MeshFilter filter)
	{

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

		Mesh mesh = filter.sharedMesh;
		mesh.uv = newUVs;
	}
	*/
}
