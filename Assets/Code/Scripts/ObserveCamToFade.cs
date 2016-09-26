	using UnityEngine;
using System.Collections;

public class ObserveCamToFade : MonoBehaviour {

	// el fade está con una animación aunque podría crearse un metodo que lo haga en este script

	public Animator anim;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (CamControllerAmbito.camAmbitoState == CamControllerAmbito.CamAmbitoState.OnDefault) 
		{
		//	Debug.Log ("ONDEFAULTTT!!");
			if(!anim.GetBool("Enable"))
				{
				FadeIn ();
				}
		}
		if (CamControllerAmbito.camAmbitoState == CamControllerAmbito.CamAmbitoState.OnTransitionToEvent) // esto debe ser a penas comience a moverce la tarjeta para ser más veloz e impedir el glitch 
		{
//			Debug.Log ("OnTransitionToEvent");
			if(anim.GetBool("Enable"))
				{
				FadeOut ();
				}
		}
	
	}
	void FadeIn()
	{
		anim.SetBool ("Enable", true);

	}
	void FadeOut()
	{
		anim.SetBool ("Enable", false);

	}
}
