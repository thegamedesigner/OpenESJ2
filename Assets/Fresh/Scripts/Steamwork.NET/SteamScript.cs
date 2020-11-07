using UnityEngine;
using System.Collections;
using Steamworks;

public class SteamScript : MonoBehaviour
{
	public static string GetSteamAccountName()
	{
		if (SteamManager.Initialized)
		{
			string name = SteamFriends.GetPersonaName();
			Debug.Log(name);
            return name;
		}
		return null;
	}

    //This is stuff for activating the steam overlay. I'm ignoring it until I need it.

    protected Callback<GameOverlayActivated_t> m_GameOverlayActivated;
	private void OnEnable()
	{
		if (SteamManager.Initialized)
		{
			m_GameOverlayActivated = Callback<GameOverlayActivated_t>.Create(OnGameOverlayActivated);
		}
	}

	private void OnGameOverlayActivated(GameOverlayActivated_t pCallback)
	{
		if (pCallback.m_bActive != 0)
		{
			Debug.Log("Steam Overlay has been activated");
		}
		else
		{
			Debug.Log("Steam Overlay has been closed");
		}
	}
}
