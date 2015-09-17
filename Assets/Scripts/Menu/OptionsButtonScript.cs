using UnityEngine;
using System.Collections;

public class OptionsButtonScript : MonoBehaviour {



	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void Back()
	{
		Application.LoadLevel (0);
	}

	public void Fullscreen()
	{
		Screen.fullScreen = true;
	}

	public void Windowed()
	{
		Screen.fullScreen = false;
	}


}
