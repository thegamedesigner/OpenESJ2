using UnityEngine;

public class OnceEnabledAlwaysReenableOnStart : MonoBehaviour
{
    public Behaviour enableThis = null;
    public static int lockedVariable = -1;

    void Start()
    {
        if (lockedVariable == UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex)
        {
            enableThis.enabled = true;
        }
        this.enabled = false;//Disables itself on start. Must start with box checked (enabled), to get this part out of the way.
    }

    void Update()
    {
        enableThis.enabled = true;
        lockedVariable = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        this.enabled = false;
    }
}
