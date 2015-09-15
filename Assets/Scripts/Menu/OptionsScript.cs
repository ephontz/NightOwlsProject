using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour {

	public AudioClip sound;

	
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{

	}


	//The function that's called when clicked.
	public void OptionsMenu()
	{
		GetComponent<AudioSource>().PlayOneShot(sound);
		Application.LoadLevel("Options Menu");
	}
}
