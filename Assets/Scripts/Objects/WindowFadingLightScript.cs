using UnityEngine;
using System.Collections;

public class WindowFadingLightScript : MonoBehaviour {

	public GameObject player;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			player.GetComponent<PlayerController>().lightExpo += 2;
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			player.GetComponent<PlayerController>().lightExpo -= 2;
		}
	}
}
