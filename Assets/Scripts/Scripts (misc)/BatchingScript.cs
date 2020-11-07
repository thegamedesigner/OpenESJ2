using UnityEngine;
using System.Collections;

public class BatchingScript : MonoBehaviour
{
	public Material batchingMat = null;
	public Color color = Color.white;
	Vector2 oldMainTextureScale = Vector2.zero;
	Vector2 oldMainTextureOffset = Vector2.zero;


	void Awake()
	{
		if (batchingMat)
		{
			//get offsets
			oldMainTextureScale = GetComponent<Renderer>().material.mainTextureScale;
			oldMainTextureOffset = GetComponent<Renderer>().material.mainTextureOffset;

			//Set Material
			GetComponent<Renderer>().material = batchingMat;
			setTexture(oldMainTextureScale, oldMainTextureOffset);

		}

		//set color
	//	MaterialPropertyBlock materialProperty = new MaterialPropertyBlock();
		//materialProperty.SetColor("_Color", color);
	//	GetComponent<Renderer>().SetPropertyBlock(materialProperty);

		//renderer.material.mainTextureScale = oldMainTextureScale;
		//renderer.material.mainTextureOffset = oldMainTextureOffset;
	}

	public void setTexture(Vector2 scale, Vector2 offset)
	{
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
	}
}
