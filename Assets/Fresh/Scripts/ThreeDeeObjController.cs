using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Holoville.HOTween;
using Holoville.HOTween.Plugins;

public class ThreeDeeObjController : MonoBehaviour
{
	public enum Type
	{
		None,
		Mod1,
		Mod2,
		Mod3,
		End
	}

	public Type type = Type.None;


	public float CurveFloat = 0;
	GameObject[] curveObjs = new GameObject[0];
	float curveTimeSet = 0;
	bool setAllCurveObjs = false;

	void Start()
	{
		switch (type)
		{
			case Type.Mod1:
				break;
			case Type.Mod2:
				{
					curveObjs = new GameObject[200];
					for (int i = 0; i < curveObjs.Length; i++)
					{
						curveObjs[i] = new GameObject("curveObj");
						curveObjs[i].transform.position = new Vector3(i, 0, 30);
					}
				}
				break;
			case Type.Mod3:
				break;
		}

	}

	void Update()
	{

		switch (type)
		{
			case Type.Mod1:
				{
					/*
					GameObject[] gos = GameObject.FindGameObjectsWithTag("threeDeePuppet");
					if (xa.player == null) { return; }
					for (int i = 0; i < gos.Length; i++)
					{

						float dist = Mathf.Abs(gos[i].transform.position.x - xa.player.transform.position.x);
						if (dist < 3) { dist = 0; }
						dist = dist * dist * dist;
						gos[i].transform.SetZ(30 - dist * 0.02f);
					}*/
				}
				break;
			case Type.Mod2:
				{
					if (!setAllCurveObjs)
					{
						if (fa.time > (curveTimeSet + 0.1f))
						{
							curveTimeSet = fa.time;

							float lowestX = 9999;
							int index = -1;
							for (int i = 0; i < curveObjs.Length; i++)
							{
								if (curveObjs[i] != null)
								{
									if (curveObjs[i].transform.localScale.z < 2)
									{
										if (curveObjs[i].transform.position.x < lowestX)
										{
											lowestX = curveObjs[i].transform.position.x;
											index = i;
										}
									}
								}
							}
							if (index != -1)
							{
								curveObjs[index].transform.AddZ(-1.5f);
								iTween.MoveBy(curveObjs[index], iTween.Hash("z", 3, "time", 1, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
								curveObjs[index].transform.SetScaleZ(2);

							}
							else
							{
								setAllCurveObjs = true;
							}

						}
					}



					for (int i = 0; i < curveObjs.Length; i++)
					{
						Vector3 v = curveObjs[i].transform.position;
						v.y += 1;
						Debug.DrawLine(curveObjs[i].transform.position, v, Color.white);
					}

					GameObject[] gos = GameObject.FindGameObjectsWithTag("threeDeePuppet");
					for (int i = 0; i < gos.Length; i++)
					{
						int roundedX = Mathf.RoundToInt(gos[i].transform.position.x);
						if (roundedX < 0) { roundedX = 0; }
						if (roundedX > 199) { roundedX = 199; }
						gos[i].transform.SetZ(curveObjs[roundedX].transform.position.z);
					}

					gos = GameObject.FindGameObjectsWithTag("moving_ThreeDeePuppet");
					for (int i = 0; i < gos.Length; i++)
					{
						//int roundedX = Mathf.RoundToInt(gos[i].transform.position.x);
						//if (roundedX < 0) { roundedX = 0; }
						//if (roundedX > 199) { roundedX = 199; }

						int upperX = Mathf.CeilToInt(gos[i].transform.position.x);
						int lowerX = Mathf.FloorToInt(gos[i].transform.position.x);
						float resultZ = 0;
						float lowerZ;
						float upperZ;

						if (upperX < 0) { upperX = 0; }
						if (upperX > 199) { upperX = 199; }
						if (lowerX < 0) { lowerX = 0; }
						if (lowerX > 199) { lowerX = 199; }

						lowerZ = curveObjs[lowerX].transform.position.z;
						upperZ = curveObjs[upperX].transform.position.z;
						resultZ = (lowerZ + upperZ) * 0.5f;

						if (gos[i].transform.position.z < resultZ)
						{
							gos[i].transform.AddZ(4 * fa.deltaTime);
							if (gos[i].transform.position.z > resultZ) { gos[i].transform.SetZ(resultZ); }
						}
						if (gos[i].transform.position.z > resultZ)
						{
							gos[i].transform.AddZ(-4 * fa.deltaTime);
							if (gos[i].transform.position.z < resultZ) { gos[i].transform.SetZ(resultZ); }
						}
					}

					//Debug.Log("Float: " + CurveFloat);
					/*
					GameObject[] gos = GameObject.FindGameObjectsWithTag("threeDeePuppet");
					for (int i = 0; i < gos.Length; i++)
					{
						float offset = Mathf.Repeat(gos[i].transform.position.x,10);
						offset -= 5;
						gos[i].transform.SetZ(30 + offset);
					}*/
				}
				break;
			case Type.Mod3:
				break;
		}
	}
}
