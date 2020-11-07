using UnityEngine;
using System.Collections;

public class AlphaFadeBatchedObject : MonoBehaviour
{
    public GameObject fadeThisGO = null;
    public float speed = 5;
    Color color;
    void Start()
    {
    }
    void Update()
    {
        MaterialPropertyBlock materialProperty = new MaterialPropertyBlock();
        color = fadeThisGO.GetComponent<Renderer>().material.color;
        color.a -= speed * fa.deltaTime;
        materialProperty.SetColor("_Color", color);
        fadeThisGO.GetComponent<Renderer>().SetPropertyBlock(materialProperty);
    }

}
