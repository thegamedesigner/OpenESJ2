using UnityEngine;

public class HeartSelfDestructScript : MonoBehaviour {


	void Update () {
        //Layering the hearts z position properly.
	    Vector3 pos = transform.position;
        pos.z -= fa.deltaTime/2;
        transform.position = pos;
		
		
		//When to destroy.
		Vector3 scale = transform.localScale;
		if(scale.x < 0.1f){
			Destroy(gameObject, 0.5f);
		}
	}
}
