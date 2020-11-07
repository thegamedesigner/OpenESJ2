using UnityEngine;
using System.Collections;

public class TriggerItweenOnHealth : MonoBehaviour
{

	public GameObject[] go;
	public string[] itweenName;
	HealthScript script;
	public float healthAmount = 0;

	void Start()
	{
		script = this.gameObject.GetComponent<HealthScript>();
	}

	void Update()
	{
		if (this.enabled)
		{
			if (script)
			{
				if (script.health <= healthAmount)
				{
					int index = 0;
					while (index < go.Length)
					{
						iTweenEvent.GetEvent(go[index], itweenName[index]).Play();
						index++;
					}
					this.enabled = false;
				}
			}
		}
	}

}
