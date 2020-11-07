using UnityEngine;
using System.Collections;

public class RandomSoundScript : MonoBehaviour
{
    public bool useNewSystem = false;
    public GameObject[] newSystem = new GameObject[0];

	public int numOfSounds = 0;
	public GameObject sound1;
	public GameObject sound2;
	public GameObject sound3;
	public GameObject sound4;
	public GameObject sound5;
	public GameObject sound6;

	void Start()
	{

        if (!za.killSoundEffects && xa.musicVolume > 0 && xa.muteSound != 0)
        {
            //xa.tempobj = (GameObject)(Instantiate(sound3, transform.position, xa.null_quat));

            if (useNewSystem)
            {
                xa.tempobj = (GameObject)(Instantiate(newSystem[Random.Range(0, newSystem.Length - 1)], transform.position, xa.null_quat));
                Destroy(this.gameObject);
            }
            else
            {

                float result = Random.Range(0, numOfSounds + 1);
                if (result > numOfSounds) { result = numOfSounds; }
                //Setup.GC_DebugLog(result);
                if (result <= 1) { xa.tempobj = (GameObject)(Instantiate(sound1, transform.position, xa.null_quat)); }
                else if (result <= 2) { xa.tempobj = (GameObject)(Instantiate(sound2, transform.position, xa.null_quat)); }
                else if (result <= 3) { xa.tempobj = (GameObject)(Instantiate(sound3, transform.position, xa.null_quat)); }
                else if (result <= 4) { xa.tempobj = (GameObject)(Instantiate(sound4, transform.position, xa.null_quat)); }
                else if (result <= 5) { xa.tempobj = (GameObject)(Instantiate(sound5, transform.position, xa.null_quat)); }
                else if (result <= 6) { xa.tempobj = (GameObject)(Instantiate(sound6, transform.position, xa.null_quat)); }
                Destroy(this.gameObject);
            }
        }
	}

	void Update()
	{

	}
}
