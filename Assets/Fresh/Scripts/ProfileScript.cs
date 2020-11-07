using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileScript : MonoBehaviour
{
	public static ProfileScript self;

	public UINavScript selectOnLogIn;
	public UINavScript selectOnLogOut;
	public UINavScript selectCancel;//cancel button
	public UINavScript selectSignUpButton;//signup button
	public UINavScript selectEmailButton;//Email button for email input field in the forgot your pw menu
	public GameObject cancelContainer;
	public GameObject onlineContainer;
	public GameObject profileMenu;
	public GameObject LogInMenu;
	public GameObject LogInContainer;
	public GameObject SignUpContainer;
	public GameObject ForgotPwContainer;
	public GameObject offlineContainer;
	public GameObject termsContainer;
	public GameObject popup;
	public Text popupText;
	public GameObject waitingDisplay;
	public Image avatarImage;
	public AvatarType desiredAvatarType = AvatarType.None;
	public Text usernameText;
	public InputField signUp_UsernameText;
	public InputField signUp_Password;
	public InputField signUp_Email;
	public InputField logIn_UsernameText;
	public InputField logIn_Password;
	public Toggle ShowPasswordToggle;
	public Toggle RememberMeToggle;
	public Image RememberMeOverlay;
	string pwhash = null;

	public static string loadedUsername = "";
	public static string loadedPwhash = "";
	public static bool loadedRememberMe = false;

	public Library[] library = new Library[0];

	public enum AvatarType
	{
		None,
		Default,
		DancingGirl,
		PixelMonkey,
		DerpDemon,
		MiniPlayer,
		Offline,
		End
	}

	[System.Serializable]
	public class Library
	{
		public string label;
		public AvatarType type;
		public Sprite sprite;
	}

	void Start()
	{
		self = this;
		EverythingOff();
	}

	void Update()
	{
		if (ShowPasswordToggle.isOn)
		{
			if (logIn_Password.contentType != InputField.ContentType.Password)
			{
				logIn_Password.contentType = InputField.ContentType.Password;
				logIn_Password.ForceLabelUpdate();
			}
		}
		else
		{
			if (logIn_Password.contentType != InputField.ContentType.Standard)
			{
				logIn_Password.contentType = InputField.ContentType.Standard;
				logIn_Password.ForceLabelUpdate();
			}
		}
	}

	public void UpdateAvatarAndUsername()
	{
		if (RemoteData.myAccount != null)
		{
			//	Debug.Log("Updated username & avatar");
			SetAvatar(AvatarType.Default);
			usernameText.text = RemoteData.myAccount.username;
		}
		else
		{
			//	Debug.Log("ACCOUNT WAS NULL: Couldn't update username & avatar");
		}
	}

	public void SetAvatar(AvatarType aType)
	{
		//update it
		RemoteData.myAccount.avatarType = aType;

		//find the new sprite
		for (int i = 0; i < library.Length; i++)
		{
			if (library[i].type == aType)
			{
				avatarImage.sprite = library[i].sprite;
			}
		}
	}

	public void EverythingOff()
	{
		offlineContainer.SetActive(true);
		onlineContainer.SetActive(false);
		cancelContainer.SetActive(false);
		LogInMenu.SetActive(false);
		LogInContainer.SetActive(false);
		SignUpContainer.SetActive(false);
		ForgotPwContainer.SetActive(false);
		popup.SetActive(false);
		termsContainer.SetActive(false);
		waitingDisplay.SetActive(false);
	}

	public void ToggleProfileMenu()
	{
		profileMenu.SetActive(!profileMenu.activeSelf);
	}

	public void ClickedOnLogIn()
	{
		pwhash = null;
		TryAutoLogin();
		EverythingOff();
		cancelContainer.SetActive(true);
		LogInMenu.SetActive(true);
		LogInContainer.SetActive(true);

		RememberMeToggle.isOn = loadedRememberMe;
		RememberMeClicked();
	}

	public void PopUpTerms()
	{
		termsContainer.SetActive(true);
	}

	public void AgreeWithTerms()
	{
		EverythingOff();
		
		LogInMenu.SetActive(true);
		SignUpContainer.SetActive(true);
	}

	public void CancelTerms()
	{
		ClickedOnLogInMenuCancel();
	}

	public void ClickedOnLogOut()
	{
		offlineContainer.SetActive(true);
		onlineContainer.SetActive(false);
		cancelContainer.SetActive(false);

		profileMenu.SetActive(false);
		LogInMenu.SetActive(false);
		popup.SetActive(false);
		desiredAvatarType = AvatarType.Default;
	}

	public void ClickedOnSignUp()
	{
		EverythingOff();

		cancelContainer.SetActive(true);
		termsContainer.SetActive(true);
		SignUpContainer.SetActive(true);

	}

	public void RememberMeClicked()
	{
		if (RememberMeToggle.isOn)
		{
			RememberMeOverlay.gameObject.SetActive(true);

			if (logIn_UsernameText.text != "")
			{
				loadedUsername = logIn_UsernameText.text;//This has just been turned on, so use the current settings

				if (logIn_Password.text != "gtdxitck")
				{
					loadedPwhash = RemoteData.GeneratePwHash(logIn_Password.text);
					Debug.Log("Pw: " + logIn_Password.text + ", Hash: " + loadedPwhash);
					Fresh_Saving.SaveUsernameAndPw(loadedUsername, loadedPwhash);
				}
			}

			logIn_UsernameText.text = loadedUsername;
			logIn_Password.text = "gtdxitck";

			Fresh_Saving.SaveRememberMe(true);
		}
		else
		{
			RememberMeOverlay.gameObject.SetActive(false);
			logIn_UsernameText.text = "";
			logIn_Password.text = "";


			Fresh_Saving.SaveRememberMe(false);
		}
	}

	public void ClickedOnLogInMenuCancel()
	{
		EverythingOff();
		offlineContainer.SetActive(true);
	}


	public void CancelOutOfSignUp()
	{
		EverythingOff();
		offlineContainer.SetActive(true);
	}

	public void SwitchToForgotPw()
	{
		offlineContainer.SetActive(false);
		onlineContainer.SetActive(false);
		cancelContainer.SetActive(true);

		LogInMenu.SetActive(true);
		LogInContainer.SetActive(false);
		SignUpContainer.SetActive(false);
		ForgotPwContainer.SetActive(true);
		
	}

	public void SwitchToSignUp()
	{
		offlineContainer.SetActive(false);
		onlineContainer.SetActive(false);
		cancelContainer.SetActive(true);

		LogInMenu.SetActive(true);
		LogInContainer.SetActive(false);
		SignUpContainer.SetActive(true);
		ForgotPwContainer.SetActive(false);
	}

	public void OpenPop(string errorText)
	{
		offlineContainer.SetActive(false);
		onlineContainer.SetActive(false);
		cancelContainer.SetActive(true);

		LogInMenu.SetActive(false);
		LogInContainer.SetActive(false);
		SignUpContainer.SetActive(false);
		ForgotPwContainer.SetActive(false);

		popupText.text = errorText;
		popup.SetActive(true);
	}

	public void LoggedIn()
	{
		UpdateAvatarAndUsername();
		UpdateHighscores();

		offlineContainer.SetActive(false);
		onlineContainer.SetActive(true);
		cancelContainer.SetActive(false);

		LogInMenu.SetActive(false);
		LogInContainer.SetActive(false);
		SignUpContainer.SetActive(false);
		ForgotPwContainer.SetActive(false);
		waitingDisplay.SetActive(false);

		if (RememberMeToggle.isOn)
		{
			logIn_UsernameText.text = loadedUsername;
			logIn_Password.text = "gtdxitck";
		}
		else
		{
			logIn_UsernameText.text = "";
			logIn_Password.text = "";
		}

	}

	public void TryToSignUp()
	{
		EverythingOff();

		waitingDisplay.SetActive(true);
		cancelContainer.SetActive(true);
		DBFuncs.CreateAccount(signUp_UsernameText.text, signUp_Password.text, signUp_Email.text);
	}

	public void AutoLogIn()
	{
		if (loadedRememberMe)
		{
			DBFuncs.LogIn(loadedUsername, loadedPwhash, true);
		}
	}

	public void TryToLogIn()
	{
		offlineContainer.SetActive(false);
		onlineContainer.SetActive(false);
		cancelContainer.SetActive(true);

		LogInMenu.SetActive(false);
		LogInContainer.SetActive(false);
		SignUpContainer.SetActive(false);
		ForgotPwContainer.SetActive(false);
		popup.SetActive(false);
		
		waitingDisplay.SetActive(true);

		if (RememberMeToggle.isOn)
		{
			Debug.Log("Hash: " + loadedPwhash);
			DBFuncs.LogIn(loadedUsername, loadedPwhash, true);
		}
		else
		{
			DBFuncs.LogIn(logIn_UsernameText.text, logIn_Password.text);
		}

	}

	public static void DontUseSavedAccount()
	{
		if (self != null)
		{
			self.pwhash = null;
			self.logIn_UsernameText.text = null;
			self.logIn_Password.text = null;
		}

	}

	public void TryAutoLogin()
	{
		/*if (PlayerPrefs.HasKey("savedUsername"))
		{
			string u = PlayerPrefs.GetString("savedUsername", "");
			string p = PlayerPrefs.GetString("savedPw", "");

			logIn_UsernameText.text = u;
			logIn_Password.text = "savedpw";
			pwhash = p;

		}*/
	}

	public void TryToResetPw()
	{
		offlineContainer.SetActive(false);
		onlineContainer.SetActive(false);
		cancelContainer.SetActive(true);

		LogInMenu.SetActive(false);
		LogInContainer.SetActive(false);
		SignUpContainer.SetActive(false);
		ForgotPwContainer.SetActive(false);
		popup.SetActive(false);
		
		waitingDisplay.SetActive(true);
	}

	public void SetAvatar_Default() { SetAvatarToX(AvatarType.Default); }
	public void SetAvatar_DancingGirl() { SetAvatarToX(AvatarType.DancingGirl); }
	public void SetAvatar_PixelMonkey() { SetAvatarToX(AvatarType.PixelMonkey); }
	public void SetAvatar_DerpDemon() { SetAvatarToX(AvatarType.DerpDemon); }
	public void SetAvatar_MiniPlayer() { SetAvatarToX(AvatarType.MiniPlayer); }
	public void SetAvatar_Offline() { SetAvatarToX(AvatarType.Offline); }

	public void SetAvatarToX(AvatarType type)
	{
		if (RemoteData.myAccount != null)
		{
			RemoteData.myAccount.avatarType = type;
			SetAvatar(type);
		}
		else
		{
			//Debug.Log("Can't set avatar, account is null");
		}
	}


	public void UpdateHighscores()
	{
		//Debug.Log("Tried to request highscores at: " + Time.time);
		if (DBFuncs.self == null) { return; }
		DBFuncs.self.RequestLeaderboard(FreshLevels.Type.TO_2);
	}


}


