using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Diagnostics;
using System.Threading;

public class AnimationScript_Generic : MonoBehaviour
{
	public int forceAddX = 0;
	public int forceAddY = 0;
    public bool debugMe = false;
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
    public bool[] triggerBehaviorOnFrame = new bool[0];
    public Behaviour[] triggerBehaviorOnFrameScript = new Behaviour[0];
    public int[] triggerBehaviorOnFrameNum = new int[0];
    public Behaviour[] triggerBehaviorOnEveryFrame = new Behaviour[0];
	public string[] optionalAniName = new string[0];

    //public string ani0Label = "";
    public Vector3[] ani0 = new Vector3[0];//maximum number of animations
    //public string ani1Label = "";
    public Vector3[] ani1 = new Vector3[0];
    //public string ani2Label = "";
    public Vector3[] ani2 = new Vector3[0];
    //public string ani3Label = "";
    public Vector3[] ani3 = new Vector3[0];
    //public string ani4Label = "";
    public Vector3[] ani4 = new Vector3[0];
    //public string ani5Label = "";
    public Vector3[] ani5 = new Vector3[0];
    //public string ani6Label = "";
    public Vector3[] ani6 = new Vector3[0];
    //public string ani7Label = "";
    public Vector3[] ani7 = new Vector3[0];
    //public string ani8Label = "";
    public Vector3[] ani8 = new Vector3[0];
    //public string ani9Label = "";
    public Vector3[] ani9 = new Vector3[0];
    //public string ani10Label = "";
    public Vector3[] ani10 = new Vector3[0];
    //public string ani11Label = "";
    public Vector3[] ani11 = new Vector3[0];
    //public string ani12Label = "";
    public Vector3[] ani12 = new Vector3[0];
    //public string ani13Label = "";
    public Vector3[] ani13 = new Vector3[0];
    //public string ani14Label = "";
    public Vector3[] ani14 = new Vector3[0];
    //public string ani15Label = "";
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
    bool currenttriggerBehaviorOnFrameBool = false;
    Behaviour currenttriggerBehaviorOnEveryFrame = null;
	int aniProgress = 0;
	float timeSet = 0;
	float timeDelay = 0;
	float multi = 0.5f;
	GameObject rendererGO = null;

	void Start()
	{
		ForceAddXYToAnis();
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

        //flip this shit
        xa.glx = rendererGO.transform.localScale;
        xa.glx.y = -xa.glx.y;
        rendererGO.transform.localScale = xa.glx;


	}

	void ForceAddXYToAnis()
	{
		for(int i = 0;i < ani0.Length;i++){ani0[i].x += forceAddX;ani0[i].y += forceAddY;}
		for(int i = 0;i < ani1.Length;i++){ani1[i].x += forceAddX;ani1[i].y += forceAddY;}
		for(int i = 0;i < ani2.Length;i++){ani2[i].x += forceAddX;ani2[i].y += forceAddY;}
		for(int i = 0;i < ani3.Length;i++){ani3[i].x += forceAddX;ani3[i].y += forceAddY;}
		for(int i = 0;i < ani4.Length;i++){ani4[i].x += forceAddX;ani4[i].y += forceAddY;}
		for(int i = 0;i < ani5.Length;i++){ani5[i].x += forceAddX;ani5[i].y += forceAddY;}
		for(int i = 0;i < ani6.Length;i++){ani6[i].x += forceAddX;ani6[i].y += forceAddY;}
		for(int i = 0;i < ani7.Length;i++){ani7[i].x += forceAddX;ani7[i].y += forceAddY;}
		for(int i = 0;i < ani8.Length;i++){ani8[i].x += forceAddX;ani8[i].y += forceAddY;}
		for(int i = 0;i < ani9.Length;i++){ani9[i].x += forceAddX;ani9[i].y += forceAddY;}
		for(int i = 0;i < ani10.Length;i++){ani10[i].x += forceAddX;ani10[i].y += forceAddY;}
		for(int i = 0;i < ani11.Length;i++){ani11[i].x += forceAddX;ani11[i].y += forceAddY;}
		for(int i = 0;i < ani12.Length;i++){ani12[i].x += forceAddX;ani12[i].y += forceAddY;}
		for(int i = 0;i < ani13.Length;i++){ani13[i].x += forceAddX;ani13[i].y += forceAddY;}
		for(int i = 0;i < ani14.Length;i++){ani14[i].x += forceAddX;ani14[i].y += forceAddY;}
		for(int i = 0;i < ani15.Length;i++){ani15[i].x += forceAddX;ani15[i].y += forceAddY;}
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
                if (currenttriggerBehaviorOnFrameBool)
                {
                    if (aniProgress == currenttriggerBehaviorOnFrameNum && aniProgress != -1)
                    {
                        currenttriggerBehaviorOnFrame.enabled = true;
                    }
                }

                //trigger behavior on every frame
                if (currenttriggerBehaviorOnEveryFrame)
                {
                    if (aniProgress != -1)
                    {
                        currenttriggerBehaviorOnEveryFrame.enabled = true;
                    }
                }

				if (aniProgress >= currentAniLength)
				{
					if (currentAniLoops)
					{
						playAnimation(currentlyPlayingAni);
					}
					else
                    {
                        currentAniIsUninterruptible = false;
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
        //if (debugMe) { Setup.GC_DebugLog("In SetCurrentAnimationVariables: Currently playing animation is: " + currentlyPlayingAni); }
		if (currentlyPlayingAni < aniLoops.Length) { currentAniLoops = aniLoops[currentlyPlayingAni]; } else { currentAniLoops = false; }
		if (currentlyPlayingAni < aniIsUninterruptible.Length) { currentAniIsUninterruptible = aniIsUninterruptible[currentlyPlayingAni]; } else { currentAniIsUninterruptible = false; }
		if (currentlyPlayingAni < forceAniSpeed.Length) { currentAniForcedSpeed = forceAniSpeed[currentlyPlayingAni]; } else { currentAniForcedSpeed = 0; }
		if (currentlyPlayingAni < switchToAniXOnEnd.Length) { currentSwitchToAniXOnEnd = switchToAniXOnEnd[currentlyPlayingAni]; } else { currentSwitchToAniXOnEnd = 0; }
		if (currentlyPlayingAni < triggerBehaviorOnStart.Length) { currenttriggerBehaviorOnStart = triggerBehaviorOnStart[currentlyPlayingAni]; } else { currenttriggerBehaviorOnStart = null; }
		if (currentlyPlayingAni < triggerBehaviorOnEnd.Length) { currenttriggerBehaviorOnEnd = triggerBehaviorOnEnd[currentlyPlayingAni]; } else { currenttriggerBehaviorOnEnd = null; }
        if (currentlyPlayingAni < triggerBehaviorOnFrame.Length) { currenttriggerBehaviorOnFrameBool = triggerBehaviorOnFrame[currentlyPlayingAni]; currenttriggerBehaviorOnFrame = triggerBehaviorOnFrameScript[currentlyPlayingAni]; currenttriggerBehaviorOnFrameNum = triggerBehaviorOnFrameNum[currentlyPlayingAni]; } else { currenttriggerBehaviorOnFrame = null; currenttriggerBehaviorOnFrameNum = -1; currenttriggerBehaviorOnFrameBool = false; }
        if (currentlyPlayingAni < triggerBehaviorOnEveryFrame.Length) { currenttriggerBehaviorOnEveryFrame = triggerBehaviorOnEveryFrame[currentlyPlayingAni];} else { currenttriggerBehaviorOnEveryFrame = null;  }

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
		else
		{
			timeDelay = currentAni[0].z;
		}

		//set first frame
		setTexture((int)(currentAni[0].x), (int)(currentAni[0].y));

	}

    public Vector2 GetAnimationFrame()
    {
        return new Vector2((int)(currentAni[aniProgress].x), (int)(currentAni[aniProgress].y));
    }

    public void playAnimation(int input)
    {
        //if (debugMe) { Setup.GC_DebugLog("Someone called playAni" + input); }
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

    public void playAnimationIfNotCurrentlyPlaying(int input)
    {
        if (currentlyPlayingAni != input) { playAnimation(input); }
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

    public void playAni0IfNotAlreadyPlaying() { playAnimationIfNotCurrentlyPlaying(0); }
    public void playAni1IfNotAlreadyPlaying() { playAnimationIfNotCurrentlyPlaying(1); }
    public void playAni2IfNotAlreadyPlaying() { playAnimationIfNotCurrentlyPlaying(2); }
    public void playAni3IfNotAlreadyPlaying() { playAnimationIfNotCurrentlyPlaying(3); }
    public void playAni4IfNotAlreadyPlaying() { playAnimationIfNotCurrentlyPlaying(4); }
    public void playAni5IfNotAlreadyPlaying() { playAnimationIfNotCurrentlyPlaying(5); }
    public void playAni6IfNotAlreadyPlaying() { playAnimationIfNotCurrentlyPlaying(6); }
    public void playAni7IfNotAlreadyPlaying() { playAnimationIfNotCurrentlyPlaying(7); }
    public void playAni8IfNotAlreadyPlaying() { playAnimationIfNotCurrentlyPlaying(8); }
    public void playAni9IfNotAlreadyPlaying() { playAnimationIfNotCurrentlyPlaying(9); }
    public void playAni10IfNotAlreadyPlaying() { playAnimationIfNotCurrentlyPlaying(10); }
    public void playAni11IfNotAlreadyPlaying() { playAnimationIfNotCurrentlyPlaying(11); }
    public void playAni12IfNotAlreadyPlaying() { playAnimationIfNotCurrentlyPlaying(12); }
    public void playAni13IfNotAlreadyPlaying() { playAnimationIfNotCurrentlyPlaying(13); }
    public void playAni14IfNotAlreadyPlaying() { playAnimationIfNotCurrentlyPlaying(14); }
    public void playAni15IfNotAlreadyPlaying() { playAnimationIfNotCurrentlyPlaying(15); }

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

        //Stopwatch stopwatch = new Stopwatch();
        //stopwatch.Start();

        if (rendererGO)
        {
            MeshFilter filter = rendererGO.GetComponent<MeshFilter>();
            if (filter != null)
            {
                Mesh mesh = filter.mesh;
                Vector2[] newUVs = new Vector2[mesh.uv.Length];
                int i = 0;

                newUVs[0].x = (0.125f * multi) * v1;//0.15625f;
                newUVs[0].y = 1 - ((0.125f * multi) * v2);

                newUVs[1].x = ((0.125f * multi) * v1) + (0.125f * multi);
                newUVs[1].y = 1 - ((0.125f * multi) * v2 + (0.125f * multi));

                newUVs[2].x = ((0.125f * multi) * v1) + (0.125f * multi);
                newUVs[2].y = 1 - ((0.125f * multi) * v2);

                newUVs[3].x = ((0.125f * multi) * v1);
                newUVs[3].y = 1 - ((0.125f * multi) * v2 + (0.125f * multi));//0.9375f;
                //foreach (Vector2 coordinate in mesh.uv)
                //{
               //     newUVs[i] = new Vector2(coordinate.x * 0.03125f + 0.15625f, coordinate.y * 0.03125f + 0.90625f);
                //    i++;
               // }

                mesh.uv = newUVs;
            }
        }

        //stopwatch.Stop();
        //UnityEngine.Setup.GC_DebugLog("" + stopwatch.Elapsed);
        /*
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
			rendererGO.renderer.material.mainTextureScale = new Vector2(x1, y1);
			rendererGO.renderer.material.mainTextureOffset = new Vector2(x2, y2);
        }*/
	}


    public static void SetTextureGhosts(int v1, int v2, GameObject ghost) // This is for ghosts.
    {
        if (ghost != null)
        {
            MeshFilter filter = ghost.GetComponent<MeshFilter>();
            if (filter != null)
            {
                Mesh mesh = filter.mesh;
                Vector2[] newUVs = new Vector2[mesh.uv.Length];
                int i = 0;
                float multi = 0.25f;

                newUVs[0].x = (0.125f * multi) * v1;
                newUVs[0].y = 1 - ((0.125f * multi) * v2);

                newUVs[1].x = ((0.125f * multi) * v1) + (0.125f * multi);
                newUVs[1].y = 1 - ((0.125f * multi) * v2 + (0.125f * multi));

                newUVs[2].x = ((0.125f * multi) * v1) + (0.125f * multi);
                newUVs[2].y = 1 - ((0.125f * multi) * v2);

                newUVs[3].x = ((0.125f * multi) * v1);
                newUVs[3].y = 1 - ((0.125f * multi) * v2 + (0.125f * multi));

                mesh.uv = newUVs;
            }
        }
    }
}
