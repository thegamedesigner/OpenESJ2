using UnityEngine;
using System.Collections;

public class ScrollTexture : MonoBehaviour
{
		float speed = 9.1f;
	float y = 0;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	public float scrollSpeed = 0.5F;
	void Update()
	{

	  //  float offset = Time.time * scrollSpeed;
	  //  renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));

		this.gameObject.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, y);
		y += speed * fa.deltaTime;
	 //   if (y > 1) { y -= 1; }
		
	}
}
