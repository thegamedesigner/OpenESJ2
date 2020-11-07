using UnityEngine;

public class ChangeLevels : MonoBehaviour
{
    public string level = "";
    public bool winLevel = false;
    public bool useZaMenuSelectionBoxValue = false;
    public bool dontFastFade = false;
    public bool goToMobileMenuIfOnMobile = false;
    public bool goToWebLevelIfFromWebShareLink = false;
    public bool ignoreIfLevelIsLocked = false;
   // public int setPlayerControlsInt = -1;
    void Update()
    {
        //if (setPlayerControlsInt != -1)
        //{
            //SetPlayerXControls.playerNumberCurrentlyBeingSet = setPlayerControlsInt;
        //}

        xa.hasCheckpointed = false;//THIS MEANS THIS SCRIPT CAN ONLY EVER BE USED TO RESPAWN OR FULLY-RESTART A LEVEL. Not respawn at checkpoint

        if (useZaMenuSelectionBoxValue)
        {
            level = LevelInfo.sceneNames[za.menuSelectionBoxValue];
        }
		
        if (ignoreIfLevelIsLocked)
        {
            if (LevelInfo.unlocked[za.menuSelectionBoxValue])
            {
                Setup.callFadeOutFunc(level, !dontFastFade, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
                this.enabled = false;
            }
        }
        else
        {
            Setup.callFadeOutFunc(level, !dontFastFade, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            this.enabled = false;
        }
    }
}
