using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InvincibleVineScript : MonoBehaviour {

	public static InvincibleVineScript instance;
	int levelIBelong = 0;
	
	void Awake(){
		this.IHaveAForcefield();
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
	
	//And even more weaknesses...
	void Update(){
		Scene latestS = SceneManager.GetActiveScene();
		int latestSNum = latestS.buildIndex;
		if(levelIBelong != latestSNum){
			Destroy(gameObject);
		}
	}
}
