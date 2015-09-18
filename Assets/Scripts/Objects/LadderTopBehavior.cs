using UnityEngine;
using System.Collections;

public class LadderTopBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.CompareTag ("Elevator")) {
			Destroy (coll.gameObject);
		}
	}
}
