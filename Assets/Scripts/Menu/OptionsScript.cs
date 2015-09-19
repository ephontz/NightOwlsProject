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
		StartCoroutine (LoadScene ());
	}

	IEnumerator LoadScene()
	{
		GetComponent<AudioSource>().PlayOneShot(sound);
		yield return new WaitForSeconds (.3f);
		Application.LoadLevel("Options Menu");
	}

}
