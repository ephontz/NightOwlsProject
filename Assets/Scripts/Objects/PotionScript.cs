using UnityEngine;
using System.Collections;

public class PotionScript : MonoBehaviour {

	public AudioClip sound;
	bool triggered = false;
	GameObject exitDoor;
	
	// Use this for initialization
	void Start () 
	{
		exitDoor = GameObject.FindWithTag ("Exit");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (triggered == true) 
		{
			StartCoroutine(DestroyObject());
		}
		
	}
	
	IEnumerator DestroyObject()
	{
		yield return new WaitForSeconds (0.2f);
		Destroy (gameObject);
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			GetComponent<AudioSource>().PlayOneShot(sound);
			triggered = true;
			exitDoor.GetComponent<ExitDoorScript>().Lock = false;
		}
	}
}
