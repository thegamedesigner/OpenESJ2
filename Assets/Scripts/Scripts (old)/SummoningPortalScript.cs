using UnityEngine;
using System.Collections;

public class SummoningPortalScript : MonoBehaviour
{
	public GameObject[] monsters;

	float timeSave1 = 0;
	float timeSave2 = 0;
	float timeSave3 = 0;
	// Arg! Michael Todd! Use arrays!
	int result = 0;
	GoomaScript script = null;

	void Start()
	{
		timeSave1 = 9999;
		timeSave2 = 9999;
		timeSave3 = 9999;
	}

	void Update()
	{
		//1 - imps
		//2 - Lots of imps
		//3 - fast imps
		//4 - lots of spike ball things
		//5 - laser imps
		//6 - humpbacks
		//7 - lots of spike ball things
		if (xa.runesCollected >= 1 && xa.runesCollected <= 6) { stage1(); }
		if (xa.runesCollected >= 2 && xa.runesCollected <= 6) { stage2(); }
		if (xa.runesCollected >= 3) { stage3(); }

		// Setup.GC_DebugLog(xa.runesCollected);
	   // Setup.GC_DebugLog(xa.runesExisting);
	}

	void stage1()
	{
		if (timeSave1 >= 9998) { timeSave1 = fa.time; }
		if (fa.time > (4 + timeSave1))
		{
			timeSave1 = fa.time;

			result = 1;
			// if (Random.Range(0, 10) < 7) { result = 1; }

			script = null;
			xa.tempobj = (GameObject)(Instantiate(monsters[result], transform.position, xa.null_quat));
			script = xa.tempobj.GetComponent<GoomaScript>();
			if (Random.Range(0, 10) < 5) { script.flipAround(); }
		}
	}

	void stage2()
	{
		if (timeSave2 >= 9998) { timeSave2 = fa.time; }
		if (fa.time > (5 + timeSave2))
		{
			timeSave2 = fa.time;

			result = 0;

			script = null;
			xa.tempobj = (GameObject)(Instantiate(monsters[result], transform.position, xa.null_quat));
			script = xa.tempobj.GetComponent<GoomaScript>();
			if (Random.Range(0, 10) < 5) { script.flipAround(); }
		}
	}

	void stage3()
	{
		if (timeSave3 >= 9998) { timeSave3 = fa.time; }
		if (fa.time > (16 + timeSave3))
		{
			timeSave3 = fa.time;

			result = 2;
			if (Random.Range(0, 10) < 5) { result = 3; }

			script = null;
			xa.tempobj = (GameObject)(Instantiate(monsters[result], transform.position, xa.null_quat));
			script = xa.tempobj.GetComponent<GoomaScript>();
			if (Random.Range(0, 10) < 5) { script.flipAround(); }
		}
	}

}
