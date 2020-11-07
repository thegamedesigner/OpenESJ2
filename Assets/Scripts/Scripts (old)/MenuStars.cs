using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuStars : MonoBehaviour 
{
    /*
	public static Dictionary<string, int> StarsPerLevelDictionary;
	public GameObject activeStar   = null;
	public GameObject inactiveStar = null;
	public TextMesh starsText      = null;
	GameObject[] stars             = new GameObject[8];
	int lastSelectedLevel          = -1;
	static bool initialized               = false;



	public static void SetStarsPerLevelDictionary() //Called in Main
	{
		if (initialized) return;
		StarsPerLevelDictionary = new Dictionary<string, int>();
		StarsPerLevelDictionary.Add("ThatBlacknWhiteOne", 1);
		StarsPerLevelDictionary.Add("AnotherGreatLevel", 1);
		StarsPerLevelDictionary.Add("alexlevel01", 1);
		StarsPerLevelDictionary.Add("ASlowPinkLevel", 1);
		StarsPerLevelDictionary.Add("ATallThinLevel", 1);
		StarsPerLevelDictionary.Add("flippingPlayerBugLevel", 1);
		StarsPerLevelDictionary.Add("OrangeDoubleJumpyLevelPart1", 1);
		StarsPerLevelDictionary.Add("OrangeDoubleJumpyLevelPart2", 1);
		StarsPerLevelDictionary.Add("aTightPinkLevel", 1);
		StarsPerLevelDictionary.Add("ThatSlightlyDifficultOrangeOne", 2);
		StarsPerLevelDictionary.Add("ThatSlightlyDifficultOrangeOnePt2", 1);
		StarsPerLevelDictionary.Add("CassiesRedLevel1of2", 1);
		StarsPerLevelDictionary.Add("ThatOneWithPulsingSquares", 1);
		StarsPerLevelDictionary.Add("GrinningThroughThePain", 2);
		StarsPerLevelDictionary.Add("ARunningLevel", 1);
		StarsPerLevelDictionary.Add("AIceLevel", 1);
		StarsPerLevelDictionary.Add("ARandom4_One", 1);
		StarsPerLevelDictionary.Add("PAXASetOfFourStarsB", 3);
		initialized = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!initialized) SetStarsPerLevelDictionary();

		int highlightedLevel = xa.levelSelectCurrentNumber;
		if (highlightedLevel == lastSelectedLevel) return;
		lastSelectedLevel = highlightedLevel;
		string currentScreen = Application.loadedLevelName;
		int worldOffset = 0;
		
		if (currentScreen == "levelSelect1C")
		{
			if (highlightedLevel >= 15) highlightedLevel = 999;
		}
		else if (currentScreen == "levelSelect2C")
		{
			worldOffset = 15;
			if (highlightedLevel >= 15) highlightedLevel = 999;
		}
		else if (currentScreen == "levelSelect3C")
		{
			worldOffset = 30;
			if (highlightedLevel >= 15) highlightedLevel = 999;
		}
		else if (currentScreen == "levelSelect4")
		{
			worldOffset = 45;
			if (highlightedLevel >= 5) highlightedLevel = 999;
		}
		else if (currentScreen == "levelSelect5")
		{
			worldOffset = 50;
			if (highlightedLevel >= 3) highlightedLevel = 999;
		}

		if (highlightedLevel + worldOffset > xa.playerStars.Length)
		{
			starsText.text = "";
			foreach (GameObject go in stars)
			{
				Destroy(go);
			}
			return;
		}

		foreach (GameObject go in stars)
		{
			Destroy(go);
		}

		string currentLevelOverlapped    = LevelInfo.getSceneName(highlightedLevel + worldOffset);
		//Setup.GC_DebugLog(highlightedLevel+worldOffset);
		int starsBitmaskForThisHereLevel = xa.playerStars[highlightedLevel + worldOffset];
		int numberOfStarsInThisHereLevel = 0;
		starsText.text                   = "";
		if (StarsPerLevelDictionary.TryGetValue(currentLevelOverlapped, out numberOfStarsInThisHereLevel))
		{
			//Setup.GC_DebugLog(currentLevelOverlapped + " has " + numberOfStarsInThisHereLevel + " stars");

			for (int i = 0; i < numberOfStarsInThisHereLevel; ++i)
			{
				GameObject newStar;
	
				if ((starsBitmaskForThisHereLevel & (0x1 << i)) != 0)
					newStar = (GameObject)Instantiate(activeStar);
				else
					newStar = (GameObject)Instantiate(inactiveStar);
				newStar.transform.parent = this.transform;
				newStar.transform.localPosition = new Vector3(i * 1.5f - (numberOfStarsInThisHereLevel * 0.5f), -2.0f, 0);
				stars[i] = newStar;
			}
	
			if(numberOfStarsInThisHereLevel > 0)
			{
				starsText.text = "Stars";
			}
		}
	}*/
}
