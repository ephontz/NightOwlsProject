using UnityEngine;
using System.Collections;


public class GuardBehavior : EnemyBase {

	//  The guard needs three different movement speeds, coinciding with
	//  the differents states AI
	public float wlkSpd, srchSpd, attkSpd;

	//  The range at which the guard will begin checking the player's
	//  light exposure and acting accordingly
	public float reactRange;

	//  The range at which the guard can kill the player
	public float killRange;

	//  The guard stores a refrence to the player assisting the chase habits
	public GameObject playRef;

	//  The position toward which the guard will walk in search and attack modes
	Vector3 targPos;



	// Use this for initialization
	public override void Start () {
		currState = ENMY_STATES.PATROL;

		killRange = 2.0f;

		reactRange = 5.0f;
	}


	// Update is called once per frame
	public override void Update () {
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
		if (wlkSpd < 0) {																							// If the unit is moving left
			if (Mathf.Abs (player.GetComponent<Transform> ().position.x - GetComponent<Transform> ().position.x) < reactRange 	// If the player is in range
				&& player.GetComponent<Transform> ().position.x < GetComponent<Transform> ().position.x				// If the player is left of unit
				&& Mathf.Abs (player.GetComponent<Transform> ().position.y - GetComponent<Transform> ().position.y) < 10) {			// Limit vertical deviation
				//  Step Three:
				//			Check the players light level.  Respond
				//			to the players exposure level.
				switch (player.GetComponent<PlayerController> ().lightExpo) {
				//  If the light exposure is 0 or 1
				case 0:
				case 1:
					currState = ENMY_STATES.PATROL;
					break;

				//  If the light exposure is 2 or 3
				case 2:
				case 3:
					currState = ENMY_STATES.SEARCH;
					break;

				//  If the light exposure is greater than or equal to 4
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				case 9:
				case 10:
					currState = ENMY_STATES.ATTACK;
					break;

				//  Any other light exposures, assuming there will never  be an exposure greater than 10
				default:
					if (player.GetComponent<PlayerController> ().lightExpo < 0)
						currState = ENMY_STATES.PATROL;
					else if (player.GetComponent<PlayerController> ().lightExpo > 10)
						currState = ENMY_STATES.ATTACK;
					break;
				}
			}
		} else if (wlkSpd > 0) {
			if (Mathf.Abs (player.GetComponent<Transform> ().position.x - GetComponent<Transform> ().position.x) < reactRange 	// If the player is in range
				&& player.GetComponent<Transform> ().position.x > GetComponent<Transform> ().position.x				// If the player is right of unit
				&& Mathf.Abs (player.GetComponent<Transform> ().position.y - GetComponent<Transform> ().position.y) < 10) {			// Limit vertical deviation
				//  Step Three:
				//			Check the players light level.  Respond
				//			to the players exposure level.
				switch (player.GetComponent<PlayerController> ().lightExpo) {
				//  If the light exposure is 0 or 1
				case 0:
				case 1:
					currState = ENMY_STATES.PATROL;
					break;
					
				//  If the light exposure is 2 or 3
				case 2:
				case 3:
					currState = ENMY_STATES.SEARCH;
					break;
					
				//  If the light exposure is greater than or equal to 4
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				case 9:
				case 10:
					currState = ENMY_STATES.ATTACK;
					break;
					
				//  Any other light exposures, assuming there will never  be an exposure greater than 10
				default:
					if (player.GetComponent<PlayerController> ().lightExpo < 0)
						currState = ENMY_STATES.PATROL;
					else if (player.GetComponent<PlayerController> ().lightExpo > 10)
						currState = ENMY_STATES.ATTACK;
					break;
				}
			}
		}

		//  Step Four:
		//			If the player is still in the patrol state, then check
		//			if the unit is outside of it's tether range.
		if (currState == ENMY_STATES.PATROL && wlkSpd < 0) {
			if (GetComponent<Transform> ().position.x < (anchorOrig.x - leash))
				wlkSpd = -wlkSpd;
		}
		else if (currState == ENMY_STATES.PATROL && wlkSpd > 0) {
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

	}

	//  This function will be called in update if the guard is in the ATTACK
	//  state.
	//  Behavior:		The gurad will move at attack speed toward the players
	//					position.  Unless the player's light exposure decreases,
	//					the guard will follow.  When in attack range, the guard
	//					triggers the kill action.
	public override void AttackBehavior(GameObject player)
	{

	}

	//  This function will be called in update if the guard is in the COORD
	//  state.
	public override void CoordinatedBehavior(GameObject player)
	{

	}
}
