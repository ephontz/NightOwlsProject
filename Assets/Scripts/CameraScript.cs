using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = new Vector3 (player.position.x, player.position.y, player.position.z - 1);
	}
}
