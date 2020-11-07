using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogScript : MonoBehaviour
{
	public FreshAni aniScript;
	public GameObject floatingText;
	public GameObject goodboyText;
	public TextMesh textMesh;
	public TextMesh goodBoyTextMesh;

	bool standing = true;
	bool triggered = false;

	float barkingTimeset = 0;

	float pettingTimeset = 0;
	float pettingDelay = 0.5f;
	public GameObject pettingPrefab;
	public GameObject pettingMuzzlepoint;

	void Start()
	{
		iTween.FadeTo(floatingText, iTween.Hash("alpha", 0, "time", 0));
		floatingText.transform.AddY(1f);
		iTween.FadeTo(goodboyText, iTween.Hash("alpha", 0, "time", 0));
		goodboyText.transform.AddY(-1f);
		aniScript.PlayAnimation(0);
	}

	void Update()
	{
		xa.nearDog = false;
		if (xa.player)
		{
			if (Vector2.Distance(xa.player.transform.position, transform.position) < 2)
			{
				xa.nearDog = true;
				//Pet
				if (fa.time > (pettingTimeset + pettingDelay))
				{
					pettingTimeset = fa.time;
					pettingDelay = Random.Range(0.2f, 0.4f);
					pettingMuzzlepoint.transform.SetAngZ(Random.Range(-45, 45));
					pettingMuzzlepoint.transform.SetX(xa.player.transform.position.x);
					pettingMuzzlepoint.transform.SetY(xa.player.transform.position.y);
					pettingMuzzlepoint.transform.SetZ(15);
					GameObject go = Instantiate(pettingPrefab, pettingMuzzlepoint.transform.position, pettingMuzzlepoint.transform.rotation);

					go.GetComponent<WoofBulletScript>().speed = Random.Range(3, 6);
					int r = Random.Range(0, 8);
					if (r == 0) { go.GetComponentInChildren<TextMesh>().text = "Pet!"; }
					if (r == 1) { go.GetComponentInChildren<TextMesh>().text = "Pet!"; }
					if (r == 2) { go.GetComponentInChildren<TextMesh>().text = "Pet!"; }
					if (r == 3) { go.GetComponentInChildren<TextMesh>().text = "Pet!"; }
					if (r == 4) { go.GetComponentInChildren<TextMesh>().text = "Pet!"; }
					if (r == 5) { go.GetComponentInChildren<TextMesh>().text = "Pet!"; }
					if (r == 6) { go.GetComponentInChildren<TextMesh>().text = "Pet!"; }
					if (r == 7) { go.GetComponentInChildren<TextMesh>().text = "Boop!"; }
				}





				//Trigger
				if (!triggered)
				{


					int amount = 0;
					int uniqueID = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
					if (!PlayerPrefs.HasKey("puppy" + uniqueID))
					{
						if (PlayerPrefs.HasKey("puppiesCollected"))
						{
							amount = PlayerPrefs.GetInt("puppiesCollected", 0);
						}
						amount++;
						PlayerPrefs.SetInt("puppiesCollected", amount);
						PlayerPrefs.SetInt("puppy" + uniqueID, 1);
						PlayerPrefs.Save();
						fa.puppiesCollected = amount;
					}


					triggered = true;
					goodBoyTextMesh.text = "Good boy!";
					aniScript.PlayAnimation(1);
					iTween.FadeTo(goodboyText, iTween.Hash("alpha", 1, "time", 0.4f, "easetype", iTween.EaseType.easeOutSine));
					iTween.MoveBy(goodboyText, iTween.Hash("y", 1, "time", 0.4f, "easetype", iTween.EaseType.easeOutSine));
					iTween.FadeTo(goodboyText, iTween.Hash("delay", 1, "alpha", 0, "time", 0.4f, "easetype", iTween.EaseType.easeInSine));
					iTween.MoveBy(goodboyText, iTween.Hash("delay", 1, "y", 1, "time", 0.4f, "easetype", iTween.EaseType.easeInSine));
				}
			}

		}

		if (!triggered)
		{
			if (fa.time >= (barkingTimeset + 1.7f))
			{
				barkingTimeset = fa.time;
				textMesh.text = "Woof!";
				aniScript.PlayAnimation(3);
				floatingText.transform.AddY(-2f);
				iTween.FadeTo(floatingText, iTween.Hash("alpha", 1, "time", 0.4f, "easetype", iTween.EaseType.easeOutSine));
				iTween.MoveBy(floatingText, iTween.Hash("y", 1f, "time", 0.4f, "easetype", iTween.EaseType.easeOutSine));
				iTween.FadeTo(floatingText, iTween.Hash("delay", 1, "alpha", 0, "time", 0.4f, "easetype", iTween.EaseType.easeInSine));
				iTween.MoveBy(floatingText, iTween.Hash("delay", 1, "y", 1, "time", 0.4f, "easetype", iTween.EaseType.easeInSine));
			}
		}
	}


}
