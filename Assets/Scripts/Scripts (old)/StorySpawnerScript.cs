using UnityEngine;

public class StorySpawnerScript : MonoBehaviour
{
	public static bool finishedGame = false;
    public enum stories { ESJ_intro, ESJ_outro, GC_intro, GC_outro, ESJ2_intro, ESJ2_outro, ESJ2_postSatan, ESJ2_preslimedaddy, ESJ2_AlpDLC_Intro, ESJ2_AlpDLC_Outro }
	public string goToThisLevel = "";
	public GameObject obj;
    public float spawnSpeed = 0;
    public stories story = stories.ESJ_intro;
	float counter = 0;
	TextMesh textMesh;
	int index = 0;
	void Start()
	{
		counter = 999;
		
		if(story == stories.ESJ2_outro)
		{
			AchivoFuncs.GetAchivo(AchivoFuncs.Achivos.Achivo_Champion);
		}

	}

	void Update()
    {
        if (Controls.GetAnyKeyDownOnce())
		{
		//	Debug.Log("PRESSED KEY IN STORY SPAWNER ONCE");
			counter = 999;
		}
		counter += 10 * fa.deltaTime;
		if (counter >= spawnSpeed)
		{
			counter = 0;

			if (obj)
			{

				xa.glx = transform.position;
				xa.tempobj = (GameObject)(Instantiate(obj, xa.glx, xa.null_quat));
                textMesh = xa.tempobj.GetComponentInChildren<TextMesh>();
				string loadedLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

				
				if (story == stories.ESJ2_AlpDLC_Intro)
                {
					textMesh.text = "INVALID INDEX: " + index;
                    if (index == 0) { textMesh.text = "A long time ago..."; }
                    if (index == 1) { textMesh.text = "You defeated the Groove Wizard."; }
                    if (index == 2) { textMesh.text = "God, that was so hot"; }
                    if (index == 3) { textMesh.text = "...anyway..."; }
                    if (index == 4) { textMesh.text = "Some rumblings have been heard..."; }
                    if (index == 5) { textMesh.text = "...coming from..."; }
                    if (index == 6) { textMesh.text = "The Groove Wizard's tower!"; }
                    if (index == 7) { textMesh.text = "Could it be his return?"; }
                    if (index == 8) { textMesh.text = "You'll have to go to the tower..."; }
                    if (index == 9) { textMesh.text = "...and check it out!"; }
                    if (index == 10) { textMesh.text = "But first..."; }
                    if (index == 11) { textMesh.text = "...there's a party for you..."; }
                    if (index == 12) { textMesh.text = "...Those Boogie Bros are so nice!"; }
                    if (index == 13) { textMesh.text = "...So kind to you!"; }
                    if (index == 14) { textMesh.text = "...What a nice party..."; }
                    if (index == 15) { textMesh.text = ""; }
                    if (index == 16)
                    {
                        Setup.callFadeOutFunc(goToThisLevel, true, loadedLevel);
                        this.enabled = false;
                        return;
                    }
                }
				if (story == stories.ESJ2_AlpDLC_Outro)
                {
					textMesh.text = "INVALID INDEX: " + index;
                    if (index == 0) { textMesh.text = "And so..."; }
                    if (index == 1) { textMesh.text = "The Groove Wizard wasn't coming back after all,"; }
                    if (index == 2) { textMesh.text = "The grumbling noises were..."; }
                    if (index == 3) { textMesh.text = "Just his pet..."; }
                    if (index == 4) { textMesh.text = "His REALLY HUNGRY pet..."; }
                    if (index == 5) { textMesh.text = "You were the one to sastify his hunger."; }
                    if (index == 6) { textMesh.text = "..."; }
                    if (index == 7) { textMesh.text = "...We just won't mention this..."; }
                    if (index == 8) { textMesh.text = "...to the other Boogie Bros..."; }
                    if (index == 9) { textMesh.text = "..."; }
                    if (index == 10) { textMesh.text = ""; }
                    if (index == 11)
                    {
                        Setup.callFadeOutFunc(goToThisLevel, true, loadedLevel);
                        this.enabled = false;
                        return;
                    }
                }

				if (story == stories.ESJ2_intro)
                {
                    if (index == 0) { textMesh.text = "The Devil has a sexy butt..."; }
                    if (index == 1) { textMesh.text = "It's made of pure gold..."; }
                    if (index == 2) { textMesh.text = "And grants magic wishes."; }
                    if (index == 3) { textMesh.text = "So, of course..."; }
                    if (index == 4) { textMesh.text = "You're gonna steal it."; }
                    if (index == 5) { textMesh.text = "This is the story..."; }
                    if (index == 6) { textMesh.text = "Of your quest..."; }
                    if (index == 7) { textMesh.text = "To Steal the Devil's Butt."; }
                    if (index == 8) { textMesh.text = ""; }
                    if (index == 9)
                    {
                        Setup.callFadeOutFunc(goToThisLevel, true, loadedLevel);
                        this.enabled = false;
                        return;
                    }
                }
				if (story == stories.ESJ2_postSatan)
                {
                    if (index == 0) { textMesh.text = "Satan is Defeated!"; }
                    if (index == 1) { textMesh.text = "You grab the golden buns..."; }
                    if (index == 2) { textMesh.text = "The choice cheeks..."; }
                    if (index == 3) { textMesh.text = "The glistening booty-bumps..."; }
                    if (index == 4) { textMesh.text = "The shining sin-mounds..."; }
                    if (index == 5) { textMesh.text = "you wish a magic wish!"; }
                    if (index == 6) { textMesh.text = "You wish that..."; }
                    if (index == 7) { textMesh.text = "...Your dear old mum..."; }
                    if (index == 8) { textMesh.text = "...wouldn't have to be so lonely!"; }
                    if (index == 9) { textMesh.text = "With a poof, the butt vanishes!"; }
                    if (index == 10) { textMesh.text = "Chapter 2: Mum's Adventure"; }
                    if (index == 11) { textMesh.text = ""; }
                    if (index == 12)
                    {
                        Setup.callFadeOutFunc(goToThisLevel, true, loadedLevel);
                        this.enabled = false;
                        return;
                    }
                }
				if (story == stories.ESJ2_preslimedaddy)
                {
                    if (index == 0) { textMesh.text = "What!! You're dating him?!?"; }
                    if (index == 1) { textMesh.text = "Mom: Meet your new dad!"; }
                    if (index == 2) { textMesh.text = "But... giant tentacles..."; }
                    if (index == 3) { textMesh.text = "Mom: Yes, he is quite large"; }
                    if (index == 4) { textMesh.text = "Arggg!"; }
                    if (index == 5) { textMesh.text = "Mom: Go play catch outside!"; }
                    if (index == 6) { textMesh.text = "Slime-Daddy: Yes, let's play catch..."; }
                    if (index == 7) { textMesh.text = "Slime-Daddy: ...Son"; }
                    if (index == 8) { textMesh.text = "....."; }
                    if (index == 9) { textMesh.text = "Dad!"; }
                    if (index == 10) { textMesh.text = "Chapter 3: A New Family!"; }
                    if (index == 11) { textMesh.text = ""; }
                    if (index == 12)
                    {
                        Setup.callFadeOutFunc(goToThisLevel, true, loadedLevel);
                        this.enabled = false;
                        return;
                    }
                }
                if (story == stories.ESJ2_outro)
                {
                    if (index == 1) { textMesh.text = "You escaped Hell..."; }
                    if (index == 2) { textMesh.text = "Grabbed Mega-Satan's Butt..."; }
                    if (index == 3) { textMesh.text = "...and made one, final, wish..."; }
                    if (index == 4) { textMesh.text = "For a really cool motorbike! "; }
                    if (index == 5) { textMesh.text = "VROOM!"; }
                    if (index == 6) { textMesh.text = "Ah yeah! Totally cool!"; }
                    if (index == 7) { textMesh.text = "...and then you took your mom out for sushi."; }
                    if (index == 8) { textMesh.text = "Life is good."; }
                   // if (index == 0) { textMesh.text = "...and then..."; }
                  //  if (index == 1) { textMesh.text = "The game ended..."; }
                  //  if (index == 2) { textMesh.text = "...Just like that."; }
                  //  if (index == 3) { textMesh.text = "<INSERT COOL STORY HERE>"; }
                    if (index == 9) { textMesh.text = ""; }
                    if (index == 10)
                    {
						finishedGame = true;
                        Setup.callFadeOutFunc(goToThisLevel, true, loadedLevel);
                        this.enabled = false;
                        return;
                    }
                }
                if (story == stories.GC_outro)
                {

                    if (index == 0) { textMesh.text = "After the dust settled..."; }
                    //if (index == 0) { textMesh.text = "And so Dr.Swinger was defeated..."; }
                   // if (index == 0) { textMesh.text = "Biggles managed to scrap most of Dr.Swinger off his Propellor"; }
                    if (xa.pgMode)
                    {
                        if (index == 1) { textMesh.text = "JoJo got her Laser-Noodles back."; }
                    }
                    else
                    {
                        if (index == 1) { textMesh.text = "JoJo got her Laser-Nipples back."; }
                    }
                    if (index == 2) { textMesh.text = "The Boogie Brothers repaired the city."; }
                    if (index == 3) { textMesh.text = "And Dr. Swinger was never seen again."; }
                    if (index == 4) { textMesh.text = "The Pope went back to being the Pope."; }
                    if (index == 5) { textMesh.text = "...But by night, dons a cape and becomes..."; }
                    if (index == 6) { textMesh.text = "The Mysterious El Pope!"; }
                    if (index == 7) { textMesh.text = "Crime Fighter!"; }
                    if (index == 8) { textMesh.text = "And you?"; }
                    if (index == 9) { textMesh.text = "You, my friend, worked hard."; }
                    //if (index == 10) { textMesh.text = "Practicing every night,"; }
                    if (index == 10) { textMesh.text = "Studying around the world,"; }
                    //if (index == 10) { textMesh.text = "Studied under expert teachers."; }
                    //if (index == 12) { textMesh.text = "Researching long into the night,"; }
                    //if (index == 10) { textMesh.text = "Stayed up late."; }
                    //if (index == 10) { textMesh.text = "Studied Dilligently"; }
                    //if (index == 10) { textMesh.text = "Studied under expert teachers."; }
                    //if (index == 11) { textMesh.text = "Read every book."; }
                    //if (index == 12) { textMesh.text = "Researched and Developed."; }
                    //if (index == 13) { textMesh.text = "Learned from Experts."; }
                    if (index == 11) { textMesh.text = "And became..."; }
                    if (xa.pgMode)
                    {
                        if (index == 12) { textMesh.text = "The World's Greatest Farter!"; }
                    }
                    else
                    {
                        if (index == 12) { textMesh.text = "The World's Greatest Lover!"; }
                    }

                    if (index == 13) { textMesh.text = "Now... come back to bed."; }
                    // if (index == 10) { textMesh.text = "When you find the Wizard,"; }
                    //  if (index == 11) { textMesh.text = "You'll make Him pay"; }
                    //  if (index == 12) { textMesh.text = "for your Butt."; }
                    // if (index == 10) { textMesh.text = "By finding that Wizard..."; }
                    // if (index == 11) { textMesh.text = "And destroying him..."; }
                    // if (index == 12) { textMesh.text = "At a dance off."; }
                    if (index == 14) { textMesh.text = ""; }
                    if (index == 15)
                    {
                        Setup.callFadeOutFunc(goToThisLevel, true, loadedLevel);
                        this.enabled = false;
                        return;
                    }
                }
                if (story == stories.ESJ_outro)
                {

                    if (index == 0) { textMesh.text = "In the years that followed..."; }
                    if (index == 1) { textMesh.text = "Boogie Brother Boris became the Pope."; }
                    if (index == 2) { textMesh.text = "He was known as Pope Boris the Super-Sexy."; }
                    if (index == 3) { textMesh.text = "The giant monsters, Glorg and Moorg, had many children..."; }
                    if (index == 4) { textMesh.text = "...much to the terror of the civilized world."; }
                    if (index == 5) { textMesh.text = "And you. You kept wandering."; }
                    if (index == 6) { textMesh.text = "Place to place, world to world."; }
                    if (index == 7) { textMesh.text = "Never quite settling down."; }
                    if (index == 8) { textMesh.text = "Aw well."; }
                    if (index == 9) { textMesh.text = "THE END"; }
                    // if (index == 10) { textMesh.text = "When you find the Wizard,"; }
                    //  if (index == 11) { textMesh.text = "You'll make Him pay"; }
                    //  if (index == 12) { textMesh.text = "for your Butt."; }
                    // if (index == 10) { textMesh.text = "By finding that Wizard..."; }
                    // if (index == 11) { textMesh.text = "And destroying him..."; }
                    // if (index == 12) { textMesh.text = "At a dance off."; }
                    if (index == 10) { textMesh.text = ""; }
                    if (index == 11)
                    {
                        Setup.callFadeOutFunc(goToThisLevel, true, loadedLevel);
                        this.enabled = false;
                        return;
                    }
                }
                if (story == stories.ESJ_intro) 
				{

					if (index == 0) { textMesh.text = "You lost an Arm,"; }
					if (index == 1) { textMesh.text = "In the Disco Wars of 1515."; }
					if (index == 2) { textMesh.text = "You lost an Eye,"; }
					if (index == 3) { textMesh.text = "In the War of Rock 'n Roll."; }
					if (index == 4) { textMesh.text = "You lost both legs,"; }
					if (index == 5) { textMesh.text = "Defeating DJ Deadly Skillz."; }
					if (index == 6) { textMesh.text = "And you lost your ENTIRE Butt,"; }
					if (index == 7) { textMesh.text = "To an Evil Wizard."; }
					if (index == 8) { textMesh.text = "This is the story of your Quest..."; }
					if (index == 9) { textMesh.text = "To get revenge for your Butt."; }
					// if (index == 10) { textMesh.text = "When you find the Wizard,"; }
					//  if (index == 11) { textMesh.text = "You'll make Him pay"; }
					//  if (index == 12) { textMesh.text = "for your Butt."; }
					// if (index == 10) { textMesh.text = "By finding that Wizard..."; }
					// if (index == 11) { textMesh.text = "And destroying him..."; }
					// if (index == 12) { textMesh.text = "At a dance off."; }
					if (index == 10) { textMesh.text = ""; }
					if (index == 11)
					{
						Setup.callFadeOutFunc(goToThisLevel, true, loadedLevel);
						this.enabled = false;
						return;
					}
				}
				index++;

			 //   xa.tempobj.transform.parent = xa.createdObjects.transform;
			}

		}

	}
}
