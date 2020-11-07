using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimensions : MonoBehaviour
{
	public GameObject blueWallCube;
	public GameObject blueFloor;
	public GameObject blueCeiling;

	public static Dimensions self;


	public enum Dimension
	{
		Red,
		Blue,
		End
	}

	public static Dimension currentDimension = Dimension.Red;

	public static List<MeshRenderer> walls;
	public static List<MeshRenderer> ceiling;
	public static List<MeshRenderer> floor;

	void Awake()
	{
		self = this;
	}

	void Start()
	{
		GameObject[] gos;
		GameObject go;
		Vector3 pos;

	//	go = Instantiate(blueFloor, new Vector3(0, -1.5f + -500, 0), blueFloor.transform.rotation);
	//	go = Instantiate(blueCeiling, new Vector3(0, 1.5f + -500, 0), blueCeiling.transform.rotation);

		gos = GameObject.FindGameObjectsWithTag("FPS_WallCube");
		for (int i = 0; i < gos.Length; i++)
		{
			pos = gos[i].transform.position;
			pos.y -= 500;
			go = Instantiate(blueWallCube, pos, gos[i].transform.rotation);

		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			SwitchDimensions();
		}
	}

	void SetDimension(Dimension d)
	{
		if (currentDimension == d) { return; }



		switch (currentDimension)
		{
			case Dimension.Red:
				FPSMainScript.FPSPlayer.transform.AddY(-500);
				currentDimension = d;
				break;
			case Dimension.Blue:
				FPSMainScript.FPSPlayer.transform.AddY(500);

				if(FPSMainScript.FPSPlayer.transform.position.y > 10)//Bug fix. SOmetimes, the player shoots up an extra 500
				{
					FPSMainScript.FPSPlayer.transform.SetY(0);
				//Debug.Log("BUG");
				}
				currentDimension = d;
				break;

		}
	}


	void SwitchDimensions()
	{
		if (currentDimension == Dimension.Red) { SetDimension(Dimension.Blue); }
		else { SetDimension(Dimension.Red); }

	}

}
