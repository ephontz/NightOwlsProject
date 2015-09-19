using UnityEngine;
using System.Collections;

public enum LIGHT_LEVEL {invisible = 0, dark =1, shadow = 2, normal = 3, full = 4, bright = 5}

public class Invisiblilityscript : MonoBehaviour {

	public int curLight = 3;
	bool invisActive = false;
	bool onCooldown = false;
	public float duration = 3;
	public float fullDuration = 3;
	public float cooldown = 30;
	public float fullCooldown = 30;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (invisActive) 
		{
			duration -= Time.deltaTime;
			if(duration <= 0)
			{
				invisActive = false;
				duration = fullDuration;
				onCooldown = true;
				curLight += 1;
			}
		}
		if (onCooldown) 
		{
			cooldown -= Time.deltaTime;
			if(cooldown <= 0)
			{
				cooldown = fullCooldown;
				onCooldown = false;
			}
		}
	}

	public void SetExposure(int i)
	{
		curLight = i;
	}

	public void Invisibility()
	{
		if (!onCooldown && !invisActive) 
		{
			invisActive = true;
			curLight -= 1;
			if (curLight < 0)
				curLight = 0;
		}

	}

	public int LightExposure()
	{
		return curLight;
	}
}
