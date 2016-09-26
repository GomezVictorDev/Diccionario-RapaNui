using UnityEngine;
using System.Collections;

public class FalseCards : MonoBehaviour {

	// Use this for initialization
	bool onTransition =false;
	Vector3InterPolation moveInterPolation;
	float closeMoveDistance=92;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void RightMove(float time)
	{
		if (!onTransition)
		{

			moveInterPolation = new Vector3InterPolation (transform.localPosition, transform.localPosition + Vector3.right * closeMoveDistance);
			moveInterPolation.SwitchLerp (Vector3InterPolation.LerpMode.Sinerp);
			moveInterPolation.LerpTime = time;
			StartCoroutine (Move ());
		}

	}
	public void LeftMove(float time)
	{
		if (!onTransition) 
		{



			moveInterPolation = new Vector3InterPolation( transform.localPosition, transform.localPosition + Vector3.right* -closeMoveDistance);
			moveInterPolation.SwitchLerp (Vector3InterPolation.LerpMode.Sinerp);
			moveInterPolation.LerpTime = time;

			StartCoroutine (Move ());
		}


	}
	IEnumerator Move()
	{
		//scrollRect.horizontal= false;
		onTransition = true;
		//boxCollider2D.enabled = false;
		while (moveInterPolation.CurrentLerpTime < moveInterPolation.LerpTime) {
			moveInterPolation.UpdateLerp ();

			transform.localPosition = moveInterPolation.InterpolatedValue;

			yield return null;
		}
		onTransition = false;
		//boxCollider2D.enabled = true;

	}

}
