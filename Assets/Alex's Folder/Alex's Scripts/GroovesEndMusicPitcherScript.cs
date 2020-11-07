using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GroovesEndMusicPitcherScript : MonoBehaviour {

    AudioSource a = null;
    float masterPitch = 1f;
    int goalPitch = 1;
	
	//Invincible stuff.
	public static GroovesEndMusicPitcherScript instance;
	int levelIBelong = 0;
	
	void Awake(){
		this.IHaveAForcefield();
	}
	
	
    void Start() {
        if(a == null) {
            a = za.skald.GetComponent<AudioSource>();
            return;
        }

        if (xa.player && a != null) {
              if(xa.playerPos.x > 823) {
                goalPitch = 0;
                masterPitch = 0;
                a.pitch = 0;
            } else {
                goalPitch = 1;
                masterPitch = 1;
                a.pitch = 1;
            }
        }
    }

	void Update () {
		Scene latestS = SceneManager.GetActiveScene();
		int latestSNum = latestS.buildIndex;
		if(levelIBelong != latestSNum){
			a.pitch = 1;
			Destroy(gameObject);
		} else {
			
			if(a == null) {
				a = za.skald.GetComponent<AudioSource>();
				return;
			}
			
			if (xa.player && a != null) {
            
				if(xa.playerPos.x > 823) {
					goalPitch = 0;
				} else {
					goalPitch = 1;
				}

				if(masterPitch > goalPitch) { masterPitch -= fa.deltaTime/2; }
				else if(masterPitch < goalPitch) { masterPitch += fa.deltaTime/2; }

				if(masterPitch > 1) { masterPitch = 1; }
				else if(masterPitch < 0) { masterPitch = 0; }

				a.pitch = masterPitch;
			}
		}
	}
	
	private void IHaveAForcefield(){
		if(instance == null){
			//Debug.Log("Created Instance");
			Scene s = SceneManager.GetActiveScene();
			levelIBelong = s.buildIndex;
			//Debug.Log("Level I Belong: " + levelIBelong.ToString());
			DontDestroyOnLoad(this.gameObject);
			instance = this;
		} else if(this != instance){ //But my forcefield has some weaknesses...
			Destroy(this.gameObject);
		}
	}
}