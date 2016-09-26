using UnityEngine;
using System.Collections;

public class RippleEffect : MonoBehaviour {

	// Use this for initialization
	Vector2 position;
	Animator animator;
	RectTransform rectTransform;
	void Start () {
		animator=GetComponent<Animator> ();
		rectTransform = GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	public void StartEffect(Vector2 pos)
	{
		//rectTransform.anchoredPosition = pos;
		transform.localPosition=pos;
		animator.SetTrigger ("StartRipple");
	}

}
