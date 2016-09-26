using UnityEngine;
using System.Collections;

public class Delay {

	// Use this for initialization
	float initialTime;
	float currentTime;
	bool startDelay;

	public float InitialTime
	{
		get{ return initialTime; }
		set{initialTime = value; }
	}
	public bool DelayEnd()
	{
		if (startDelay) 
		{
			return false;
		} else 
		{
			return true;
		}

		
	}
	public Delay (float time)
	{
		this.initialTime = time;
		currentTime = time;
		startDelay = false;
	}
	
	// Update is called once per frame
	public void StartDelay()
	{
		startDelay = true;
	}
	public void Update () 
	{
		if (startDelay) 
		{
			currentTime -= Time.deltaTime;

			if (currentTime < 0.1f) 
			{
				startDelay = false;
				currentTime = initialTime;
			}
		}
	
	}
}
