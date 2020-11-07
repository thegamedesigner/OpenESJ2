using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlayerFuncs : MonoBehaviour
{
	public static bool multiplayerMode = false;
	public static GameObject[] players = new GameObject[4];
	public static bool[] desireToSpawn = new bool[4];

	public static void CheckForMultiPlayers()
	{
		//Check if other players want to spawn

		
		if (CheckInput(1)) { desireToSpawn[1] = true; multiplayerMode = true;}//Player2 (base zero)
		if (CheckInput(2)) { desireToSpawn[2] = true; multiplayerMode = true;}//Player3 (base zero)
		if (CheckInput(3)) { desireToSpawn[3] = true; multiplayerMode = true;}//Player4 (base zero)


		for (int i = 0; i < desireToSpawn.Length; i++)
		{
			if (desireToSpawn[i])
			{
				//Player 2 wants to spawn
				Vector3 spawnPt = FindSpawnpoint();
				if (spawnPt.x < -998 && spawnPt.y < -998 && spawnPt.z < -998)
				{
					//don't spawn
					//just wait
				}
				else
				{
					//spawn here
					desireToSpawn[i] = false;

					//spawn
					spawnPt.z = xa.GetLayer(xa.layers.PlayerAndBlocks);
					GameObject go = Instantiate<GameObject>(xa.de.multiPlayerPrefab, spawnPt, xa.null_quat);
					NovaPlayerScript nps = go.GetComponent<NovaPlayerScript>();
					nps.playerNumber = i;
					xa.totalRespawns++;
					if (i == 0) { xa.player = go; }

					/*//Don't stap to camera anything, because checkpoints don't exist in this mode.
					if (za.useSnapCameraToCheckpoint)
					{
						fa.mainCameraObject.transform.position = za.snapCameraToThisPos;
						za.useSnapCameraToCheckpoint = false;
					}*/
					//
					//xa.lastSpawnPoint.x = spawnPoint.transform.position.x;
					//xa.lastSpawnPoint.y = spawnPoint.transform.position.y;
				}
			}
		}

	}

	public static Vector3 FindSpawnpoint()
	{
		for (int i = 0; i < players.Length; i++)
		{
			if (players[i] != null)
			{
				//spawn here
				return players[i].transform.position;
			}
		}
		return new Vector3(-999, -999, -999);
	}

	public static bool CheckInput(int playerNum)
	{
		bool result = false;
		for (int i = 0; i < Controls.controls.Count; i++)
		{
			if (Controls.controls[i].type == Controls.Type.Jump && Controls.controls[i].player == playerNum)
			{
				Controls.Control c = Controls.controls[i];

				//is it a key, or an axis?
				if (c.keyInt != -1)
				{
					if (Input.GetKey((KeyCode)c.keyInt)) { result = true; }
				}
				else
				{
					//handle axis
					if (c.posAxis)
					{
						if (Controls.axes[c.joyNum, c.axisNum] > Controls.deadzone) { result = true; }
					}
					else
					{
						if (Controls.axes[c.joyNum, c.axisNum] < -Controls.deadzone) { result = true; }
					}
				}
			}
		}

		return result;
	}

}
