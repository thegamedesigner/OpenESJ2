using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Restart : MonoBehaviour
{
	public enum RestartFrom
	{
		RESTART_FROM_MENU,
		RESTART_FROM_CHECKPOINT,
		RESTART_FROM_START
	};

	//called when the player dies
	public void cleanLoadLevel(RestartFrom respawning, string lvl)
	{
		string level = SceneManager.GetActiveScene().name;
		
		//clean anything that needs resetting in Xa
		xa.cleanXa();
		xa.carryingStars = 0;
		Time.timeScale = 1;
		xa.playerAirSwording = false;
		ScreenShakeCamera.screenshakeAmount = 0;
		ScreenShakeCamera.screenshakeDelay = 0;
		ScreenShakeCamera.screenshakeTimeSet = 0;
		ScreenSlash.Reset();


		//if not respawning, reset some extra the .xa variables
		if (respawning == RestartFrom.RESTART_FROM_MENU || respawning == RestartFrom.RESTART_FROM_START)
		{
			fa.teleportedOnJumpingMassacre = false;
			xa.realScore = 0;
			xa.displayScore = 0;
			xa.checkpointScore = 0;
			StarScript.cleanStarsRegister();

			xa.lastSpawnPoint = Vector3.zero;
			xa.deathCountThisLevel = 0;
			xa.checkpointedStarsThisLevel = 0;
			xa.hasCheckpointed = false;

			GhostManager.RewindGhosts();

		}

		switch (respawning)
		{
			case RestartFrom.RESTART_FROM_MENU:
				//Application.LoadLevel(lvl);
				SceneManager.LoadScene(lvl);
				break;
			case RestartFrom.RESTART_FROM_CHECKPOINT:
				if (xa.hasCheckpointed)
				{
					Setup.callFadeOutFunc(level, true, level);
				}
				else
				{
					// Respawn from the beginning of the level. Reset kills and deaths
					//	if (LevelInfo.restartMusicOnLevelRestart(level))
					//{
					//	xa.bard.audio.time = 0;
					//		if (level == "Boss_run1") xa.bard.audio.time = 106;
					//	}
					xa.deathCountThisLevel = 0;
					fa.ResetSpeedrun();//reset speed run time because you haven't checkpointed
					Setup.callFadeOutFunc(level, true, level);
				}
				break;
			case RestartFrom.RESTART_FROM_START:
				//Load the current level from the start
				//if (LevelInfo.restartMusicOnLevelRestart(level))
				//{
				//	xa.bard.audio.time = 0;
				//	if (level == "Boss_run1") xa.bard.audio.time = 106;
				//}
				fa.ResetSpeedrun();
				Setup.callFadeOutFunc(level, true, level);
				break;
		}
		// Setup.GC_DebugLog("Deaths: " + xa.deathCountThisLevel);
	}
}