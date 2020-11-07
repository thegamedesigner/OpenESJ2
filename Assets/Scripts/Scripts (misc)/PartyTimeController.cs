using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PartyTimeController : MonoBehaviour
{
    int nextIndex = 0;
    int nextPartyTimeIndex = 0;
    bool past2Seconds = false;
    bool checkStart = false;

    public GameObject[] partyTimeGOs;
    public GameObject[] partyTimeGOsToSetVisible;
    public ParticleSystem[] partyTimeParticles;
    public GameObject[] nonPartyTimeGOsToSetVisible;

    public static PartyTimeController selfScript;
    [HideInInspector]
    public List<PartyTimeSlave> scripts;
    bool inPartyTime = false;

    void Awake()
    {
        selfScript = this;
        scripts = new List<PartyTimeSlave>();
    }

    void Start()
    {
        PartyTimeInfo.currentTrackPartyTimes = PartyTimeInfo.flare_PartyTimes;
        PartyTimeInfo.currentTrackBeats = PartyTimeInfo.flare_Beats;
        nextIndex = 0;
    }

    void Update()
    {
        HandleBeats();

        HandlePartyTimes();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (inPartyTime)
            {
                ExitPartyTime();
            }
            else
            {
                EnterPartyTime();
            }
        }
    }

    void Beat()
    {
        PartyTimeInfo.beat = true;

        foreach (PartyTimeSlave i in scripts)
        {
            i.TriggerMe();
        }
    }

    void Looped()
    {
        nextIndex = 0;
        nextPartyTimeIndex = 0;
    }

    void HandlePartyTimes()
    {
        float mTime = xa.music_Time;

        if (nextPartyTimeIndex < PartyTimeInfo.currentTrackPartyTimes.Length)
        {
            if (mTime >= PartyTimeInfo.currentTrackPartyTimes[nextPartyTimeIndex])
            {
                TogglePartyTime();
                nextPartyTimeIndex++;
            }
        }
    }
    float stepTime = 0;
    void HandleBeats()
    {
        float mTime = xa.music_Time;
        PartyTimeInfo.beat = false;
        if (mTime > 2) { past2Seconds = true; }
        if (past2Seconds && mTime < 2) { /*Debug.Log("Looped");*/ Looped(); past2Seconds = false; }

        if (nextIndex < PartyTimeInfo.currentTrackBeats.Length)
        {
            if (PartyTimeInfo.currentTrackBeats[nextIndex].y == 0)
            {
                //Just a normal beat
                if (mTime >= PartyTimeInfo.currentTrackBeats[nextIndex].x)
                {
                    Beat();
                    nextIndex++;
                }
            }
            else
            {
                if (stepTime == 0)
                {
                    stepTime = PartyTimeInfo.currentTrackBeats[nextIndex].x;
                }
                if (mTime >= stepTime)
                {
                    Beat();
                    stepTime += PartyTimeInfo.currentTrackBeats[nextIndex].y;
                    if (stepTime >= PartyTimeInfo.currentTrackBeats[nextIndex].z)
                    {
                        nextIndex++;
                        stepTime = 0;
                    }
                }
            }
        }
    }

    void TogglePartyTime()
    {
        if (inPartyTime)
        {
            ExitPartyTime();
        }
        else
        {
            EnterPartyTime();
        }
    }

    void EnterPartyTime()
    {
        inPartyTime = true;
        //Fade in the party time gameobjects
        if (partyTimeParticles != null)
        {
            foreach (ParticleSystem ps in partyTimeParticles)
            {
                //go.SetActive(true);
                ps.Play();
            }
        }

        if (partyTimeGOs != null)
        {
            foreach (GameObject go in partyTimeGOs)
            {
                //go.SetActive(true);
                iTweenEvent.GetEvent(go, "ScaleIn").Play();
            }
        }
        if (partyTimeGOsToSetVisible != null)
        {
            foreach (GameObject go in partyTimeGOsToSetVisible)
            {
                go.GetComponent<Renderer>().enabled = true;
            }
        }

        if (nonPartyTimeGOsToSetVisible != null)
        {
            foreach (GameObject go in nonPartyTimeGOsToSetVisible)
            {
                go.GetComponent<Renderer>().enabled = false;
                //iTweenEvent.GetEvent(go, "ScaleOut").Play();
            }
        }

    }

    void ExitPartyTime()
    {
        inPartyTime = false;
        if (partyTimeParticles != null)
        {
            foreach (ParticleSystem ps in partyTimeParticles)
            {
                ps.Stop();
                ps.Clear();
            }
        }
        if (partyTimeGOs != null)
        {
            foreach (GameObject go in partyTimeGOs)
            {
                //go.SetActive(false);
                iTweenEvent.GetEvent(go, "ScaleOut").Play();
            }
        }
        if (partyTimeGOsToSetVisible != null)
        {
            foreach (GameObject go in partyTimeGOsToSetVisible)
            {
                go.GetComponent<Renderer>().enabled = false;
            }
        }

        if (nonPartyTimeGOsToSetVisible != null)
        {
            foreach (GameObject go in nonPartyTimeGOsToSetVisible)
            {
                go.GetComponent<Renderer>().enabled = true;
                //iTweenEvent.GetEvent(go, "ScaleIn").Play();
            }
        }

    }
}
