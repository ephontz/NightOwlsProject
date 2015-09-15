using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	float moveSpeed;
	public bool onLadder;

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










}
