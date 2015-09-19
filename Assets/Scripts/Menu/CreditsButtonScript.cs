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
		StartCoroutine (LoadScene ());
	}
	
	IEnumerator LoadScene()
	{
		GetComponent<AudioSource>().PlayOneShot(sound);
		yield return new WaitForSeconds (.3f);
		Application.LoadLevel("Credits Menu");
	}
}
