using UnityEngine;

public class UnlockThisLevel : MonoBehaviour
{

    void Update()
    {
        LevelInfo.unlocked[LevelInfo.getSceneNumFromName(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name)] = true;
       
        this.enabled = true;
    }
}
