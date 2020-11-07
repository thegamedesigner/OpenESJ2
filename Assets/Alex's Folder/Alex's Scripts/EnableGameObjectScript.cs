using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGameObjectScript : MonoBehaviour {

	public GameObject objToEnable = null;

	void Start(){
		objToEnable.SetActive(true);
	}
}
