using UnityEngine;
using System.Collections;

public class LadderBehavior : MonoBehaviour {

	//  The ladder will need to have capacity for multiple elevators
	//  at once.
	ArrayList elevators = new ArrayList();

	//  The ladder also needs to have a copy of an elevator to clone.
	public GameObject prefab;

	//  The ladder needs to be able to tell children who they belong to.
	public GameObject currObj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		//  The temporary elevator will spawn at the unit's feet, and then
		//  be added to the elevators array.
		GameObject tempElevator;

		//  The elevator needs to be spawned at a very specific point
		//  X-value:		center of the ladder
		//  Y-value:		bottom of the player collision/unit
		//  Z-value:		0.0f
		Vector3 spawnPoint = new Vector3 (GetComponent<Transform> ().position.x,
		                                 coll.GetComponent<Transform> ().position.y,
		                                 0.0f);


		tempElevator = 
			Instantiate (prefab,spawnPoint, Quaternion.identity) as GameObject;

		tempElevator.GetComponent<ElevatorBehavior> ().parent = currObj;

		elevators.Add (tempElevator);
	}

	//  This function will delete an elevator that is no longer in use
	void DestroyElevator(GameObject elev)
	{
		elevators.Remove (elev);
	}
}
