using UnityEngine;
using System.Collections;

public enum TYPE_DEATH {MELEE = 0, RANGED, SWARM}

public class PlayerController : MonoBehaviour {
	float moveSpeed;
	public bool onLadder;
	public int lightExpo;

	// Use this for initialization
	void Start () 
	{
		moveSpeed = 3.0f;
		onLadder = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Movement ();


	}

	void Movement()
	{
		//Movement
		Vector3 temp = transform.position;
		if (Input.GetKey ("a"))
			temp.x -= moveSpeed * Time.deltaTime;
		else if (Input.GetKey ("d"))
			temp.x += moveSpeed * Time.deltaTime;
		else if (Input.GetKey ("w") && onLadder)
			temp.y += moveSpeed * Time.deltaTime;
		else if (Input.GetKey ("s") && onLadder)
			temp.y -= moveSpeed * Time.deltaTime;
		transform.position = temp;
	}

	//  This one function will handle the multiple types of death possible to the player
	//  Parameters:		The function takes in a TYPE_DEATH and uses that to determine
	//					which actions to take.
	void PlayerDeath(TYPE_DEATH method)
	{

	}







}
