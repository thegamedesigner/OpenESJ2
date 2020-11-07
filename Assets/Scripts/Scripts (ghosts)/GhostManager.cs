using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GhostManager : MonoBehaviour {
	
	public GameObject ghostPrefab;

	private static GhostManager instance;

    bool debugBool = false;

	private static bool _ghostsEnabled = true;
	public static bool ghostsEnabled {
		get { return _ghostsEnabled; }
		set { }
	}

	#if STEAMWORKS
	public static Ghost ghost;

	private static List<GhostPuppet> ghostPuppets;
	private static List<Ghost> ghosts;
	private static string currentLeaderboardName = "";

	
	private IEnumerator SaveGhost() {
		//Debug.Log("Start Saving Recording");

        //debugging
             //   if (ghost.isLoaded && !ghost.isDone)
             //   {
             //       ghost.PrintAllFrames("Saving: ");

                
           // }



		ghost.BeginSavingRecording();
		while(!ghost.IsDoneSaving) { 
			yield return null; 
		}
		ghost.FinishSavingRecording();
		ghost.ClearRecording();
	}

	private PlayerScript playerScript;
	private Transform playerTransform;
	
	public void FixedUpdate() {
		if(ghost.isRecording) {
			if( playerScript == null && xa.player != null ) {
				playerScript = xa.player.GetComponent<PlayerScript>();
			}
			if(playerTransform == null && xa.player != null ) {
				playerTransform = xa.player.transform;
			}
			
			if(playerScript != null && playerTransform != null) {
                //Debug.Log(playerScript.GetAniFrameAsInt());
				ghost.Record( playerTransform.position.x, playerTransform.position.y, playerScript.GetAniFrameAsInt() );



                //debugging
                /*
                if (!debugBool)
                {

                    debugBool = true;
				for( int i = 0; i < ghosts.Count; i++ ) {
                    if (ghosts[i].isLoaded && !ghosts[i].isDone)
                    {
                        //ghosts[i].PrintAllFrames();

                    }
                }
                }
                */
				for( int i = 0; i < ghosts.Count; i++ ) {
					if(ghosts[i].isLoaded && !ghosts[i].isDone) {
						ghostPuppets[i].SetPosition( ghosts[i].GetPosition(xa.playerAndBlocksLayer) );
						ghostPuppets[i].SetAnimationFrame( ghosts[i].GetAnimationFrame() );
						ghosts[i].Step();


						if( ghosts[i].isDone ) {
							ghostPuppets[i].Hide();
						}
					}
				}
			}
		}
	}
	#endif // STEAMWORKS

	
	public void Awake() {
		instance = this;
		#if STEAMWORKS
		#endif // STEAMWORKS
	}

	public static void Init(bool disableGhosts) {
		#if STEAMWORKS
		ghost = new Ghost();
		ghostPuppets = new List<GhostPuppet>();
		ghosts = new List<Ghost>();

        if (disableGhosts) { DisableGhosts(); }
        #endif // STEAMWORKS
	}
    
#if STEAMWORKS
	public static GhostPuppet CreateGhostPuppet(Ghost newGhost = null) {
		GameObject ghostPuppetObject = GameObject.Instantiate(instance.ghostPrefab,Vector3.zero,Quaternion.identity) as GameObject;
		DontDestroyOnLoad( ghostPuppetObject );
		GhostPuppet ghostPuppet = ghostPuppetObject.GetComponent<GhostPuppet>();
		if(newGhost != null && newGhost.steamName != null) {
			ghostPuppet.SetNameTag( newGhost.steamName );
		}
		else {
			ghostPuppet.SetNameTag( "anonymous" );
		}
		if(!ghostsEnabled) {
			ghostPuppet.Hide();
		}
		return ghostPuppet;
	}
#endif
	public static void OnLevelLoaded(string level) {
		#if STEAMWORKS
		int levelNum = LevelInfo.getSceneNumFromName( level );
		string leaderboardName = LevelInfo.getLeaderboardName( levelNum );

		currentLeaderboardName = leaderboardName;

		//Debug.Log ("Loading ghosts for: " + leaderboardName);
		/*
		if( SteamLeaderboards.ghostsAreReady ) {
			List<SteamLeaderboards.GC_LeaderboardEntry> board = SteamLeaderboards.instance.GetLeaderboard( leaderboardName, true );

			if( board != null ) {
				foreach( SteamLeaderboards.GC_LeaderboardEntry entry in board ) {
					if(entry.ghost.isLoaded) {
						ghosts.Add( entry.ghost );
						ghostPuppets.Add( CreateGhostPuppet( entry.ghost ) );
					}
				}
			}
		}*/

		ghost.StartRecording(leaderboardName);

		#endif // STEAMWORKS
	}

	public static void OnLevelWon(int totalScore) { //This is called in SavingAndLoading if you beat your top score.
		#if STEAMWORKS
		if(!currentLeaderboardName.Equals("") && ghost.isRecording) {
			//SteamLeaderboards.PostScore(currentLeaderboardName,totalScore,ghost);
			//Debug.Log("Posting new top score to steam with ghost");
		}
		#endif // STEAMWORKS
	}

	public static void OnLevelComplete() {
		#if STEAMWORKS
		if(ghost.isRecording) {
			if(ghostPuppets.Count > 0) {
				foreach( GhostPuppet ghostPuppet in ghostPuppets ) {
					Destroy( ghostPuppet.gameObject );
				}
				ghostPuppets.Clear();
			}
		}
		foreach( Ghost currentGhost in ghosts ) {
			currentGhost.ClearRecording();
		}
		ghosts.Clear();
		currentLeaderboardName = "";
		#endif // STEAMWORKS
	}

	public static void RewindGhosts() {
		#if STEAMWORKS
		if(!currentLeaderboardName.Equals("")) {
			if(ghost.isRecording) {
				if(ghostPuppets.Count > 0) {
					foreach( GhostPuppet ghostPuppet in ghostPuppets ) {
						ghostPuppet.Show();
					}
				}
			}
			foreach( Ghost currentGhost in ghosts ) {
				currentGhost.Rewind();
			}
			ghost.StartRecording(currentLeaderboardName);
		}
		#endif
	}

	public static void EnableGhosts() {
		#if STEAMWORKS
		_ghostsEnabled = true;
		if(ghost.isRecording) {
			if(ghostPuppets.Count > 0) {
				for( int i = 0; i < ghostPuppets.Count; i++ ) {
					if(!ghosts[i].isDone) {
						ghostPuppets[i].Show();
					}
				}
			}
		}
		#endif
	}

	public static void DisableGhosts() {
		#if STEAMWORKS
		_ghostsEnabled = false;
		if(ghost.isRecording) {
			if(ghostPuppets.Count > 0) {
				foreach( GhostPuppet ghostPuppet in ghostPuppets ) {
					ghostPuppet.Hide();
				}
			}
		}
		#endif
	}

}
