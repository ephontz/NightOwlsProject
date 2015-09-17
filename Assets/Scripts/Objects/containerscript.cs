using UnityEngine;
using System.Collections;

public class containerscript : MonoBehaviour {


	float normDelay = 5.0f;
	float delay = 5.0f;
	int value = 10;
	GameObject loot = null;
	public Sprite opened;
	public AudioClip pick;
	public AudioClip open;
	public AudioClip hit;
	bool is_playing;
	// Use this for initialization
	public void SetUp (float _delay, int _value, GameObject _loot) 
	{
		delay = normDelay = _delay;
		value = _value;
		loot = _loot;
	}
	
	// Update is called once per frame
	public float inUse () 
	{
		if (!is_playing) 
		{
			GetComponent<AudioSource>().loop = true;
			GetComponent<AudioSource> ().clip = pick;
			GetComponent<AudioSource> ().Play ();
			is_playing = true;
		}

		delay -= Time.deltaTime;
		return delay;
	}

	public int Payout()
	{
		GetComponent<AudioSource> ().Stop ();
		GetComponent<AudioSource> ().clip = open;
		GetComponent<AudioSource> ().Play ();
		GetComponent<AudioSource> ().loop = false;
		int temp = value;
		value = 0;
		GetComponent<SpriteRenderer> ().sprite = opened;
		return temp;
	}

	void OnCollisionExit2D(Collision2D col)
	{
		delay = normDelay;
		GetComponent<AudioSource> ().Stop ();
		is_playing = false;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		GetComponent<AudioSource> ().clip = hit;
		GetComponent<AudioSource> ().Play ();
		GetComponent<AudioSource> ().loop = false;
	}

}
