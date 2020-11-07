using UnityEngine;
using System.Collections;

public class SetToCountdown : MonoBehaviour
{

    public static bool paused = false;
    TextMesh textMesh = null;
    float timeSet = 0;
    float delay = 1;

    
	void OnLevelWasLoaded()
    {
        xa.countdownSecondsLeft = xa.countdownSecondsTotal;
        timeSet = 0;

        textMesh = this.gameObject.GetComponent<TextMesh>();
        if (textMesh)
        {
            textMesh.text = getSecondsLeft();
        }
    }

    void Start()
    {
        textMesh = this.gameObject.GetComponent<TextMesh>();
        textMesh.text = string.Empty;
    }

    public static string getSecondsLeft()
    {
        return ("" + xa.countdownSecondsLeft);
    }

    void Update()
    {
        if (textMesh)
        {
            string level = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            if (level == "AChunkOfStory" || level == "OutroStory" || za.thisLevelShouldntHaveScoreOrTimers)
            {
                textMesh.text = string.Empty;
            }
            else
            {
                textMesh.text = getSecondsLeft();
            }
        }

        if (fa.time >= (timeSet + delay))
        {
            timeSet = fa.time;
            if (xa.countdownSecondsLeft <= 0)
            {
                xa.countdownSecondsLeft -= 3;
            }
            else
            {
                xa.countdownSecondsLeft -= 1;
            }
            //Setup.GC_DebugLog(xa.countdownSecondsLeft);
        }

    }
}
