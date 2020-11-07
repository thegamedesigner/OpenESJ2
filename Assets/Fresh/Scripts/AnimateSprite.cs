using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateSprite : MonoBehaviour
{
	public float speed = 0;
	public Image image;
	public Sprite[] frames = new Sprite[0];

	float timeSet = 0;
	int currentFrame = 0;

	void Start()
	{
	}

	void Update()
	{
		if (fa.time >= (timeSet + speed))
		{
			//then advance the frame
			currentFrame++;
			timeSet = fa.time;

			if (currentFrame >= frames.Length)
			{
				currentFrame = 0;
			}
			image.sprite = frames[currentFrame];
		}
	}
}
