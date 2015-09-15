using UnityEngine;
using System.Collections;

public class containerscript : MonoBehaviour {


	float normDelay = 1.0f;
	float delay = 1.0f;
	int value = 10;
	GameObject loot = null;
	public Sprite opened;
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
		delay -= Time.deltaTime;
		return delay;
	}

	public int Payout()
	{
		int temp = value;
		value = 0;
		GetComponent<SpriteRenderer> ().sprite = opened;
		return temp;
	}

	void OnCollisionExit2D(Collision2D col)
	{
		delay = normDelay;
	}

}
