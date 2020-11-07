using UnityEngine;
using System.Collections;

public class SetWizard : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		xa.wizardCutsceneScript = this.gameObject.GetComponent<CutsceneCharacterScript>();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
