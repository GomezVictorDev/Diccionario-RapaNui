using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
//[RequireComponent(typeof(BoxCollider2D))]
public class Card : MonoBehaviour, IPointerClickHandler {
	// su función es dar a conocer que está en posicion de ser "abierta" o "cerrada" respectivamente.
	// Use this for initialization

	//BoxCollider2D boxCollider2D;
	private SideCard sideA,sideB;
	public enum CardState
	{
		Accessible,UnAccessible,OnCamera
	};
	[HideInInspector]
	public CardState cardState= CardState.UnAccessible; 
	private Vector3InterPolation moveInterPolation;
	private bool onTransition=false;
	private int closeMoveDistance=92;// ANCHO LADO DE TARJETA - COS(70);
	//private int OpenMoveDistance=280;

	//estas variables son para saber el orden de las tarjetas en sus diferentes idiomas.

	//public int OrderCard;

	//estas variables son para enviar los datos cuando se selecciona una tarjeta a la camara,  al ripple y al audio 
	[SerializeField]
	AudioClip espEventLocution,rapaEventLocution;
	[SerializeField]
	private Vector2 eventPosition;
	[SerializeField]
	private float eventZoom;
	[SerializeField]
	private Vector2 rippleLocalPosition;
	[SerializeField]
	private float rippleScale;
	public float GetEventZoom()
	{
		return eventZoom;
	}
	public Vector2 GetEventPosition()
	{
		return eventPosition;
	}



	public SideCard SideA
	{
		get{return sideA; }
	}
	public SideCard SideB
	{
		get{return sideB; }
	}
	void Awake () 
	{
		
		//boxCollider2D=GetComponent<BoxCollider2D>();
		moveInterPolation = new Vector3InterPolation ();
		//boxCollider2D.offset = Vector2.zero;
		//boxCollider2D.size = new Vector2 (40, 1);
		//boxCollider2D.isTrigger = true;
		for (int i = 0; i < transform.childCount; i++) 
		{
			if (transform.GetChild (i).tag.Equals ("SideA") && transform.GetChild (i).GetComponent<SideCard> ()) 
			{
				sideA = transform.GetChild (i).GetComponent<SideCard> ();
			}
			if (transform.GetChild (i).tag.Equals ("SideB") && transform.GetChild (i).GetComponent<SideCard> ()) 
			{
				sideB = transform.GetChild (i).GetComponent<SideCard> ();
			}
		}
	//	RectTransform temp = SideB.GetComponent<RectTransform> ();
	//	Mathf.Abs(temp.sizeDelta) - Mathf.Cos( Mathf.Abs(temp.eulerAngles.y) *Mathf.Deg2Rad ) ;
	
	}

	// Update is called once per frame
	void Update () // el problema es cuando se está expandiendo o contrayendo y pasa a llevar el collider mediante el raycast
	{
		
		if (cardState != CardState.OnCamera) 
		{
			if (sideA.currentCardState != SideCard.SideCardState.Open && sideB.currentCardState != SideCard.SideCardState.Open)
			{
				cardState = CardState.UnAccessible;

			} else 
			{
				cardState = CardState.Accessible;


			
			}
		}
//		Debug.Log ("Card: "+transform.name+"| State: "+ cardState);
	
	
	}
	public void ClearShadowA(float time)
	{
		sideA.ClearShadow (time);
	}
	public void ClearShadowB(float time)
	{
		sideB.ClearShadow (time);
	}
	public void ShowShadowA(float time)
	{
		sideA.ShowShadow (time);
	}
	public void ShowShadowB(float time)
	{
		sideB.ShowShadow (time);
	}
	public void ShowSoftShadow(float time)
	{

		sideA.SoftShadow (time);
		sideB.SoftShadow (time);
	}
	public  void OpenCard()
	{
		if (sideA.currentCardState == SideCard.SideCardState.Close && sideB.currentCardState == SideCard.SideCardState.Close) 
		{
			sideA.OpenSide ();
			sideB.OpenSide ();
			ClearShadowA (sideA.GetOpenCardTime);
			ClearShadowB (sideB.GetOpenCardTime);

		
		}
	}
	public void CloseCard()
	{
		if (sideA.currentCardState == SideCard.SideCardState.Open && sideB.currentCardState == SideCard.SideCardState.Open) 
		{
			sideA.CloseSide();
			sideB.CloseSide ();
			ShowSoftShadow (sideA.GetCloseCardTime);
	
		}
	}

	public void OnPointerClick (PointerEventData data)
	{
		Debug.Log ("MouseUp");
		if (sideA.currentCardState == SideCard.SideCardState.Open && sideB.currentCardState == SideCard.SideCardState.Open)
		{
			//cardState= CardState.OnCamera;
			GoToEvent (); //si se hace click antes de que el tiempo de espera a default llegue a cero en la camara lo reiniciara constantemente 
			// produciendo un loop si la persona clickea durante ese tiempo. Solucionar si esto llegase a quedar
		}

		

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
	public void GoToEvent()
	{
		//CamController.Instance.GoToPosition (eventPosition, eventZoom);
		CamControllerAmbito cam = CamControllerAmbito.Instance as CamControllerAmbito;
		cam.RipplePosition = rippleLocalPosition;
		cam.SetRippleScale = rippleScale;
		if (Lenguages.GetActualLenguageType() == Lenguages.LenguagesType.Esp ) 
		{
			cam.SetClip (espEventLocution);
		}
		if (Lenguages.GetActualLenguageType() == Lenguages.LenguagesType.Rapa ) 
		{
			cam.SetClip (rapaEventLocution);
		}

		CamControllerAmbito.Instance.GoToPosition(eventPosition,eventZoom);

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
