using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SaveLoadScript : MonoBehaviour {

	public AudioClip sound;
	
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void SaveLoad()
	{
		GetComponent<AudioSource>().PlayOneShot(sound);
		Application.LoadLevel("Save-Load Menu");
	}
}
