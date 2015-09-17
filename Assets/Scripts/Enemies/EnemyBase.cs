using UnityEngine;
using System.Collections;

//  This enum will be used to identify which state the unit is in
public enum ENMY_STATES {PATROL, SEARCH, ATTACK, COORD, ASLEEP, AWAKE, SWARM};



public abstract class EnemyBase : MonoBehaviour {
	//  All enemies will need an anchor
	public GameObject anchor;

	public Vector3 anchorOrig;
	//  All enemies will need a roam distance
	public float leash;

	//  All enemies will need an ENMY_STATES enum
	public ENMY_STATES currState;

	// Use this for initialization
	public virtual void Start () {
		anchorOrig = anchor.GetComponent<AnchorBehavior> ().location;
	}
	
	// Update is called once per frame
	public virtual void Update () {
	
	}

	//  Enemies will overwrite the following functions based on
	//  which behaviors each unit needs to use.
	//  Parameters:		All functions take a GameObject reference to access
	//					the player's light exposure, position, and invisibility
	//					perk informaation.
	//  The PatrolBehavior() must always be overwritten by the child
	public abstract void PatrolBehavior (GameObject player);

	public virtual void SearchBehavior(GameObject player)
	{

	}

	public virtual void AttackBehavior(GameObject player)
	{

	}

	public virtual void CoordinatedBehavior(GameObject player)
	{

	}

	public virtual void AsleepBehavior(GameObject player)
	{

	}

	public virtual void AwakeBehavior(GameObject player)
	{

	}

	public virtual void SwarmBehavior(GameObject player)
	{

	}
}
