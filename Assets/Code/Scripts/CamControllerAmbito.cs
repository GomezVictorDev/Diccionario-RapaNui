using UnityEngine;
using System.Collections;

public class CamControllerAmbito : CamController {

	[SerializeField]
	private float timeToDefault=3f;
	private float currentTimeToDefault;
	private Vector2 defaultPos;

	bool flagRipple =false;


	private Vector2 ripplePosition;
	public Vector2 RipplePosition
	{
		set{ripplePosition = value; }
		get{return ripplePosition; }
	}
	private Vector2 rippleScale;
	public float SetRippleScale
	{
		set{rippleScale = new Vector2(value,value); }

	}

	[SerializeField]
	private RippleController rippleController;
	//private RippleEffect rippleEffect;
	[SerializeField]
	private Locucion locucion;

	public void SetClip(AudioClip clip)
	{
		locucion.SetClip (clip);
	}

	public enum CamAmbitoState
	{
		OnEvent,OnDefault,OnTransitionToDefault,OnTransitionToEvent
	};
	public static CamAmbitoState camAmbitoState= CamAmbitoState.OnDefault;
	void Start ()
	{
		base.Start ();
		defaultPos = cam.transform.position;
		//rippleEffect = GetComponentInChildren<RippleEffect> ();
	}
	//crear opcion de raycast
	// Update is called once per frame
	private void StartRippleEffect()
	{
		//rippleEffect.StartEffect (ripplePosition);
		rippleController.StartRippleEffects(ripplePosition,rippleScale);
		// dar la posicion al objeto ripple
		//iniciar la animacion del objeto ripple
	}
	protected override void OnSamePosition ()
	{
		locucion.Play ();
		StartRippleEffect ();
		currentTimeToDefault = timeToDefault;
		//metodo permite hacer click muchas veces sobre la misma posicion. Si se deja de hacer click el contador podra comenzar la cuenta regresiva para volver a la posicion por default
	}
	void Update () 
	{



		base.Update ();
		Vector2 endPos = new Vector2 (camMoveInterpolation.EndPos.x, camMoveInterpolation.EndPos.y);
		if (StartErp) 
		{
			flagRipple = true;
			if (currentTimeToDefault != timeToDefault) 
				currentTimeToDefault = timeToDefault;
			if (endPos != defaultPos) 
			{
				camAmbitoState = CamAmbitoState.OnTransitionToEvent;
				rippleController.CancelRiple ();
				locucion.Stop ();
			}
			if (endPos == defaultPos) 
			{
				camAmbitoState = CamAmbitoState.OnTransitionToDefault;
				rippleController.CancelRiple ();
				locucion.Stop ();

			}

		}
		if(!StartErp) //esto de abajo es parte de la camara en los ambitos y no de la camara global
		{
			if (timeToDefault > 0) 
			{
				Vector2 actualPos = cam.transform.position;
				if (actualPos != defaultPos) 
				{
					currentTimeToDefault -= Time.deltaTime;
					//aqui está posicionado en un evento
//					Debug.Log ("Time: " + currentTimeToDefault);
				}
				if (currentTimeToDefault < 0.1f) {

					GoToPosition (defaultPos, 14f);

					//se debe establecer una variable que diferencie ir a default desde el time y cuando es desde el "evento"
					//aqui estara viajando a la posicion por default
					currentTimeToDefault = timeToDefault;
				}
			
			}

			if (endPos != defaultPos ) 
			{
				camAmbitoState = CamAmbitoState.OnEvent;
				if (flagRipple) 
				{
					StartRippleEffect ();
					locucion.Play ();
					flagRipple = false;
					//defaultPositionFromTimer=false
				}
			}
			if (endPos == defaultPos) 
			{
				camAmbitoState = CamAmbitoState.OnDefault;
			}

		}




	
	}
}
