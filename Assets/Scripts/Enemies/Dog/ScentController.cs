using UnityEngine;
using System.Collections;

public class ScentController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
			if (GetComponentInParent<DogBehavior>().state == ENMY_STATES.PATROL || GetComponentInParent<DogBehavior>().state == ENMY_STATES.RESET) 
		{
			GetComponentInParent<DogBehavior>().scent = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			GetComponentInParent<DogBehavior>().scent = false;
			GetComponentInParent<DogBehavior>().delay = 3;
		}
	}



}
