using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINavScript : MonoBehaviour
{
	[HideInInspector]
	public Button selfButton;
	[HideInInspector]
	public Slider selfSlider;
	[HideInInspector]
	public Scrollbar selfScrollbar;
	[HideInInspector]
	public InputField selfInputField;

	public UINavScript north;
	public UINavScript south;
	public UINavScript west;
	public UINavScript east;

	public UINavScript[] northSecondary;
	public UINavScript[] southSecondary;
	public UINavScript[] westSecondary;
	public UINavScript[] eastSecondary;


	public UINavScript up;//This is for selecting an input field that this button is covering
	public UINavScript down;

	bool inited = false;

	public void Init()
	{
		if (inited) { return; }
		inited = true;

		selfButton = GetComponent<Button>();
		selfSlider = GetComponent<Slider>();
		selfScrollbar = GetComponent<Scrollbar>();
		selfInputField = GetComponent<InputField>();
	}

	public void SelectSelf()
	{
		if (selfButton != null) { selfButton.Select(); }
		if (selfSlider != null) { selfSlider.Select(); }
		if (selfScrollbar != null) { selfScrollbar.Select(); }
		if (selfInputField != null) { selfInputField.Select(); }
	}

	public void SelectDown()
	{
		ProfileScript.DontUseSavedAccount();
		Debug.Log("Locked");

	}

}
