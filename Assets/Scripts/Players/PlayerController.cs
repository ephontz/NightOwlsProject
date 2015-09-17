using UnityEngine;
using System.Collections;

public enum TYPE_DEATH {MELEE = 0, RANGED, SWARM}

public class PlayerController : MonoBehaviour {
	float moveSpeed;
	public bool onLadder;
	public int loot;
	public GameObject usable;
	char upgrades;
	public int lightExpo;
	public Transform mainSpawn;

	//Jump variables
	float jumpForce = 600f;
	float jumpNumber = 0;

	// Use this for initialization
	void Start () 
	{
		moveSpeed = 5.0f;
		onLadder = false;
		usable = null;
		transform.position = mainSpawn.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Movement ();
		if (Input.GetKey ("e"))
			Use ();
		else if (Input.GetKeyDown ("q"))
			GetComponent<Invisiblilityscript> ().Invisibility ();

		if (jumpNumber == 0 && Input.GetKeyDown(KeyCode.Space)) 
		{
			jumpNumber = 1;
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
		}


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
	public void PlayerDeath(TYPE_DEATH method)
	{
		this.GetComponent<Invisiblilityscript> ().SetExposure (0);

		//GetComponent<Transform> ().position = new Vector3 (20.0f, 20.0f, 0.0f);
	}

	void Use()
	{
		if (usable == null)
			return;

		if (usable.tag == "chest") 
		{
			if(usable.GetComponent<containerscript>().inUse() <= 0)
				loot += usable.GetComponent<containerscript>().Payout();
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag != "Enemy" && col.gameObject.tag != "world")
			usable = col.gameObject;

		if (col.gameObject.tag == "Floor" || col.gameObject.tag == "chest") 
		{
			jumpNumber = 0;
		}
	}

	void OnCollisionExit2D(Collision2D col)
	{
		usable = null;
	}

	 


}
