using UnityEngine;
using System.Collections;

public class MoveBarElement : MonoBehaviour {

	// Use this for initialization	
	public float moveLerpTime = 1f;
	public float moveDistance = 10f;

	private float currentLerpTime=0f;

	Vector3 initialPos;
	Vector3 currentPos;
	Vector3 endPos;

	Vector3 initialScale = new Vector3(.9f,.8f,1);
	Vector3 endScale = new Vector3(1,1,1);

	bool moveDone = true;
	bool scaleDone=true;

	Vector3InterPolation moveInterpolation;
	Vector3InterPolation scaleInterpolation;


	void Start()
	{
		transform.localScale = initialScale;
		moveInterpolation = new Vector3InterPolation ();
		ResetInitialPosition (transform.position);
		ResetEndPosition (Vector3.right);

	}
	void ResetInitialPosition(Vector3 newPos)
	{
		initialPos = newPos;
		moveInterpolation.StarPos = initialPos;
	}
	void ResetEndPosition(Vector3 direction)
	{
		
		moveInterpolation.EndPosByDistance(direction,moveDistance);
	}
	public void MovingLeft()
	{
		if (moveDone)
		{
			ResetInitialPosition (transform.position);
			ResetEndPosition (-Vector3.right);
			StartCoroutine (MovingRoutine ());
		}


	}
	public void MovingRight()
	{
		if (moveDone) 
		{
			ResetInitialPosition (transform.position);
			ResetEndPosition (Vector3.right);
			StartCoroutine (MovingRoutine ());

		}


	}
	public void ScaleUp()
	{
		
	}
	private IEnumerator MovingRoutine()
	{
		moveDone = false;


	
		while (moveInterpolation.CurrentLerpTime< moveInterpolation.LerpTime)
		{
			moveInterpolation.UpdateLerp ();
			transform.position = moveInterpolation.InterpolatedValue;
			Debug.Log (moveInterpolation.CurrentLerpTime);
			yield return null;
		}
		moveDone = true;

	}
	/*private IEnumerator  ScaleRoutine()
	{


		
	}*/



}
