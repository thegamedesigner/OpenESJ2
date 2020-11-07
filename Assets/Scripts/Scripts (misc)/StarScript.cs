using UnityEngine;
using System.Collections;

public class StarScript : MonoBehaviour
{
    public int score = 0;
    public GameObject starCollectedEffect = null;
    public Behaviour enableThisOnCollection = null;
    public bool dontSelfDestroyOnPickup = false;
    public bool dead = false;
    public bool dontRegisterMe = false;
    public bool dontTriggerForStarCollector = false;
    public bool amElectricBolt = false;
    public bool amBlackStar = false;
    public bool amRainbowStar = false;
    void Awake()
    {
        //check if I'm already in the stars array
       if (!dontRegisterMe)
        {
            if (lookForMe())
            {
                //I've been registered, so I have been collected. Delete me.
                if (amElectricBolt) { za.numberOfElectricalBolts++;  }
                Destroy(this.gameObject);
            }
            else
            {
                //I haven't been registered, which means I haven't been collected
            }
        }
    }

    void Start()
    {
        //work out & save how many stars are in this level

    }

    void Update()
    {

    }

    bool lookForMe()
    {
        int index = 0;
        while (index < za.checkpointedStars.Length)
        {
            if (za.checkpointedStars[index].z == 1)//slot has been used
            {
                if (compareStars(transform.position, za.checkpointedStars[index]))
                {
                    return (true);
                }
            }
            index++;
        }
        return (false);
    }

    void addToStarRegistry()
    {
        int index = 0;
        while (index < za.collectableStars.Length)
        {
            if (za.collectableStars[index].z == 0)
            {
               // Setup.GC_DebugLog("Added to star registry!");
                za.collectableStars[index].x = transform.position.x;
                za.collectableStars[index].y = transform.position.y;
                za.collectableStars[index].z = 1;

                break;
            }
            index++;
        }
    }

    //called in Main
    public static void cleanStarsRegister()
    {
        //Setup.GC_DebugLog("Cleaned star registry.");
        int index = 0;
        while (index < za.collectableStars.Length)
        {
            za.collectableStars[index].x = 0;
            za.collectableStars[index].y = 0;
            za.collectableStars[index].z = 0;
            za.checkpointedStars[index].x = 0;
            za.checkpointedStars[index].y = 0;
            za.checkpointedStars[index].z = 0;
            index++;
        }
    }

    public static void saveCollectedStarsToCheckpointedStars()
    {
        int index = 0;
        while (index < za.collectableStars.Length)
        {
            za.checkpointedStars[index] = za.collectableStars[index];
            index++;
        }
    }

    public static void overwriteCollectedStarsWithCheckpointedStars()
    {
        int index = 0;
        while (index < za.collectableStars.Length)
        {
            za.collectableStars[index] = za.checkpointedStars[index];
            index++;
        }
    }

    public void actualCollectThisStar()
    {
        StartCoroutine(IEnumeratorCollectThisStar());
    }

    IEnumerator IEnumeratorCollectThisStar()
    {
        dead = true;
        if (enableThisOnCollection)
        {
            enableThisOnCollection.enabled = true;
        }

        if (amBlackStar)
        { 
            za.blackStarsTouched++;
            if (za.blackStarsTouched >= 100)
            {
                //GC_Achievements.getAward(GC_Achievements.Awards.MISSILEBAIT);
            }
            else
            {
                //GC_Saving.SaveFastAwardsData();

            }
        }

        if (amRainbowStar)
        {
            za.numOfRainbowStarsFound++;
            handleRainbowStarStuff();

        }
        xa.realScore += score;
        //xa.displayScore += score;

        if (!dontRegisterMe) { addToStarRegistry(); }
        if (starCollectedEffect) { Instantiate(starCollectedEffect, transform.position, starCollectedEffect.transform.rotation); }
        yield return null;//This is so it destroys one frame later, so enableThis can do its thing for one frame
        if (!dontSelfDestroyOnPickup) { Destroy(this.gameObject); }

    }
    public void collectThisStar()
    {
        if (!dontTriggerForStarCollector)
        {
            actualCollectThisStar();
        }
    }

    public void collectThisStarTriggerExternally()
    {
            actualCollectThisStar();
    }

    bool compareStars(Vector3 ingameStar, Vector3 registryStar)
    {
        //if (registryStar.z != 1) { return (false); }//then this slot is not used!
        if (ingameStar.x != registryStar.x) { return (false); }
        if (ingameStar.y != registryStar.y) { return (false); }
        //if (t1.position.z != t2.position.z) { return (false); }//dont check this. It can be changed by snapToLevel scripts


        return (true);

    }


    public void handleRainbowStarStuff()
    {
       // Setup.GC_DebugLog("Handling a rainbow star...");
        //GC_Achievements.getAward(GC_Achievements.Awards.CONSPIRACYTHEORY);

        //Have I found this rainbow star before?
        int index = 0;
        bool previouslyFound = false;

        while (index < LevelInfo.firstTimeRainbowStars.Length)
        {
            if (transform.position.x == LevelInfo.firstTimeRainbowStars[index].x && transform.position.y == LevelInfo.firstTimeRainbowStars[index].y)
            {
                //have been found before
                previouslyFound = true;
                break;
            }
            index++;
        }


        if (!previouslyFound)
        {
            index = 0;
            while (index < LevelInfo.firstTimeRainbowStars.Length)
            {
                if (LevelInfo.firstTimeRainbowStars[index].x == -999 && LevelInfo.firstTimeRainbowStars[index].y == -999)
                {
                    //free slot
                    LevelInfo.firstTimeRainbowStars[index].x = transform.position.x;
                    LevelInfo.firstTimeRainbowStars[index].y = transform.position.y;
                }
                index++;
            }
            LevelInfo.totalRainbowStars++;
        }
        else
        {

           // Setup.GC_DebugLog("Previously discovered this star. Ignoring...");

        }


    }
}
