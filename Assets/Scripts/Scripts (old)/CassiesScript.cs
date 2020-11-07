using UnityEngine;

public class CassiesScript : MonoBehaviour
{
    //call an itween X times with X delay, and then change level
    public float delayInSeconds = 0;
    public float yDist = 0;
    public float iTweenSpeedInSeconds = 0;
    public int numberOfMovements = 0;
    public string lvl = "";

    int count = 0;
    float timeSet = 0;

    void Start()
    {
        timeSet = -99;
    }

    void Update()
    {
        if (fa.time > timeSet + delayInSeconds)
        {
            count += 1;
            if (count > numberOfMovements)
            {
				UnityEngine.SceneManagement.SceneManager.LoadScene(lvl);
            }

            timeSet = fa.time;
            iTween.MoveBy(this.gameObject, iTween.Hash("y", yDist, "time", iTweenSpeedInSeconds, "easetype", iTween.EaseType.easeInOutSine));
	
        }
    }
}
