using Holoville.HOTween;
using UnityEngine;

public class NovaMovingBlock : MonoBehaviour
{
	public bool dontLoop = false;
	public bool useInOutSineTween = false;
	public GameObject node;
	public float time;
	public Collider hitbox;
	public bool amSticky = false;

	[HideInInspector]
	public Vector2 vel = new Vector2(0, 0);

	Vector2 startPos;
	[SerializeField]
	Vector2 nodePos = Vector2.negativeInfinity;
	Vector2 oldPos;
	float stickyOffset = 0;
	float distBetweenPoints = 0;
	bool calledStartMoving = false;
	Tweener tweenerScale = null;
	TweenParms tweenParms;
	[HideInInspector]
	public float percentage = 0;

	void Start()
	{
		startPos = new Vector2(transform.position.x, transform.position.y);
		if (node)
		{
			nodePos = new Vector2(node.transform.position.x, node.transform.position.y);
		}
		if (node)
		{
			node.SetActive(false);
		}
	}
	
	void Update()
	{
		if (calledStartMoving || fa.paused) { return; }

		calledStartMoving = true;
		StartMoving();
	}

	float distPerFrame = 0;
	float stopDist = 0.2f;
	bool headingToNode = true;

	void StartMoving()
	{
		//EaseType easeType = EaseType.Linear;
		distBetweenPoints = Vector2.Distance(startPos, nodePos);

		distPerFrame = distBetweenPoints / (time * 50);

		if (dontLoop)
		{
		//	tweenParms = new TweenParms().Prop("percentage", 0).Ease(easeType).UpdateType(UpdateType.FixedUpdate);
		}
		else
		{
		//	tweenParms = new TweenParms().Prop("percentage", 0).Ease(easeType).Loops(-1, LoopType.Yoyo).UpdateType(UpdateType.FixedUpdate);
		}
		//HOTween.To(this, time, tweenParms);
	}

	void FixedUpdate()
	{
		startPos.y = transform.position.y;
		startPos.y = nodePos.y;
		if (fa.paused) { return; }
		if (calledStartMoving)
		{
			oldPos.x = transform.position.x;
			oldPos.y = transform.position.y;
			if (headingToNode)
			{
				if (Vector2.Distance(startPos, transform.position) > distBetweenPoints)
				{
					headingToNode = false;
				}
			}
			else
			{
				if (Vector2.Distance(nodePos, transform.position) > distBetweenPoints)
				{
					headingToNode = true;
				}
			}

			if (headingToNode)
			{
				if (nodePos.x > startPos.x)
				{
					transform.AddX(distPerFrame);
				}
				else
				{
					transform.AddX(-distPerFrame);
				}
			}
			else
			{
				if (nodePos.x > startPos.x)
				{
					transform.AddX(-distPerFrame);
				}
				else
				{
					transform.AddX(distPerFrame);
				}
			}





			//Debug.DrawLine(startPos, transform.position, Color.green);
			//Debug.DrawLine(nodePos, transform.position, Color.red);

			vel.x = transform.position.x - oldPos.x;
			vel.y = transform.position.y - oldPos.y;//Add vel to the player

			CheckForAndMovePlayer();
		}
	}

	void SetPos(float percentage)
	{
		float angle = Setup.ReturnAngleTowardsVec(startPos, nodePos);
		Vector3 result = Setup.projectVec(startPos, new Vector3(0, 0, angle), distBetweenPoints * percentage, -Vector3.left);
		transform.SetX(result.x);
		transform.SetY(result.y);
	}

	void CheckForAndMovePlayer()
	{
		//Is the player inside my bounds?
		if (xa.player == null) { return; }
		if (xa.playerScript == null) { return; }


		if (!amSticky)
		{
			if (hitbox.GetComponent<Collider>().bounds.max.x + stickyOffset > (xa.player.transform.position.x - (xa.playerBoxWidth * 0.5f)) &&
				hitbox.GetComponent<Collider>().bounds.min.x - stickyOffset < (xa.player.transform.position.x + (xa.playerBoxWidth * 0.5f)) &&
				(hitbox.GetComponent<Collider>().bounds.max.y + 0.2f) > (xa.player.transform.position.y - (xa.playerBoxHeight * 0.5f)) &&
				hitbox.GetComponent<Collider>().bounds.min.y < (xa.player.transform.position.y + (xa.playerBoxHeight * 0.5f)))
			{
				if (!xa.playerMoving)
				{

					xa.playerScript.vel.x += vel.x;

					//also, lets move the player away from the edge, a little bit
					/*
					if (hitbox.GetComponent<Collider>().bounds.min.x > xa.player.transform.position.x)
					{
						Debug.Log("hereXXX");
						if (xa.player.transform.position.x < hitbox.GetComponent<Collider>().bounds.center.x)
						{
							xa.playerScript.vel.x += 3;
						}
						else
						{
							xa.playerScript.vel.x -= 3;
						}
					}
					*/
					// (hitbox.GetComponent<Collider>().bounds.max.x < (xa.player.transform.position.x + (xa.playerBoxWidth * 0.5f)) &&
					//	hitbox.GetComponent<Collider>().bounds.max.x - 0.1f > (xa.player.transform.position.x + (xa.playerBoxWidth * 0.5f))
					//)
					//{
					//
					//	xa.playerScript.vel.x -= vel.x * 0.1f;
					//}
				}
				xa.playerScript.vel.y += vel.y;
			}
		}
		else
		{
			if (hitbox.GetComponent<Collider>().bounds.max.x + stickyOffset > (xa.player.transform.position.x - (xa.playerBoxWidth * 0.5f)) &&
				hitbox.GetComponent<Collider>().bounds.min.x - stickyOffset < (xa.player.transform.position.x + (xa.playerBoxWidth * 0.5f)) &&
				(hitbox.GetComponent<Collider>().bounds.max.y + 0.2f) > (xa.player.transform.position.y - (xa.playerBoxHeight * 0.5f)) &&
				hitbox.GetComponent<Collider>().bounds.min.y < (xa.player.transform.position.y + (xa.playerBoxHeight * 0.5f))
				&&

				(hitbox.GetComponent<Collider>().bounds.max.y - 0.1f) < (xa.player.transform.position.y - (xa.playerBoxHeight * 0.5f)))
			{
				if (!xa.playerMoving)
				{
					xa.playerScript.vel.x += vel.x;
				}
				xa.playerScript.vel.y += vel.y;
			}
		}


		// Debug.DrawLine(new Vector3(xa.player.transform.position.x + (xa.playerBoxWidth * 0.5f), xa.player.transform.position.y + (xa.playerBoxHeight * 0.5f), xa.GribblyFrontLayer0), new Vector3(xa.player.transform.position.x - (xa.playerBoxWidth * 0.5f), xa.player.transform.position.y + (xa.playerBoxHeight * 0.5f), xa.GribblyFrontLayer0), Color.cyan);
		// Debug.DrawLine(new Vector3(xa.player.transform.position.x + (xa.playerBoxWidth * 0.5f), xa.player.transform.position.y - (xa.playerBoxHeight * 0.5f), xa.GribblyFrontLayer0), new Vector3(xa.player.transform.position.x - (xa.playerBoxWidth * 0.5f), xa.player.transform.position.y - (xa.playerBoxHeight * 0.5f), xa.GribblyFrontLayer0), Color.cyan);

		// Debug.DrawLine(new Vector3(xa.player.transform.position.x + (xa.playerBoxWidth * 0.5f), xa.player.transform.position.y + (xa.playerBoxHeight * 0.5f), xa.GribblyFrontLayer0), new Vector3(xa.player.transform.position.x + (xa.playerBoxWidth * 0.5f), xa.player.transform.position.y - (xa.playerBoxHeight * 0.5f), xa.GribblyFrontLayer0), Color.cyan);
		//Debug.DrawLine(new Vector3(xa.player.transform.position.x - (xa.playerBoxWidth * 0.5f), xa.player.transform.position.y + (xa.playerBoxHeight * 0.5f), xa.GribblyFrontLayer0), new Vector3(xa.player.transform.position.x - (xa.playerBoxWidth * 0.5f), xa.player.transform.position.y - (xa.playerBoxHeight * 0.5f), xa.GribblyFrontLayer0), Color.cyan);

	}
}

/*
 * 

        //is the player inside me on both the x & the y?
        //if (collider.bounds.max.x > (xa.player.transform.position.x - (xa.playerBoxWidth * 0.5f)) &&
          //  collider.bounds.min.x < (xa.player.transform.position.x + (xa.playerBoxWidth * 0.5f)) &&
           // collider.bounds.max.y > (xa.player.transform.position.y - (xa.playerBoxHeight * 0.5f)) &&
           // collider.bounds.min.y < (xa.player.transform.position.y + (xa.playerBoxHeight * 0.5f)))
        //{
          //  xa.player.transform.SetX(xa.player.transform.position.x + vel.x);
            //xa.player.transform.SetY(xa.player.transform.position.y + vel.y);
        //}
        //else
      //  {
            //The player is not inside me, but...
            //If the player did one more round of gravity, would they be standing on me?
            if (collider.bounds.max.x > (xa.player.transform.position.x - (xa.playerBoxWidth * 0.5f)) &&
                collider.bounds.min.x < (xa.player.transform.position.x + (xa.playerBoxWidth * 0.5f)) &&
                (collider.bounds.max.y + 0.2f) > (xa.player.transform.position.y - (xa.playerBoxHeight * 0.5f)) &&
                collider.bounds.min.y < (xa.player.transform.position.y + (xa.playerBoxHeight * 0.5f)))
            {
                xa.player.transform.SetX(xa.player.transform.position.x + vel.x);
                // xa.player.transform.SetY(xa.player.transform.position.y + vel.y);

                //The vibrartion seems to be about findGround(), not anything to do with snapping/adding vel
            }
        //}


*/
