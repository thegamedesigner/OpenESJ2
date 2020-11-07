using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaHoveringScript : MonoBehaviour
{
	public GameObject missile;
	public GameObject giftBullet;
	public GameObject giftBomb;
	public GameObject wobblegift;
	public HealthScript healthScript;
	public FreshAni aniScript;
	public GameObject healthBar;
	public GameObject redHealthBar;
	public GameObject puppet;
	public GameObject SantaBoot;
	public GameObject SantaChunk;
	public GameObject TelegraphAttack;
	public GameObject endPortal;
	public GameObject textPrefab;
	public GameObject textCreationPoint;

	bool Phase1 = true;
	float phase1TimeSet;
	float firingAngle = 0;

	bool Phase2 = true;
	float phase2TimeSet;
	float phase2firingAngle = 0;

	bool Phase3 = true;
	float phase3TimeSet;


	bool ConstantMissiles = false;
	float missileTimeSet;

	public bool attacking = false;
	float attackTimeSet;
	bool warned = false;
	float secondTimeSet;
	int attackIndex = 0;

	int realHealth = 6;
	bool currentlyDead = false;
	float deathTimeSet;

	GameObject textGO;
	TextMesh textGOMesh;

	void Start()
	{
		phase1TimeSet = 1.5f;
		missileTimeSet = 0;

		iTween.MoveBy(puppet, iTween.Hash("y", 0.5f, "time", 4, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
	}

	void Update()
	{
		if (attacking)
		{
			if (warned == false)
			{
				if (fa.time >= (attackTimeSet + 2.8f))
				{
					attackTimeSet = fa.time;
					secondTimeSet = fa.time;
					Instantiate(TelegraphAttack, transform.position, Quaternion.Euler(0, 0, 0));
					warned = true;

					textGO = Instantiate(textPrefab, textCreationPoint.transform.position, textCreationPoint.transform.rotation);
					textGOMesh = textGO.GetComponentInChildren<TextMesh>();
					int ran = Random.Range(0,15);
					switch(ran)
					{
						case 0:textGOMesh.text = "Ho Ho Ho";break;
						case 1:textGOMesh.text = "Suck my Jingleballs!";break;
						case 2:textGOMesh.text = "Reindeer jerky? I have plenty.";break;
						case 3:textGOMesh.text = "I'm gonna deck your halls!";break;
						case 4:textGOMesh.text = "Ho Ho Holy Shit!";break;
						case 5:textGOMesh.text = "Suck on this!";break;
						case 6:textGOMesh.text = "Ho ho ho, bitches!";break;
						case 7:textGOMesh.text = "Merry Christmas!";break;
						case 8:textGOMesh.text = "You've been naughty!";break;
						case 9:textGOMesh.text = "Unwrap this!";break;
						case 10:textGOMesh.text = "Gonna stuff your stocking!";break;
						case 11:textGOMesh.text = "Check out my sack!";break;
						case 12:textGOMesh.text = "Gonna kick you in the christmas puddings!";break;
						case 13:textGOMesh.text = "Right up your chimney!";break;
						case 14:textGOMesh.text = "Naughty, naughty, naughty!";break;

					}
					
					iTween.FadeTo(textGO, iTween.Hash("alpha", 1, "time", 0.4f, "easetype", iTween.EaseType.easeOutSine));
					iTween.MoveBy(textGO, iTween.Hash("y",1, "time", 0.4f, "easetype", iTween.EaseType.easeOutSine));

					iTween.FadeTo(textGO, iTween.Hash("delay", 2, "alpha", 0, "time", 0.4f, "easetype", iTween.EaseType.easeOutSine));
					iTween.MoveBy(textGO, iTween.Hash("delay", 2, "y",1, "time", 0.4f, "easetype", iTween.EaseType.easeOutSine));

				}
			}
			else
			{
				if (fa.time >= (secondTimeSet + 0.4f))
				{
					attackTimeSet = fa.time;
					secondTimeSet = fa.time;
					warned = false;

					attackIndex++;
					if (attackIndex == 3) { attackIndex = 0; }
					switch (attackIndex)
					{
						case 0:
							Instantiate(giftBomb, transform.position, Quaternion.Euler(0, 0, 45));
							Instantiate(giftBomb, transform.position, Quaternion.Euler(0, 0, -45));
							Instantiate(giftBomb, transform.position, Quaternion.Euler(0, 0, 180 + 45));
							Instantiate(giftBomb, transform.position, Quaternion.Euler(0, 0, 180 + -45));
							break;
						case 1:
							for (int i = 15; i < 375; i += 30)
							{
								Instantiate(wobblegift, transform.position, Quaternion.Euler(0, 0, i));
							}
							break;
						case 2:
							Instantiate(missile, transform.position, Quaternion.Euler(0, 0, 15));
							Instantiate(missile, transform.position, Quaternion.Euler(0, 0, 30));
							Instantiate(missile, transform.position, Quaternion.Euler(0, 0, 45));
							Instantiate(missile, transform.position, Quaternion.Euler(0, 0, 180 + -15));
							Instantiate(missile, transform.position, Quaternion.Euler(0, 0, 180 + -30));
							Instantiate(missile, transform.position, Quaternion.Euler(0, 0, 180 + -45));
							break;
					}
				}
			}
		}

		/*
		if (fa.time >= (phase1TimeSet + 3))
		{
			phase1TimeSet = fa.time;
			if (Phase1)
			{
				Instantiate(giftBomb, transform.position, Quaternion.Euler(0, 0, firingAngle));
				firingAngle -= 45;
			}
		}

		if (fa.time >= (phase2TimeSet + 6))
		{
			phase2TimeSet = fa.time;
			if (Phase2)
			{
				for (int i = 0; i < 360; i += 30)
				{
					Instantiate(wobblegift, transform.position, Quaternion.Euler(0, 0, i));
				}
			}
		}

		if (fa.time >= (phase3TimeSet + 3))
		{
			phase3TimeSet = fa.time;
			if (Phase3)
			{
				Instantiate(giftBullet, transform.position, Quaternion.Euler(0, 0, 0));
				Instantiate(giftBullet, transform.position, Quaternion.Euler(0, 0, 15));
				Instantiate(giftBullet, transform.position, Quaternion.Euler(0, 0, -15));
				Instantiate(giftBullet, transform.position, Quaternion.Euler(0, 0, 180 + 0));
				Instantiate(giftBullet, transform.position, Quaternion.Euler(0, 0, 180 + 15));
				Instantiate(giftBullet, transform.position, Quaternion.Euler(0, 0, 180 + -15));
			}
		}

		if (fa.time >= (missileTimeSet + 3))
		{
			missileTimeSet = fa.time;
			if (ConstantMissiles)
			{
				Instantiate(missile, transform.position, Quaternion.Euler(0, 0, 5));
			}
		}
		*/
		if (healthScript.health <= 0)
		{
			if (!currentlyDead)
			{
				realHealth -= 1;
				if (realHealth < 0) { realHealth = 0; }

				if (realHealth <= 0)
				{
					aniScript.PlayAnimation(2);
					currentlyDead = true;
					deathTimeSet = 9999;
					attacking = false;
					iTween.ScaleTo(redHealthBar, iTween.Hash("x", 0, "time", 1, "easetype", iTween.EaseType.easeInOutSine));
					iTween.MoveBy(redHealthBar, iTween.Hash("x", -1.5f, "time", 1, "easetype", iTween.EaseType.easeInOutSine));

					iTween.MoveBy(healthBar, iTween.Hash("delay", 5, "y", 24, "time", 4, "easetype", iTween.EaseType.easeInSine));

					Instantiate(SantaBoot, transform.position, transform.rotation);
					Instantiate(SantaBoot, transform.position, transform.rotation);
					Instantiate(SantaChunk, transform.position, transform.rotation);
					Instantiate(SantaChunk, transform.position, transform.rotation);
					Instantiate(SantaChunk, transform.position, transform.rotation);

					endPortal.SetActive(true);
				}
				else
				{
					currentlyDead = true;
					deathTimeSet = fa.time;

					//spray blood
					//play dead animation
					aniScript.PlayAnimation(1);

					iTween.ScaleTo(redHealthBar, iTween.Hash("x", realHealth * 3, "time", 1, "easetype", iTween.EaseType.easeInOutSine));
					iTween.MoveBy(redHealthBar, iTween.Hash("x", -1.5f, "time", 1, "easetype", iTween.EaseType.easeInOutSine));

				}
			}
			else
			{
				if (fa.time >= (deathTimeSet + 3) && realHealth > 0)
				{
					//revive
					currentlyDead = false;
					healthScript.health = 100;
					aniScript.PlayAnimation(0);
				}
			}
		}
	}
}
