using UnityEngine;
using System.Collections;

public class AlertSpritesBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeSprite(Sprite sprite)
	{
		GetComponent<SpriteRenderer> ().sprite = sprite;
	}

	public void PlayClip(AudioClip clip)
	{
		GetComponent<AudioSource> ().PlayOneShot (clip);
	}
}
