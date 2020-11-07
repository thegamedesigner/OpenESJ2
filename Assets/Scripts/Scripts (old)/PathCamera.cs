using System.Collections;
using UnityEngine;

public class PathCamera : MonoBehaviour
{
	public enum CameraBehaviour
	{
		STATIC			= 0,
		TRACK_PLAYER	= 1,
		TRIGGER			= 2,
		TWEEN			= 3,
		//CAN_BUMP		= 1 << 3,
	}
	
	CameraBehaviour behaviour	= CameraBehaviour.STATIC;
	//iTween.EaseType	easing		= iTween.EaseType.linear;
	public GameObject nextNode	= null;
	public float timeToNextNode	= 1.0f;
	
	void Start()
	{
		// snap to player location.
		switch (behaviour)
		{
			case CameraBehaviour.TWEEN:
				// Constantly tween the camera (probably handled in the itween itself)
				// Add tween here
				Hashtable ht = new Hashtable();
				ht.Add("time", timeToNextNode);
				ht.Add("amount", nextNode.transform.position);
				ht.Add("easetype", iTween.EaseType.linear);
				ht.Add("oncomplete", "OnComplete");
				iTween.MoveAdd(this.gameObject, ht);
			break;
			default:
			case CameraBehaviour.STATIC:
				// Don't move the camera.
			break;
			case CameraBehaviour.TRACK_PLAYER:
				// Follow Player's x/y position
			break;
			case CameraBehaviour.TRIGGER:
				// Move the camera to the next node when the player is within a trigger area
			break;
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		switch (behaviour)
		{
			case CameraBehaviour.TRACK_PLAYER:
				// Follow Player's x/y position
			break;
			case CameraBehaviour.TRIGGER:
				// Move the camera to the next node when the player is within a trigger area
			break;
			case CameraBehaviour.TWEEN:
				// Constantly tween the camera (probably handled in the itween itself)
			break;
			case CameraBehaviour.STATIC:
				// Don't move the camera.
			break;
		}
	}

	void OnComplete()
	{
		// Camera has finished moving to the next node.
		
	}
}
