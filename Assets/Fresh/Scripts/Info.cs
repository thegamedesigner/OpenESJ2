using UnityEngine;

public class Info : MonoBehaviour
{
	public FreshLevels.Type level;
	public FreshBoostPlatformScript movingPlatScript;
	public GameObject puppet;
	public GameObject hitbox;
	public bool triggered = false;
	[UnityEngine.Serialization.FormerlySerializedAs("particleSystem")]
	public ParticleSystem infoParticleSystem;
	public HealthScript healthScript;
	public bool stoodOnByPlayer = false;
	public bool killPlayer;//Used to tell the airswording player that impacting with this hitbox is deadly.
	public ProfileScript.AvatarType avatarType = ProfileScript.AvatarType.None;
}
