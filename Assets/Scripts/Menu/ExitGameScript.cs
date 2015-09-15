using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExitGameScript : MonoBehaviour {

	public AudioClip sound;
	
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void ExitGame()
	{
		GetComponent<AudioSource>().PlayOneShot(sound);
		Application.Quit();
	}
}
