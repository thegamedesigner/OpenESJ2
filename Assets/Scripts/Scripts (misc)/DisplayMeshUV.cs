using UnityEngine;
using System.Collections;

public class DisplayMeshUV : MonoBehaviour
{
    MeshFilter filter;
    Mesh mesh;
    public Vector2[] UVs;
    public Vector2 scale;
    public Vector2 offset;
    public Vector2 result;
    public Vector2 result2;

    void Start()
    {
        filter = this.gameObject.GetComponent<MeshFilter>();
        mesh = filter.mesh;
        UVs = new Vector2[mesh.uv.Length];

    }

    float x1 = 0;
    float y1 = 0;
    float x2 = 0;
    float y2 = 0;
    void Update()
    {

        x1 = 0.125f * 0.25f;
        y1 = 0.125f * 0.25f;
        x2 = (0.125f * 0.25f) * 5;
        y2 = 1 - (((0.125f * 0.25f) * 2) + (0.125f * 0.25f));

        result.x = x1;
        result.y = y1;
        result2.x = x2;
        result2.y = y2;


        int i = 0;

        foreach (Vector2 coordinate in mesh.uv)
        {
            UVs[i] = mesh.uv[i];
            i++;
        }
        scale = GetComponent<Renderer>().material.mainTextureScale;
        offset = GetComponent<Renderer>().material.mainTextureOffset;
    }
}
