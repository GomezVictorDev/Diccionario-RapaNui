using UnityEngine;
using System.Collections;

public class RippleEffect2 : MonoBehaviour {

	// Use this for initialization
	Vector3InterPolation scaleInterpolation;
	Color32InterPolation fadeInterPolation;

	//private Vector2 finalScale;

	public float timeToFade,timeToScale;
	public int initialAlfa,finalALfa;
	SpriteRenderer circle;
	Color32 startCircleColor;
	Color32  endCircleColor;
	bool startInterpolation=false;
	Delay delay;
	void Start () 
	{
		
		circle = GetComponent<SpriteRenderer> ();

		initialAlfa = Mathf.Clamp (0, 255, initialAlfa);
		startCircleColor = circle.color;
		startCircleColor = new Color32 (startCircleColor.r,startCircleColor.g,startCircleColor.b,(byte)initialAlfa);
		endCircleColor = new Color32 (startCircleColor.r,startCircleColor.g,startCircleColor.b, (byte)finalALfa);

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (startInterpolation) 
		{
			if (scaleInterpolation.InterpolatedValue != scaleInterpolation.EndPos) 
			{
				scaleInterpolation.UpdateLerp ();
				transform.localScale = scaleInterpolation.InterpolatedValue;
			}
			delay.Update ();
		
			if (delay.DelayEnd()) 
			{
//				Debug.Log ("fINAL DELAY!");
				if(fadeInterPolation.CurrentLerpTime != fadeInterPolation.LerpTime) 
				{
					fadeInterPolation.UpdateLerp ();
					//int newInterpolated =(int) fadeInterPolation.InterpolatedValue;

					//circleColor = new Color32 ((byte)circle.color.r, (byte)circle.color.g, (byte)circle.color.b, (byte)newInterpolated);

					circle.color = fadeInterPolation.InterpolatedValue;
				}
				else
				{
					startInterpolation = false;
				}

			}

		}
	
	}
	public void CancelRippleEffect()
	{
		if (startInterpolation) 
		{
			startInterpolation = false;
			transform.localScale = Vector2.zero;
		}
	}
	public void StartRippleEffect(Vector2 position,Vector2 scale)
	{
		transform.localPosition = new Vector3(position.x,position.y, transform.localPosition.z);
		transform.localScale = Vector2.zero;
		circle.color = startCircleColor;
		scaleInterpolation = new Vector3InterPolation (Vector2.zero, scale);
		scaleInterpolation.LerpTime = timeToScale;


		fadeInterPolation = new Color32InterPolation (startCircleColor, endCircleColor);
		fadeInterPolation.LerpTime = timeToFade;

		scaleInterpolation.SwitchLerp (Vector3InterPolation.LerpMode.Sinerp);
		fadeInterPolation.SwitchLerp (Color32InterPolation.LerpMode.Sinerp);

		delay = new Delay (timeToScale * .6f);

		delay.StartDelay ();
		startInterpolation = true;
	}


}
