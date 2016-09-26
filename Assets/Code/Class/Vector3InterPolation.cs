using UnityEngine;
using System.Collections;

public class Vector3InterPolation 
{

	public enum LerpMode
	{
		Sinerp,Coserp,Cuadratic,Smoothstep,Smootherstep
	} ;

	private LerpMode lerpMode;
	float lerpTime = 1f;
	float currentLerpTime=0;
	float percentTime;

	float moveDistance=0;
	Vector3 startPos;
	Vector3 endPos;
	Vector3 interpolatedValue;

	private bool EndLerp;

	public float MoveDistance
	{
		get{return moveDistance; }
		set{moveDistance = value; }
	}

	public Vector3 StarPos
	{
		get{ return startPos; }
		set
		{
			
			startPos = value;
		//	Reset ();
		}
	}
	public Vector3 EndPos
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
	public Vector3 InterpolatedValue
	{
		get{ return interpolatedValue; }
	}
	public  Vector3InterPolation ()
	{
		this.startPos = Vector3.zero;
		this.endPos = Vector3.zero;
	}
	public  Vector3InterPolation (Vector3 starPos, Vector3 endPos)
	{
		this.startPos =starPos;
		this.endPos = endPos;
		lerpMode = LerpMode.Smootherstep;
	}
	public  Vector3InterPolation (Vector3 startPos,Vector3 direction,float moveDistance)
	{
		this.startPos =startPos;
		this.moveDistance = moveDistance;
		this.endPos = startPos + direction * moveDistance ;
	}
	public void EndPosByDistance(Vector3 direction,float moveDistance)
	{
		this.endPos = this.startPos + direction * moveDistance ;
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
		interpolatedValue=Vector3.Lerp(startPos, endPos, percentTime);
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
		interpolatedValue = Vector3.zero;
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
