using UnityEngine;
using System.Collections;

public class playCheckpointSoundScript : MonoBehaviour
{
    void Update()
    {
	   Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Checkpoint);
       this.enabled = false;
    }
}
