using UnityEngine;
using System.Collections;

public class TestGhost : MonoBehaviour {

#if STEAMWORKS
	IEnumerator Start() {
		yield return new WaitForSeconds(1f);
		Ghost test = new Ghost();
		test.StartRecording("level1");

		for(int i = 0; i < 30000;i++) { 
			test.Record(new Vector2(Random.Range(0,1000),Random.Range(0,1000)),(int)Random.Range(0,1024));
		}

		test.BeginSavingRecording();

		while(!test.IsDoneSaving) { 
			yield return null; 
		}
		
		test.FinishSavingRecording();

		test.ClearRecording();
		test.LoadRecording("level1");
	}
#endif
}