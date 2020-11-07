using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
	public NodeScript connectTo;
	public bool BonusDLC = false;
	public bool dontAutoSet = false;
	public bool locked = false;
	public string levelName = "LevelNameHere";
	public FreshLevels.Type levelType = FreshLevels.Type.None;
	public GameObject lockObj;
	public int lvlNum = -1;
	public int lvlNumFPS = -1;
	public int lvlNum3D = -1;
	

	[Space(10)]
	public NodeScript north;
	public NodeScript south;
	public NodeScript west;
	public NodeScript east;
	/*
	public NodeController.Type north = NodeController.Type.None;
	public NodeController.Type south = NodeController.Type.None;
	public NodeController.Type west = NodeController.Type.None;
	public NodeController.Type east = NodeController.Type.None;*/
	public bool setCamPos = false;
	public Vector2 camPos = new Vector2(0, 0);

	[HideInInspector]
	public float cameraAngle2 = 0;

	public GameObject[] objsOn = new GameObject[0];

	void Start()
	{
		cameraAngle2 = transform.localEulerAngles.z;
	}

	void Update()
	{

	}
}
