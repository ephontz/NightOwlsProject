using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewGameScript : MonoBehaviour {


	public AudioClip sound;


	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void LoadNewGame()
	{
		StartCoroutine (LoadScene ());
	}

	IEnumerator LoadScene()
	{
		GetComponent<AudioSource>().PlayOneShot(sound);
		yield return new WaitForSeconds (.3f);
		Application.LoadLevel("tutorial");
	}
}
