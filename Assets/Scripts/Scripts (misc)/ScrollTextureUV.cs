using UnityEngine;
using System.Collections;

public class ScrollTextureUV : MonoBehaviour
{
    public float scrollSpeed = -0.5F;
    public float scrollSpeedX = 0;
    void Update()
    {

        float offset = Time.time * scrollSpeed;
        float offset2 = Time.time * scrollSpeedX;
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(offset2, offset));


    }
}
