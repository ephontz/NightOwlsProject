using UnityEngine;
using System.Collections;

public class LadderNavigatorBehavior : MonoBehaviour {
	//  This is where the navigator needs to make it.
	public Vector3 target;

	//  Move Direction will decide whether the navigator moves in an x or y
	//  direction.
	Vector3 moveDirect;

	//  This ArrayList of vector3s will send the positions to the guard
	//  that it needs to follow.
	ArrayList path;

	//  The unit that wants the path
	public GameObject user;

	// Use this for initialization
	void Start () {
	//	path = new ArrayList ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	//  This object needs to know when it enters and exits a ladder
	void OnTriggerEnter2D(Collider2D collider)
	{

	}

	void OnTriggerExit2D(Collider2D collider)
	{

	}

	//  It needs to know whether to change x and y directions in a positive or negative direction
	void SetYDirection()
	{

	}

	void SetXDirection()
	{

	}


}
