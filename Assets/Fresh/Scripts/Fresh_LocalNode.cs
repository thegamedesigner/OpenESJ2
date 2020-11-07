using UnityEngine;
using UnityEngine.UI;

public class Fresh_LocalNode : MonoBehaviour
{
	public bool isMenuLevel = false;
	public bool showMouse = false;
	public bool escapeDoesntOpenInGameMainMenu = false;
	public bool autoOpenStartMenu = false;
	public bool escapeQuitsToWorldMap = false;
	public bool escapeQuitsToTitle = false;
	public bool escapeQuitsToStartMenu = false;
	public bool escapeQuitsApplication = false;
	public bool loadLevelInfo = false;
	public bool dontLetRespawnAndRestartHappen = false;
	public bool ThisLevelHasMom = false;
	public bool OpenRawMenuOnStart = false;
	public bool ResetAchivoBools = false;

	public Text resetToDefaultControlsText;
	public TextMesh resetToDefaultControlsText2;

	float timeset = 0.0f;
	bool heldFor5Seconds = false;

	public void Awake()
	{
		xa.fresh_localNode = this;
		fa.isMenuLevel = this.isMenuLevel;

		if (this.showMouse)
		{

			Setup.SetCursor(Setup.C.Visible);
		}
		else
		{
			Setup.SetCursor(Setup.C.NotVisible);
		}
		fa.escapeDoesntTriggerInGameMainMenu = this.escapeDoesntOpenInGameMainMenu;

		if (this.loadLevelInfo)
		{
			FreshLevels.InitFreshLevelInfo();
		}

		if (autoOpenStartMenu)
		{

		}
	}

	public void Start()
	{
		if (OpenRawMenuOnStart)
		{
			Main.AskForMMLeaderboard();
			RawFuncs.self.MenuOn(RawInfo.MenuType.MainMenu);

			if (StorySpawnerScript.finishedGame)
			{
				//Debug.Log("OPENED ROLLING CREDITS");
				StorySpawnerScript.finishedGame = false;
				RawFuncs.self.MenuOn(RawInfo.MenuType.RollingCredits);
			}
		}

		if (ResetAchivoBools)
		{
			//this is the world map, or another pure menu level, so bools like "hasStomped" or "hasJumped3Times" can be reset
			NovaPlayerScript.hasStomped = false;
			NovaPlayerScript.has3rdJumped = false;

			MultiPlayerFuncs.multiplayerMode = false;//reset this in the world map
		}
	}

	public void Update()
	{


		if (escapeQuitsToTitle && !RawFuncs.InRawMenu && !RawFuncs.RawMenuHandover)
		{
			bool openMenu2 = false;
			if (Controls.EscapeUp()) { openMenu2 = true; }//
			if (openMenu2)
			{
				if (heldFor5Seconds)
				{
					heldFor5Seconds = false;
				}
				else
				{
					bool skip = false;
					if (RawFuncs.InRawMenu || RawFuncs.RawMenuHandover) { skip = true; }

					if (!skip)
					{
						xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_MENU, "ESJ2Title");
					}
				}
			}
		}

		bool openMenu = false;
		if (Controls.GetInputDown(Controls.Type.OpenMenu, 0)) { openMenu = true; }
		if (openMenu)
		{
			bool skip = false;
			//	if (RawFuncs.InRawMenu || RawFuncs.RawMenuHandover) { skip = true; }


			if (!skip)
			{
				if (this.escapeQuitsToWorldMap)
				{
					xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_MENU, "MegaMetaMap");
				}
				else if (this.escapeQuitsToStartMenu)
				{
					xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_MENU, "StartMenu");
				}
				else if (this.escapeQuitsApplication)
				{
					Application.Quit();
				}
			}
		}

	}
}
