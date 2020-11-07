using UnityEngine;

public class GC_Bard2Script : MonoBehaviour
{
    /*
     * For handling new Groove City things with music (such as the meta world).
     * Just leave the old Bard silent for that level, and use this one.
     */

    bool onMetaLevel = false;
    float desiredVolume = 0;
    float rampDownSpeed = 1;
    float rampUpSpeed = 1;
    float localVolume = 0;


    void Start()
    {
        //Setup bard
      //  if (!xa.bard2) { DontDestroyOnLoad(this.gameObject); xa.bard2 = this.gameObject; xa.bard2Script = this; }
       // else { Setup.GC_DebugLog("Another bard2 exists, destroying self."); Destroy(this.gameObject); return; }
    }

    void Update()
    {
        onMetaLevel = checkIfOnMetaLevel();
        if (onMetaLevel)
        {
            desiredVolume = 1;
            if (!GetComponent<AudioSource>().isPlaying) { GetComponent<AudioSource>().Play(); GetComponent<AudioSource>().loop = true; }
        }
        else
        {
            desiredVolume = 0;
        }

        setMusicToDesiredVolume();
    }

    bool checkIfOnMetaLevel()
    {
		string loadedLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (loadedLevel == "YazarGC_World" ||
            loadedLevel == "YazarGC_Level1" ||
            loadedLevel == "YazarGC_Controls" ||
            loadedLevel == "YazarGC_Options" ||
            loadedLevel == "YazarGC_MenuTutorial")
        {
            return true;
        }

        return false;
    }

    void setMusicToDesiredVolume()
    {

        GetComponent<AudioSource>().volume = xa.musicVolume * xa.muteMusic * localVolume;

        if (desiredVolume < localVolume)
        {
            localVolume -= rampDownSpeed * fa.deltaTime;

            if (desiredVolume > localVolume) { localVolume = desiredVolume; }

        }
        if (desiredVolume > localVolume)
        {
            localVolume += rampUpSpeed * fa.deltaTime;

            if (desiredVolume < localVolume) { localVolume = desiredVolume; }

        }
    }
}
