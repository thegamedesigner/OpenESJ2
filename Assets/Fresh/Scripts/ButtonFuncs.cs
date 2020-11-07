using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFuncs : MonoBehaviour
{
	public void GoToPartyCity()
	{
		xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_MENU,"PartyCity");
	}
	public void GoToSantasCave()
	{
		xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_MENU,"SantasCave");
	}
	public void GoToBabylon()
	{
		xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_MENU,"Babylon");
	}
	
	public void GoToWeek1Level()
	{
		xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_MENU,"SamuraiSword1");
	}
	public void GoToWeek2Level()
	{
		xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_MENU,"SamuraiSword2");
	}
	public void GoToWeek3Level()
	{
		xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_MENU,"EntranceToBabylon");
	}
	public void GoToPink1Level()
	{
		xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_MENU,"Pink1");
	}
	
	public void GoToPink2Level()
	{
		xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_MENU,"Pink2");
	}
	
	public void GoToBlue1Level()
	{
		xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_MENU,"Blue1");
	}

	public void GoToMegaMeta()
	{
		xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_MENU,"MegaMetaWorld");
	}

}
