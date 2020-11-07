using UnityEngine;
//using UnityEditor;
using System.Collections;

public class Ani_Triggered : MonoBehaviour
{
	// [Compact]
	// public Vector3 target = new Vector3(100, 200, 300);

	// public Vector3 forward = Vector3.forward;
	public bool use32x32Sheet = false;
	public bool use16x16Sheet = true;
	public bool use8x8Sheet = false;
	public bool use4x4Sheet = false;

	public GameObject forceAnimateThisGO = null;

	public bool[] playAniOnStart = new bool[0];
	public bool[] aniLoops = new bool[0];
	public bool[] aniIsUninterruptible = new bool[0];
	public float[] forceAniSpeed = new float[0];
	public int[] switchToAniXOnEnd = new int[0];//ignores zero
	public Behaviour[] triggerBehaviorOnStart = new Behaviour[0];
	public Behaviour[] triggerBehaviorOnEnd = new Behaviour[0];
	public Behaviour[] triggerBehaviorOnFrame = new Behaviour[0];
	public int[] triggerBehaviorOnFrameNum = new int[0];
	public string[] optionalAniName = new string[0];

	public Vector3[] ani0 = new Vector3[0];//maximum number of animations
	public Vector3[] ani1 = new Vector3[0];
	public Vector3[] ani2 = new Vector3[0];
	public Vector3[] ani3 = new Vector3[0];
	public Vector3[] ani4 = new Vector3[0];
	public Vector3[] ani5 = new Vector3[0];
	public Vector3[] ani6 = new Vector3[0];
	public Vector3[] ani7 = new Vector3[0];
	public Vector3[] ani8 = new Vector3[0];
	public Vector3[] ani9 = new Vector3[0];
	public Vector3[] ani10 = new Vector3[0];
	public Vector3[] ani11 = new Vector3[0];
	public Vector3[] ani12 = new Vector3[0];
	public Vector3[] ani13 = new Vector3[0];
	public Vector3[] ani14 = new Vector3[0];
	public Vector3[] ani15 = new Vector3[0];

	int index = 0;
	Vector3[] currentAni = new Vector3[30];//maximum length to an animation
	int currentAniLength = 0;
	[HideInInspector]
	public int currentlyPlayingAni = -1;
	bool currentAniLoops = false;
	bool currentAniIsUninterruptible = false;
	float currentAniForcedSpeed = 0;
	int currentSwitchToAniXOnEnd = 0;
	Behaviour currenttriggerBehaviorOnStart = null;
	Behaviour currenttriggerBehaviorOnEnd = null;
	Behaviour currenttriggerBehaviorOnFrame = null;
	int currenttriggerBehaviorOnFrameNum = -1;
	int aniProgress = 0;
	float timeSet = 0;
	float timeDelay = 0;
	float multi = 0.5f;
	GameObject rendererGO = null;

	void Start()
	{
		if (use32x32Sheet) { multi = 0.25f; }
		if (use16x16Sheet) { multi = 0.5f; }
		if (use8x8Sheet) { multi = 1; }
		if (use4x4Sheet) { multi = 2f; }

		rendererGO = this.gameObject;
		if (forceAnimateThisGO) { rendererGO = forceAnimateThisGO; }

		index = 0;
		while (index < playAniOnStart.Length)
		{
			if (playAniOnStart[index]) { playAnimation(index); }
			index++;
		}
	}

	void Update()
	{
		if (currentlyPlayingAni != -1)
		{
			//count delay
			if (fa.time >= (timeSet + timeDelay))
			{
				timeSet = fa.time;
				aniProgress++;

				//trigger behavior on frame
				if (aniProgress == currenttriggerBehaviorOnFrameNum && aniProgress != -1)
				{
					currenttriggerBehaviorOnFrame.enabled = true;
				}

				if (aniProgress >= currentAniLength)
				{
					if (currentAniLoops)
					{
						playAnimation(currentlyPlayingAni);
					}
					else
					{
						if (currenttriggerBehaviorOnEnd != null) { currenttriggerBehaviorOnEnd.enabled = true; }
						currentlyPlayingAni = -1;
						if (currentSwitchToAniXOnEnd != 0)
						{
							playAnimation(currentSwitchToAniXOnEnd);
						}
					}
				}

				if (currentlyPlayingAni != -1)
				{
					setTexture((int)(currentAni[aniProgress].x), (int)(currentAni[aniProgress].y));
					if (currentAniForcedSpeed != 0)
					{
						timeDelay = currentAniForcedSpeed;
					}
					else
					{
						timeDelay = currentAni[aniProgress].z;
					}
				}
			}
		}
	}

	void setCurrentAnimationVariables()
	{
		if (currentlyPlayingAni < aniLoops.Length) { currentAniLoops = aniLoops[currentlyPlayingAni]; } else { currentAniLoops = false; }
		if (currentlyPlayingAni < aniIsUninterruptible.Length) { currentAniIsUninterruptible = aniIsUninterruptible[currentlyPlayingAni]; } else { currentAniIsUninterruptible = false; }
		if (currentlyPlayingAni < forceAniSpeed.Length) { currentAniForcedSpeed = forceAniSpeed[currentlyPlayingAni]; } else { currentAniForcedSpeed = 0; }
		if (currentlyPlayingAni < switchToAniXOnEnd.Length) { currentSwitchToAniXOnEnd = switchToAniXOnEnd[currentlyPlayingAni]; } else { currentSwitchToAniXOnEnd = 0; }
		if (currentlyPlayingAni < triggerBehaviorOnStart.Length) { currenttriggerBehaviorOnStart = triggerBehaviorOnStart[currentlyPlayingAni]; } else { currenttriggerBehaviorOnStart = null; }
		if (currentlyPlayingAni < triggerBehaviorOnEnd.Length) { currenttriggerBehaviorOnEnd = triggerBehaviorOnEnd[currentlyPlayingAni]; } else { currenttriggerBehaviorOnEnd = null; }
		if (currentlyPlayingAni < triggerBehaviorOnFrame.Length) { currenttriggerBehaviorOnFrame = triggerBehaviorOnFrame[currentlyPlayingAni]; currenttriggerBehaviorOnFrameNum = triggerBehaviorOnFrameNum[currentlyPlayingAni]; } else { currenttriggerBehaviorOnFrame = null; currenttriggerBehaviorOnFrameNum = -1; }

		if (currentlyPlayingAni == 0) { currentAni = ani0; currentAniLength = ani0.Length; }
		if (currentlyPlayingAni == 1) { currentAni = ani1; currentAniLength = ani1.Length; }
		if (currentlyPlayingAni == 2) { currentAni = ani2; currentAniLength = ani2.Length; }
		if (currentlyPlayingAni == 3) { currentAni = ani3; currentAniLength = ani3.Length; }
		if (currentlyPlayingAni == 4) { currentAni = ani4; currentAniLength = ani4.Length; }
		if (currentlyPlayingAni == 5) { currentAni = ani5; currentAniLength = ani5.Length; }
		if (currentlyPlayingAni == 6) { currentAni = ani6; currentAniLength = ani6.Length; }
		if (currentlyPlayingAni == 7) { currentAni = ani7; currentAniLength = ani7.Length; }
		if (currentlyPlayingAni == 8) { currentAni = ani8; currentAniLength = ani8.Length; }
		if (currentlyPlayingAni == 9) { currentAni = ani9; currentAniLength = ani9.Length; }
		if (currentlyPlayingAni == 10) { currentAni = ani10; currentAniLength = ani10.Length; }
		if (currentlyPlayingAni == 11) { currentAni = ani11; currentAniLength = ani11.Length; }
		if (currentlyPlayingAni == 12) { currentAni = ani12; currentAniLength = ani12.Length; }
		if (currentlyPlayingAni == 13) { currentAni = ani13; currentAniLength = ani13.Length; }
		if (currentlyPlayingAni == 14) { currentAni = ani14; currentAniLength = ani14.Length; }
		if (currentlyPlayingAni == 15) { currentAni = ani15; currentAniLength = ani15.Length; }
	}

	void restartAnimation()
	{
		setCurrentAnimationVariables();

		if (currenttriggerBehaviorOnStart != null) { currenttriggerBehaviorOnStart.enabled = true; }
		aniProgress = 0;
		timeSet = fa.time;
		if (currentAniForcedSpeed != 0) { timeDelay = currentAniForcedSpeed; }
		else { timeDelay = currentAni[0].z; }

		//set first frame
		setTexture((int)(currentAni[0].x), (int)(currentAni[0].y));

	}

	void playAnimation(int input)
	{
		if (currentAniIsUninterruptible && input != currentlyPlayingAni)
		{
			//if a different animation is trying to butt in, and the current one is UNINTERRUPTIBLE, then do nothing.
		}
		else
		{
			currentlyPlayingAni = input;
			restartAnimation();
		}
	}

	public void playAni0() { playAnimation(0); }
	public void playAni1() { playAnimation(1); }
	public void playAni2() { playAnimation(2); }
	public void playAni3() { playAnimation(3); }
	public void playAni4() { playAnimation(4); }
	public void playAni5() { playAnimation(5); }
	public void playAni6() { playAnimation(6); }
	public void playAni7() { playAnimation(7); }
	public void playAni8() { playAnimation(8); }
	public void playAni9() { playAnimation(9); }
	public void playAni10() { playAnimation(10); }
	public void playAni11() { playAnimation(11); }
	public void playAni12() { playAnimation(12); }
	public void playAni13() { playAnimation(13); }
	public void playAni14() { playAnimation(14); }
	public void playAni15() { playAnimation(15); }

	public void playAniByName(string name) 
	{
		int index = 0;
		while (index < optionalAniName.Length)
		{
			if (optionalAniName[index] != null)
			{
				if (optionalAniName[index] == name)
				{
					playAnimation(index);
				}
			}
			index++;
		}
		playAnimation(15); 
	}

	void setTexture(int v1, int v2)
	{
        //Setup.setTexture(v1, v2, multi, rendererGO, false);//breaks cleanerexploplatforms and chargers
        
		float x1 = 0;
		float y1 = 0;
		float x2 = 0;
		float y2 = 0;

		x1 = 0.125f * multi;
		y1 = 0.125f * multi;
		x2 = (0.125f * multi) * v1;
		y2 = 1 - (((0.125f * multi) * v2) + (0.125f * multi));

		if (rendererGO)
		{
			rendererGO.GetComponent<Renderer>().material.mainTextureScale = new Vector2(x1, y1);
			rendererGO.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x2, y2);
		}
	}

}
