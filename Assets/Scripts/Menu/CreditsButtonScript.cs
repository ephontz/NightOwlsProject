using UnityEngine;
using System.Collections;

public class CreditsButtonScript : MonoBehaviour {

	public AudioClip sound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void LoadCreditsMenu()
	{
		GetComponent<AudioSource>().PlayOneShot(sound);
		Application.LoadLevel("Credits Menu");
	}
}
