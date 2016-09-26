using UnityEngine;
using System.Collections;

public class FloatInterpolation
{
	public enum LerpMode
	{
		Sinerp,Coserp,Cuadratic,Smoothstep,Smootherstep
	} ;

	private LerpMode lerpMode;
	private float lerpTime = 1f;
	private float currentLerpTime=0;
	private float percentTime;

	private float moveDistance=0;

	private float startPos;
	private float endPos;
	private float interpolatedValue;

	private bool angleMode;
	private bool EndLerp;
	public bool AngleMode
	{
		get{return angleMode; }
		set{angleMode = value; }
	}
	public float MoveDistance
	{
		get{return moveDistance; }
		set{moveDistance = value; }
	}

	public float StarPos
	{
		get{ return startPos; }
		set
		{
			
			startPos = value;
		//	Reset ();
		}
	}
	public float EndPos
	{
		get{ return endPos; }
		set
		{

			endPos = value; 
		//	Reset ();
		}
	}
	public float CurrentLerpTime
	{
		get{return currentLerpTime; }
	}
	public float LerpTime
	{
		get{return lerpTime; }
		set{lerpTime = value;}
	}
	public float InterpolatedValue
	{
		get{ return interpolatedValue; }
	}
	public  FloatInterpolation ()
	{
		this.startPos = 0f;
		this.endPos = 0f;
		lerpMode = LerpMode.Smootherstep;
	}
	public FloatInterpolation (float starPos, float endPos)
	{
		this.startPos =starPos;
		this.endPos = endPos;
		lerpMode = LerpMode.Smootherstep;
	}
	public  FloatInterpolation(float startPos,int direction,float moveDistance)
	{
		float dir = Mathf.Sign (direction);
		this.startPos =startPos;
		this.moveDistance = moveDistance;
		this.endPos = startPos + dir * moveDistance ;
	}
	public void EndPosByDistance(float direction,float moveDistance)
	{
		float dir = Mathf.Sign (direction);
		this.endPos = this.startPos + dir * moveDistance ;
	}
	private void UpdateTime() 
	{

		//increment timer once per frame
		currentLerpTime += Time.deltaTime;
		if (currentLerpTime > lerpTime) {
			currentLerpTime = lerpTime;
		}

		//lerp!
		percentTime = currentLerpTime / lerpTime;
		//	transform.position = Vector3.Lerp(startPos, endPos, perc);
	}
	public void SwitchLerp(LerpMode lerpMode)
	{
		//ResetartTime ();
		this.lerpMode= lerpMode;

	}
	private void Lerping()
	{
		if (angleMode) {
			interpolatedValue = Mathf.LerpAngle (startPos, endPos, percentTime);
			
		}
		else 
		{
			interpolatedValue= Mathf.Lerp(startPos, endPos, percentTime);
		}

	}
	public void UpdateLerp()
	{
		UpdateTime ();
		switch (lerpMode)
		{
		case LerpMode.Sinerp:
			Sinerp ();
			break;
		case LerpMode.Coserp:
			Coserp ();
			break;
		case LerpMode.Cuadratic:
			Cuadratic ();
			break;
		case LerpMode.Smoothstep: 
			Smoothstep ();
			break;
		case LerpMode.Smootherstep:
			Smootherstep ();
			break;

		}

		Lerping();

	}
	public void ResetartTime()
	{
		currentLerpTime = 0;
	}
	private void RestartInterpolatedValue()
	{
		interpolatedValue = 0;
	}
	public void Reset()
	{
		ResetartTime ();
		RestartInterpolatedValue ();
	}
	private void Sinerp()
	{
		percentTime = Mathf.Sin(percentTime * Mathf.PI * 0.5f);
	}
	private void Coserp()
	{
		percentTime = 1f - Mathf.Cos (percentTime * Mathf.PI * 0.5f);
	}

	private void Cuadratic()
	{
		percentTime = percentTime * percentTime;
	}

	private void Smoothstep()
	{
		percentTime = percentTime * percentTime * (3f - 2f * percentTime);
	}
	private void Smootherstep()
	{
		percentTime = percentTime * percentTime * percentTime * (percentTime * (6f * percentTime - 15f) + 10f);
	}



}
