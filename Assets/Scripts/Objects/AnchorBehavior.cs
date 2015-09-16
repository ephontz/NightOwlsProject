using UnityEngine;
using System.Collections;

public class AnchorBehavior : MonoBehaviour {

	public Vector3 location;

	// Use this for initialization
	void Start () {
		location = GetComponent<Transform> ().position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
