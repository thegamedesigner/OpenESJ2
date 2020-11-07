using UnityEngine;
using System.Collections;

public class Defines : MonoBehaviour
{
	public Material FaderMat;
	public Material FaderMatBlack;
	public GameObject fartParticles;
	public GameObject[] coinSounds = new GameObject[10];
	public GameObject snd_TeleporterLeftToRight = null;
	public GameObject snd_TeleporterRightToLeft = null;
	public GameObject snd_MissileSnds = null;
	public GameObject snd_KeyAndDoor = null;
	public GameObject snd_MissileLongSnds = null;
	public GameObject createdObjectsPrefab = null;
	public GameObject respawnPrefab_coin = null;
	public GameObject respawnPrefab_goomba = null;
	public GameObject respawnPrefab_tinyGoomba = null;
	public GameObject stompHitbox = null;
	public GameObject zeroAngleStompHitbox = null;
	public GameObject oneUseStompHitbox = null;
	public GameObject debugTxt = null;
	public GameObject awardPopupPrefab = null;
	public Texture2D arrowCursor = null;
	public Material genericBlockAndDetailMat = null;
	public Material batchable_genericBlockAndDetailMat = null;
	public AspectUtility aspectScript = null;
	public GameObject ghostPuppet;
	public GameObject AchivoPopup_AllasKlar;
	public GameObject AchivoPopup_DaddyLove;
	public GameObject AchivoPopup_DontStomp;
	public GameObject AchivoPopup_MagicMonk;
	public GameObject AchivoPopup_Routes66;
	public GameObject AchivoPopup_Cheater;
	public GameObject AchivoPopup_Reverso;
	public GameObject AchivoPopup_Champion;
	public GameObject AchivoPopup_NoThanks;
	public GameObject AchivoPopup_MitLiebe;
	public GameObject AchivoPopup_GoingFast;
	public GameObject AchivoPopupSpawnPoint;
	public GameObject ControlsResetPopupController;
	public GameObject multiPlayerPrefab;
	public GameObject explosiveLinkPrefab;
}
