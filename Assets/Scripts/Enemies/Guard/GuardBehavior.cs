using UnityEngine;
using System.Collections;



public class GuardBehavior : EnemyBase {
	//  Constants
	const bool LEFT_H = false;

	const bool RIGHT_H = true;



	//  The guard needs three different movement speeds, coinciding with
	//  the differents states AI
	public float wlkSpd, srchSpd, attkSpd;

	//  The range at which the guard will begin checking the player's
	//  light exposure and acting accordingly
	public float reactRange;

	//  The range at which the guard can kill the player
	float killRange;

	//  The guard stores a refrence to the player assisting the chase habits
	public GameObject playRef;
	//public GameObject navigatorPrefab;
	//bool pathfinding = false;

	//  The position toward which the guard will walk in search and attack modes
	Vector3 targPos;

	//  The direction the unit is going to move when on a ladder
	LAD_MOVEMENT ladMove = LAD_MOVEMENT.STAY;

	////  The paths used when coordinated
	//ArrayList outgoing;
	//ArrayList returning;

	//  The two sprites that display over the units head
	public Sprite qMark, xMark;
	public AudioClip qSound, xSound, fSound;



	// Use this for initialization
	public override void Start () {
		base.Start ();

		ChangeENMYState(ENMY_STATES.PATROL);

		killRange = 1.0f;

		reactRange = 5.0f;

		//targPos = playRef.GetComponent<Transform> ().position;
	}


	// Update is called once per frame
	public override void Update () {
		//if (Mathf.Abs (targPos.y - GetComponent<Transform> ().position.y) > 5.0f && !pathfinding) {
		//	GameObject temp;
		//	temp = Instantiate (navigatorPrefab, GetComponent<Transform>().position, Quaternion.identity) as GameObject;
		//	temp.GetComponent<LadderNavigatorBehavior>().user = this.gameObject;
		//	temp.GetComponent<LadderNavigatorBehavior>().target = targPos;
		//	pathfinding = true;
		//}

		switch (currState)
		{
		case ENMY_STATES.PATROL:
			PatrolBehavior (playRef);
			break;

		case ENMY_STATES.SEARCH:
			SearchBehavior(playRef);
			break;

		case ENMY_STATES.ATTACK:
			AttackBehavior(playRef);
			break;

		case ENMY_STATES.COORD:
			CoordinatedBehavior(playRef);
			break;

		default:
			PatrolBehavior(playRef);
			break;
		}
	}

	//  This function will be called in update if the guard is in the PATROL
	//  state.
	//  Behavior:		The guard will be patrolling at walk speed.  If the
	//					player is in reaction range, the guard will read the
	//					light exposure and react accordingly.
	public override void PatrolBehavior(GameObject player)
	{
		//  Step One:
		//			The unit moves
		GetComponent<Transform> ().position += new Vector3 (wlkSpd, 0, 0) * Time.deltaTime;

		//  Step Two:
		//  		Check for player position relative to the player.
		//  		The player must be in-front of the unit, and within 
		//			the rectRange.

		//  Check the left hand side of the player
		if (!CheckSign(wlkSpd)) {
			if (IsPlayerInRange(player, LEFT_H)) {
				//  Step Three:
				//			Check the players light level.  Respond
				//			to the players exposure level.
				CheckPlayerExposure(player, currState);
			}
		} 

		//  Check the right hand side of the player
		else if (CheckSign(wlkSpd)) {
			if (IsPlayerInRange(player, RIGHT_H)) {
				//  Step Three:
				//			Check the players light level.  Respond
				//			to the players exposure level.
				CheckPlayerExposure (player, currState);
			}
		}

		//  Step Four:
		//			If the unit is still in the patrol state, then check
		//			if the unit is outside of it's tether range.
		if (currState == ENMY_STATES.PATROL && !CheckSign(wlkSpd)) 
		{
			if (GetComponent<Transform> ().position.x < (anchorOrig.x - leash))
				wlkSpd = -wlkSpd;
		}
		else if (currState == ENMY_STATES.PATROL && CheckSign(wlkSpd)) {
			if (GetComponent<Transform> ().position.x > anchorOrig.x + leash)
				wlkSpd = -wlkSpd;
		}
	}

	//  This function will be called in update if the guard is in the SEARCH
	//  state.
	//  Behavior:		The guard will move at search speed toward the player's
	//					position when last seen.  If the player is seen again,
	//					then the player's position will be updated again.  If
	//					in hight exposure, the unit enters attack mode.
	public override void SearchBehavior(GameObject player)
	{
		//  Step One:
		//			The unit moves toward the last known position.
		if (targPos.x < GetComponent<Transform> ().position.x && CheckSign (srchSpd))
			srchSpd = -srchSpd;
		else if (targPos.x > GetComponent<Transform> ().position.x && !CheckSign (srchSpd))
			srchSpd = -srchSpd;

		GetComponent<Transform> ().position += new Vector3(srchSpd, 0, 0) * Time.deltaTime;

		//  Step Two:
		//			Check for player position relative to the player.
		//  		The player must be in-front of the unit, and within 
		//			the rectRange.

		//  Check the left hand side
		if (!CheckSign (srchSpd)) 
		{
			if (IsPlayerInRange(player, LEFT_H))
			{
				//  Step Three:
				//			Check the players light level.  Respond
				//			to the players exposure level.
				CheckPlayerExposure(player, currState);
			}
		}

		//  Check the right hand side
		if (CheckSign (srchSpd)) 
		{
			if (IsPlayerInRange(player, RIGHT_H))
			{
				//  Step Three:
				//			Check the players light level.  Respond
				//			to the players exposure level.
				CheckPlayerExposure(player, currState);
			}
		}

		//  Step Four:
		//			If the unit is still in the search state, check
		//			if the unit has reached the location the player
		//			was last seen at.  If not, continue searching.
		//			If yes, return to normal patrol.
		if (currState == ENMY_STATES.SEARCH) 
		{
			if (Mathf.Abs(targPos.x - GetComponent<Transform>().position.x) <= .25f)
			{
				ChangeENMYState(ENMY_STATES.PATROL);

				if (1 == Random.Range(1, 10))
					GetComponentInChildren<AlertSpritesBehavior>().PlayClip(fSound);

				targPos = anchorOrig;
			}
		}
	}

	//  This function will be called in update if the guard is in the ATTACK
	//  state.
	//  Behavior:		The gurad will move at attack speed toward the players
	//					position.  Unless the player's light exposure decreases,
	//					the guard will follow.  When in attack range, the guard
	//					triggers the kill action.
	public override void AttackBehavior(GameObject player)
	{
		//  Step One:
		//			The unit moves toward the last known position.
		if (targPos.x < GetComponent<Transform> ().position.x && CheckSign (srchSpd))
			srchSpd = -srchSpd;
		else if (targPos.x > GetComponent<Transform> ().position.x && !CheckSign (srchSpd))
			srchSpd = -srchSpd;

		GetComponent<Transform> ().position += new Vector3 (attkSpd, 0, 0) * Time.deltaTime;

		//  Step Two:
		//			Check for player position relative to the player.
		//  		The player must be in-front of the unit, and within 
		//			the rectRange.

		//  Check the left hand side
		if (!CheckSign (attkSpd)) 
		{
			if (IsPlayerInRange(player, LEFT_H))
			{
				//  Step Three:
				//			Check the players light level.  Respond
				//			to the players exposure level.
				CheckPlayerExposure(player, currState);
			}
		}

		//  Check the right hand side
		if (CheckSign (attkSpd)) 
		{
			if (IsPlayerInRange(player, RIGHT_H))
			{
				//  Step Three:
				//			Check the players light level.  Respond
				//			to the players exposure level.
				CheckPlayerExposure(player, currState);
			}
		}

		//  Step Four:
		//			If the unit is still in the attack state, check
		//			if the unit has reached the location the player
		//			was last seen at.  If not, continue attacking.
		//			If yes, return to searching.
		if (currState == ENMY_STATES.ATTACK)
		{
			//  UNIQUE:
			//			If the player is within the unit's kill range,
			//			invoke the layers death function passing the correct
			//			enemy type.
			if (Mathf.Abs(player.GetComponent<Transform>().position.x - GetComponent<Transform>().position.x) < killRange
			    && Mathf.Abs (player.GetComponent<Transform>().position.y - GetComponent<Transform>().position.y) < .5f)
			{
				player.GetComponent<PlayerController>().PlayerDeath(TYPE_DEATH.MELEE);

				ChangeENMYState(ENMY_STATES.PATROL);
			}

			else if (Mathf.Abs(targPos.x - GetComponent<Transform>().position.x) <= .25f)
			{
				ChangeENMYState(ENMY_STATES.PATROL);
				
				targPos = anchorOrig;
			}
		}
	}

	//  This function will be called in update if the guard is in the COORD
	//  state.
	public override void CoordinatedBehavior(GameObject player)
	{

	}

	//  This function handles advanced pathing.  


	//  This function is refactored code with the purpose of changing the
	//  unit's state.  This is made with plans to also attach the particle system
	//  and sound alerts to this function.
	//  Parameters:			The ENMY_STATE is what the currState is reassigned to.
	//  Returns:		void
	void ChangeENMYState(ENMY_STATES change)
	{

		if (change == ENMY_STATES.SEARCH && currState != ENMY_STATES.ATTACK) {
			GetComponentInChildren<AlertSpritesBehavior> ().ChangeSprite (qMark);
			GetComponentInChildren<AlertSpritesBehavior> ().PlayClip (qSound);
		} else if (change == ENMY_STATES.SEARCH && currState == ENMY_STATES.ATTACK) {
			GetComponentInChildren<AlertSpritesBehavior> ().ChangeSprite (qMark);
			GetComponentInChildren<AlertSpritesBehavior> ().PlayClip (fSound);		
		}
		else if (change == ENMY_STATES.ATTACK) {
			GetComponentInChildren<AlertSpritesBehavior> ().ChangeSprite(xMark);
			GetComponentInChildren<AlertSpritesBehavior> ().PlayClip(xSound);

		} else
			GetComponentInChildren<AlertSpritesBehavior> ().ChangeSprite(null);

		
		currState = change;
	}

	//  Verticle navigation function will help set the path for the unit to take to the 

	//  This function changes the unit's LAD_MOVEMENT
	//bool SetLadMovement(LAD_MOVEMENT dir)
	//{
	//	return false;
	//}
	//
	//  This function returns the unit's LAD_MOVEMENT
	public LAD_MOVEMENT GetLadMovement()
	{
		return ladMove;
	}

	//  This function will check the sign of the value
	//  Returns:		True == RHSide	False == LHSide
	bool CheckSign(float f)
	{
		if (f >= 0)
			return true;
		else
			return false;
	}

	//  This function will check if the player is in range and in front of the
	//  player.
	//  Parameters:			The boolean will be used to determine whether to check
	//						the right hand side or left hand side of the unit.
	//
	//			True = right			False = left
	//
	//  Returns:			True if player is in range.
	bool IsPlayerInRange(GameObject player, bool direction)
	{

		// Left
		if (!direction) 
		{
			if (Mathf.Abs (player.GetComponent<Transform> ().position.x - GetComponent<Transform> ().position.x) < reactRange 	// If the player is in range
			    && player.GetComponent<Transform> ().position.x < GetComponent<Transform> ().position.x				// If the player is right of unit
			    && Mathf.Abs (player.GetComponent<Transform> ().position.y - GetComponent<Transform> ().position.y) < 1.5) 			// Limit vertical deviation
			{			
					return true;
			}

				else return false;
		}
		
		//  Right
		if (direction) 
		{
			if (Mathf.Abs (player.GetComponent<Transform> ().position.x - GetComponent<Transform> ().position.x) < reactRange 	// If the player is in range
				&& player.GetComponent<Transform> ().position.x > GetComponent<Transform> ().position.x				// If the player is right of unit
				&& Mathf.Abs (player.GetComponent<Transform> ().position.y - GetComponent<Transform> ().position.y) < 1.5) 			// Limit vertical deviation
			{			
				return true;
			}

			else return false;
		}

		//  OMGWTFBBQ
		else return false;
	}

	//  This function will check the player's light exposure.  The function
	//
	//  Parameters:			The GameObject is a reference to the player and
	//						is used to extract information about the light
	//						exposure.  The ENMY_STATES enum is used to detemine
	//						which switch statement is used when called.
	//
	//  Returns;			void
	void CheckPlayerExposure(GameObject player, ENMY_STATES cState)
	{
		if (cState == ENMY_STATES.PATROL) 
		{
			switch (player.GetComponent<Invisiblilityscript> ().LightExposure ()) 
			{
			//  If the light exposure is 0 or 1
			case 0:
			case 1:
			{
				//ChangeENMYState(ENMY_STATES.PATROL);
				break;
			}

			//  If the light exposure is 2 or 3
			case 2:
			case 3:
			{
				ChangeENMYState(ENMY_STATES.SEARCH);
				targPos = player.GetComponent<Transform> ().position;
				break;
			}

			//  If the light exposure is greater than or equal to 4
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
			case 10:
			{
				ChangeENMYState(ENMY_STATES.ATTACK);
				targPos = player.GetComponent<Transform> ().position;
				break;
			}

			//  Any other light exposures, hopefully there will never  be an exposure greater than 10
			default:
			{
				if (player.GetComponent<Invisiblilityscript> ().LightExposure () < 0)
					ChangeENMYState(ENMY_STATES.PATROL);
				else if (player.GetComponent<Invisiblilityscript> ().LightExposure () > 10) {
					ChangeENMYState(ENMY_STATES.ATTACK);
					targPos = player.GetComponent<Transform> ().position;
				}
				break;
			}
			}

		} 

		else if (cState == ENMY_STATES.SEARCH) {
			switch (player.GetComponent<Invisiblilityscript> ().LightExposure ()) {
			//  If the light exposure is 0 or 1
			case 0:
			case 1:
			{
				//ChangeENMYState(ENMY_STATES.SEARCH);
				break;
			}

			//  If the light exposure is 2 or 3
			case 2:
			case 3:
			{
				//ChangeENMYState(ENMY_STATES.SEARCH);
				targPos = player.GetComponent<Transform> ().position;
				break;
			}

			//  If the light exposure is greater than or equal to 4
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
			case 10:
			{
				ChangeENMYState(ENMY_STATES.ATTACK);
				targPos = player.GetComponent<Transform> ().position;
				break;
			}

			//  Any other light exposures, hopefully there will never  be an exposure greater than 10
			default:
			{
				if (player.GetComponent<Invisiblilityscript> ().LightExposure () < 0)
					ChangeENMYState(ENMY_STATES.SEARCH);
				else if (player.GetComponent<Invisiblilityscript> ().LightExposure () > 10) {
					ChangeENMYState(ENMY_STATES.ATTACK);
					targPos = player.GetComponent<Transform> ().position;
				}
				break;
			}
			}
		} 

		else if (cState == ENMY_STATES.ATTACK) 
		{
			switch (player.GetComponent<Invisiblilityscript> ().LightExposure ()) {
			//  If the light exposure is 0 or 1
			case 0:
			case 1:
			{
				ChangeENMYState(ENMY_STATES.SEARCH);
				break;
			}

			//  If the light exposure is 2 or 3
			case 2:
			case 3:
			{
				ChangeENMYState(ENMY_STATES.SEARCH);
				targPos = player.GetComponent<Transform> ().position;
				break;
			}

			//  If the light exposure is greater than or equal to 4
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
			case 10:
			{
				//ChangeENMYState(ENMY_STATES.ATTACK);
				targPos = player.GetComponent<Transform> ().position;
				break;
			}

			//  Any other light exposures, hopefully there will never  be an exposure greater than 10
			default:
			{
				if (player.GetComponent<Invisiblilityscript> ().LightExposure () < 0)
					ChangeENMYState(ENMY_STATES.SEARCH);
				else if (player.GetComponent<Invisiblilityscript> ().LightExposure () > 10) {
					ChangeENMYState(ENMY_STATES.ATTACK);
					targPos = player.GetComponent<Transform> ().position;
				}
				break;
			}
			}
		}
	}
}
