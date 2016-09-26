using UnityEngine;
using System.Collections;

public class Color32InterPolation 
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
	Color32 startPos;
	Color32 endPos;
	Color32 interpolatedValue;

	private bool EndLerp;

	public float MoveDistance
	{
		get{return moveDistance; }
		set{moveDistance = value; }
	}

	public Color32 StarPos
	{
		get{ return startPos; }
		set
		{
			
			startPos = value;
		//	Reset ();
		}
	}
	public Color32 EndPos
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
	public Color32 InterpolatedValue
	{
		get{ return interpolatedValue; }
	}
	public  Color32InterPolation  ()
	{
		this.startPos = Color.white;
		this.endPos = Color.black;
	}
	public  Color32InterPolation (Color32 starPos, Color32 endPos)
	{
		this.startPos =starPos;
		this.endPos = endPos;
		lerpMode = LerpMode.Smootherstep;
	}
/*	public  Vector3InterPolation (Vector3 startPos,Vector3 direction,float moveDistance)
	{
		this.startPos =startPos;
		this.moveDistance = moveDistance;
		this.endPos = startPos + direction * moveDistance ;
	}*/
	public void EndPosByDistance(Vector3 direction,float moveDistance)
	{
	//	this.endPos = this.startPos + direction * moveDistance ;
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
		interpolatedValue=Color32.Lerp(startPos, endPos, percentTime);
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
		interpolatedValue = Color.white;
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
