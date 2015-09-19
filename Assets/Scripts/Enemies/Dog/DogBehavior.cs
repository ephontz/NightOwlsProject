

using UnityEngine;
using System.Collections;


public class DogBehavior : MonoBehaviour {

	bool face;
	// facing left = false, right = true
	float walkSpeed;
	float srchSpeed;
	float atkSpeed;
	public ENMY_STATES state;
	public GameObject Player;
	public float delay;
	public float leash;
	public Vector3 anchor;
	public bool scent;
	public Vector3 target;
	public float detRange;
	public float searchTime;
	public float atkTime;
	// Use this for initialization
	void Start () 
	{
		anchor = transform.position;
		state = ENMY_STATES.PATROL;
		walkSpeed = 3;
		srchSpeed = 2;
		atkSpeed = 5;
		face = true;
		delay = 30.0f;
		leash = 3;
		scent = false;
		detRange = 2.0f;
		searchTime = 5.0f;
		atkTime = 3.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!scent)
			Detect ();
		else 
			ScentDetect ();

		switch (state) 
		{
		case ENMY_STATES.PATROL:
			PatrolBehavior();
			break;
		case ENMY_STATES.SEARCH:
			SearchBehavior();
			break;
		case ENMY_STATES.ATTACK:
			AttackBehavior();
			break;
		case ENMY_STATES.RESET:
			Reset();
			break;
		}
	}

	void PatrolBehavior ()
	{
		//check to see if at end of leash
		if (transform.position.x - leash > anchor.x && face)
			face = false;
		else if (transform.position.x + leash < anchor.x && !face)
			face = true;

		//movement
		Vector3 temp = transform.position;

		if (face)
			temp.x += walkSpeed * Time.deltaTime;
		else
			temp.x -= walkSpeed * Time.deltaTime;

		transform.position = temp;		
	}

	void SearchBehavior ()
	{
		Vector3 temp = transform.position;

		if (transform.position.x < target.x && !face)
			face = true;
		else if (transform.position.x > target.x && face)
			face = false;

		if (face)
			temp.x += srchSpeed * Time.deltaTime;
		else 
			temp.x -= srchSpeed * Time.deltaTime;

		transform.position = temp;
	}

	void AttackBehavior ()
	{
		Vector3 temp = transform.position;
		
		if (transform.position.x < Player.transform.position.x && !face)
			face = true;
		else if (transform.position.x > Player.transform.position.x && face)
			face = false;
		
		if (face)
			temp.x += atkSpeed * Time.deltaTime;
		else 
			temp.x -= atkSpeed * Time.deltaTime;
		
		transform.position = temp;
	}

	void Reset()
	{
		if (face && transform.position.x > anchor.x)
			face = false;
		else if (!face && transform.position.x < anchor.x)
			face = true;

		Vector3 temp = transform.position;

		if (face)
			temp.x += walkSpeed * Time.deltaTime;
		else 
			temp.x -= walkSpeed * Time.deltaTime;

		transform.position = temp;

		if (transform.position.x + .1 > anchor.x || transform.position.x - .1 < anchor.x)
			state = ENMY_STATES.PATROL;
	}





	void Detect()
	{
		float tempRange = detRange * Player.GetComponent<Invisiblilityscript> ().LightExposure ();
		RaycastHit2D targ;

		if (state == ENMY_STATES.PATROL) 
		{
			if (!face)
				targ = Physics2D.Raycast ((Vector2)transform.position, Vector2.left, tempRange, Physics2D.DefaultRaycastLayers, -1.0f);
			else
				targ = Physics2D.Raycast ((Vector2)transform.position, Vector2.right, tempRange, Physics2D.DefaultRaycastLayers, -1.0f);

			if (targ.collider != null && targ.collider.gameObject.tag == "Player")
			{
				target = targ.transform.position;
				state = ENMY_STATES.SEARCH;
			}
			else return;
		} 
		else if (state == ENMY_STATES.SEARCH) 
		{
			if (!face)
				targ = Physics2D.Raycast ((Vector2)transform.position, Vector2.left, detRange, Physics2D.DefaultRaycastLayers, -1.0f);
			else 
				targ = Physics2D.Raycast ((Vector2)transform.position, Vector2.right, detRange, Physics2D.DefaultRaycastLayers, -1.0f);

			if (targ.collider != null && targ.collider.gameObject.tag == "Player")
			{
				state = ENMY_STATES.ATTACK;
				return;
			}
			else
			{
				if (!face)
					targ = Physics2D.Raycast ((Vector2)transform.position, Vector2.left, tempRange, Physics2D.DefaultRaycastLayers, -1.0f);
				else
					targ = Physics2D.Raycast ((Vector2)transform.position, Vector2.right, tempRange, Physics2D.DefaultRaycastLayers, -1.0f);
			}

			if(targ.collider != null && targ.collider.gameObject.tag == "Player")
			{
				target = targ.transform.position;
			}
			else 
			{
				searchTime -= Time.deltaTime;
				if (searchTime <= 0) 
				{
					searchTime = 5.0f;
					state = ENMY_STATES.RESET;
				}
				else return;
			}
		} 
		else if (state == ENMY_STATES.ATTACK) 
		{
			if (!face)
				targ = Physics2D.Raycast ((Vector2)transform.position, Vector2.left, detRange, Physics2D.DefaultRaycastLayers, -1.0f);
			else 
				targ = Physics2D.Raycast ((Vector2)transform.position, Vector2.right, detRange, Physics2D.DefaultRaycastLayers, -1.0f);

			if(targ.collider == null || targ.collider.gameObject.tag != "Player")
			{
				atkTime -= Time.deltaTime;

				if(atkTime <= 0)
				{
					atkTime = 3.0f;
					state = ENMY_STATES.SEARCH;
				}
			}
			else return;
		}


	}

	void ScentDetect()
	{
		if (state == ENMY_STATES.PATROL || state == ENMY_STATES.RESET) {
			state = ENMY_STATES.SEARCH;
			target = Player.transform.position;

		} 
		else if (state == ENMY_STATES.SEARCH) 
		{
			delay -= Time.deltaTime;
			if (delay <= 0)
				state = ENMY_STATES.ATTACK;
		}
	}




}
