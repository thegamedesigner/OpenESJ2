using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenButtScript : MonoBehaviour
{
	public GameObject explo;
	public ParticleSystem stars;
	public GameObject portal;
	bool pickedUp = false;
	void Start()
	{

	}

	void Update()
	{
		if (!pickedUp)
		{
			if (xa.player)
			{
				Vector3 vec1 = xa.player.transform.position;
				vec1.z = transform.position.z;
				if (Vector3.Distance(vec1, transform.position) < 1)
				{
					//Is this a unique collection?
					int uniqueID = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
					if (!PlayerPrefs.HasKey("goldenButt" + uniqueID))
					{
						int goldenButtsAmount = 0;
						if (PlayerPrefs.HasKey("goldenButtsCollected"))
						{
							goldenButtsAmount = PlayerPrefs.GetInt("goldenButtsCollected", 0);
						}
						goldenButtsAmount++;
						PlayerPrefs.SetInt("goldenButtsCollected", goldenButtsAmount);
						PlayerPrefs.SetInt("goldenButt" + uniqueID, 1);
						PlayerPrefs.Save();
						fa.goldenButtsCollected = goldenButtsAmount;
					}

					pickedUp = true;
					stars.Stop();
					stars.transform.SetParent(null);

					GameObject go = Instantiate(explo);
					go.transform.position = transform.position;

					Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Butt);
					portal.SetActive(true);
					Destroy(this.gameObject);

				}
			}
		}

	}
}
