using UnityEngine;
using System.Collections;

public class RippleController : MonoBehaviour {



	[SerializeField]
	RippleEffect2 [] rippleEffects= new RippleEffect2[2];

	private float timeBetwenEffects;

	Delay delay;
	bool startRippleEffects=false;

	private Vector2 position;
	private Vector2 scale;
	void Start () 
	{
		
		
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (startRippleEffects) 
		{
			
			delay.Update ();
			if (delay.DelayEnd ()) 
			{
				Vector2 tempScale = this.scale * .7f;
				rippleEffects[1].timeToScale= rippleEffects[0].timeToScale * 1.2f;
				rippleEffects [1].StartRippleEffect (position, tempScale);
				startRippleEffects = false;
			}
		}
	
	}
	public void CancelRiple()
	{
		rippleEffects [0].CancelRippleEffect ();
		rippleEffects [1].CancelRippleEffect ();
	}
	public void StartRippleEffects(Vector2 position, Vector2 scale)
	{
		this.position = position;
		this.scale = scale;
		rippleEffects [0].StartRippleEffect (position, scale);

		timeBetwenEffects = rippleEffects [0].timeToScale * .4f;
		delay = new Delay (timeBetwenEffects);
		startRippleEffects = true;
		delay.StartDelay ();
		
	}
}
