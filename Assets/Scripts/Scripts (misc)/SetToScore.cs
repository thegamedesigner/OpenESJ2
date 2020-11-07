using UnityEngine;

public class SetToScore : MonoBehaviour
{
    TextMesh textMesh = null;

    void OnLevelWasLoaded()
    {
        textMesh = this.gameObject.GetComponent<TextMesh>();
        if (textMesh) { textMesh.text = "" + 0; }
        
    }

    void Start()
    {
        textMesh = this.gameObject.GetComponent<TextMesh>();
        textMesh.text = string.Empty;
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
                textMesh.text = ""+xa.displayScore;
            }
        }

    }
}
