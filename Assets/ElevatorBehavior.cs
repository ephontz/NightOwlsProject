using UnityEngine;
using System.Collections;

public class ElevatorBehavior : MonoBehaviour {

	//  This object needs to know it's parent
	public GameObject parent;
	//  This object needs to know it's user
	public GameObject user;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (user != null) {
			if (user.CompareTag ("Player")) {
				if (user.GetComponent<PlayerController> ().GetLadMovement () == LAD_MOVEMENT.DOWN) {
					GetComponent<Transform> ().position += new Vector3 (0.0f, -1.0f, 0.0f) * Time.deltaTime;
				} else if (user.GetComponent<PlayerController> ().GetLadMovement () == LAD_MOVEMENT.UP) {
					GetComponent<Transform> ().position += new Vector3 (0.0f, 1.0f, 0.0f) * Time.deltaTime;
				}
			}

			if (user.CompareTag ("PatrollingGuard")) {
				if (user.GetComponent<GuardBehavior> ().GetLadMovement () == LAD_MOVEMENT.DOWN) {
					GetComponent<Transform> ().position += new Vector3 (0.0f, -1.0f, 0.0f) * Time.deltaTime;
				} else if (user.GetComponent<GuardBehavior> ().GetLadMovement () == LAD_MOVEMENT.UP) {
					GetComponent<Transform> ().position += new Vector3 (0.0f, 1.0f, 0.0f) * Time.deltaTime;
				}
			}
		}

		if (parent != null) {
			if (this.GetComponent<Transform> ().position.y > parent.GetComponent<Transform> ().position.y + parent.GetComponent<Transform> ().localScale.y + 0.2f)
				Destroy (this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		user = coll.gameObject;

		user.GetComponent<PlayerController> ().LockHorizontalMovement (true);
	}
}
