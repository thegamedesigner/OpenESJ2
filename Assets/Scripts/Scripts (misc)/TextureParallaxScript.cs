using UnityEngine;
using System.Collections;

public class TextureParallaxScript : MonoBehaviour
{
    public float multiplier = 0.005f;
    public float offset = 0;
    float xOffset = 0;
    public float scrolling = 0;
    float scrollAmount = 0;

    void Start()
    {

    }

    void Update()
    {
        scrollAmount += scrolling * fa.deltaTime;
        xOffset = ((Camera.main.GetComponent<Camera>().gameObject.transform.position.x) * multiplier) + offset + scrollAmount;



        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(xOffset, 0));
    }


}
