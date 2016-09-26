using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SideCard : MonoBehaviour {



	Vector3InterPolation closeCardColor;
	Image imageColor;
	Color32 endColor;

	float  angleToRotate=0; 
	const float   widthCard=140f;
	float distanceToMove;
	FloatInterpolation erpRotation;
	FloatInterpolation erpMove;

	const float closeCardTime=0.3f;
	const float openCardTime=0.4f;
	//la suma de open más close debe ser el tiempo de espera para moverse entre tarjetas
	public float GetCloseCardTime
	{
		get{return closeCardTime; }
	}
	public float GetOpenCardTime
	{
		get{return openCardTime; }
	}
	//private enum Proces{ OnClosing,OnOpening,OnProcesing};
	//private Proces proces;
	public enum SideCardState
	{
		Opening,Closing,Open,Close
	};
	[HideInInspector]
	public SideCardState currentCardState= SideCardState.Close; 


	bool onTransition=false;

	public float DistanceToMove
	{
		get{return distanceToMove; }
	}
	public bool OnTransition
	{
		get{ return onTransition; }
	}
	void Awake () 
	{
		//por defecto deben comenzar todas cerradas
		currentCardState=SideCardState.Close;
		imageColor= GetComponent<Image>();
		closeCardColor = new Vector3InterPolation();

		imageColor.color=  new Color32(255,255,255,255);
		if (transform.tag.Equals ("SideA")) 
		{
			if (currentCardState == SideCardState.Open) 
			{
				//establece los datos para cerrar la tarjeta
			//	endColor= new Color32(255,255,255,255);
				angleToRotate=0;
			}


			if (currentCardState == SideCardState.Close) 
			{
				//establece los datos para abrir la tarjeta
			//	endColor= new Color32(155,155,155,255);
				angleToRotate=-70f;
			}

		}
		if (transform.tag.Equals ("SideB")) 
		{
			if (currentCardState == SideCardState.Open) 
			{
				
				angleToRotate = 0;
			}
			if (currentCardState == SideCardState.Close) 
			{
			//	endColor= new Color32(155,155,155,255);
				angleToRotate = 70;
			}

			//distanceToMove = distanceToMove * -1;
		}
		distanceToMove=   Mathf.Cos(Mathf.Abs(angleToRotate)* Mathf.Deg2Rad)* widthCard * Mathf.Sign( transform.localPosition.x) ;
		transform.localPosition = new Vector3 (distanceToMove, 0, 0);
		transform.eulerAngles = new Vector3 (0, angleToRotate, 0);

	}

	
	// Update is called once per frame
	void Update () 
	{
		
		
	}

	public void OpenSide()
	{
		if (!onTransition && currentCardState== SideCardState.Close) 
		{
			//proces = Proces.OnOpening;
			setAngle ();
			currentCardState = SideCardState.Opening;
		
			erpRotation.LerpTime= openCardTime;
			StartCoroutine (RotationRutine ());


		}

	}
	public void CloseSide()
	{
		if(!onTransition && currentCardState== SideCardState.Open)
		{
			setAngle ();
			currentCardState = SideCardState.Closing;
			erpRotation.LerpTime= closeCardTime;
			StartCoroutine (RotationRutine ());


		}
	}
	private void setAngle()
	{
		//dependiendo el estado de currentCardState es si al darle play la tarjeta estara en su posicion abierta o cerrada


		if (transform.tag.Equals ("SideA")) 
		{
			if (currentCardState == SideCardState.Open) 
			{
				//establece los datos para cerrar la tarjeta
			//	endColor = new Color32(155,155,155,255);
				angleToRotate=-70;
			}


			if (currentCardState == SideCardState.Close) 
			{
				//establece los datos para abrir la tarjeta
			//	endColor = new Color32(255,255,255,255);
				angleToRotate=359.9f;
			}

		}
		if (transform.tag.Equals ("SideB")) 
		{
			if (currentCardState == SideCardState.Open) 
			{
				
				angleToRotate = 70;

			}
			if (currentCardState == SideCardState.Close) 
			{
				
				angleToRotate = 0;
			}

			//distanceToMove = distanceToMove * -1;
		}
		distanceToMove=   Mathf.Cos(Mathf.Abs(angleToRotate)* Mathf.Deg2Rad)* widthCard * Mathf.Sign( transform.localPosition.x) ;

		erpRotation= new FloatInterpolation(transform.eulerAngles.y,angleToRotate);
		erpMove = new FloatInterpolation (transform.localPosition.x,distanceToMove);
		erpRotation.SwitchLerp (FloatInterpolation.LerpMode.Sinerp);
		erpMove.SwitchLerp (FloatInterpolation.LerpMode.Sinerp);
		erpRotation.AngleMode = true;
	}
	public void ClearShadow(float time)
	{
		endColor= new Color32(255,255,255,255);
		StartShadowRutine (time);
	}
	public void ShowShadow(float time)
	{
		endColor= new Color32(155,155,155,255);
		StartShadowRutine (time);
	}
	public void SoftShadow(float time)
	{
		endColor= new Color32(245,245,245,255);
		StartShadowRutine (time);
	}
	private void StartShadowRutine(float time)
	{
		Color32 starColor = imageColor.color;
		closeCardColor.StarPos = new Vector3 (starColor.r, starColor.g, starColor.b);
		closeCardColor.EndPos = new Vector3 (endColor.r, endColor.g, endColor.b);
		closeCardColor.Reset ();
		closeCardColor.LerpTime = time;
		StartCoroutine (ShadowRutine());
	}
	private IEnumerator ShadowRutine()
	{
		while (closeCardColor.CurrentLerpTime != closeCardColor.LerpTime) 
		{
			closeCardColor.UpdateLerp ();
			imageColor.color = new Color32 ((byte)closeCardColor.InterpolatedValue.x,(byte) closeCardColor.InterpolatedValue.y,(byte) closeCardColor.InterpolatedValue.z,255);
			yield return null;
		}
	}
	private IEnumerator RotationRutine()
	{
		onTransition = true;
	//	erpRotation.Reset ();

		string sideTag=transform.tag;
		erpMove.LerpTime = erpRotation.LerpTime;
	
		while (erpRotation.CurrentLerpTime < erpRotation.LerpTime)
		{
			
		

			erpRotation.UpdateLerp();
			erpMove.UpdateLerp ();

			transform.localPosition= new Vector3( erpMove.InterpolatedValue,0,0);
			transform.eulerAngles =Vector3.up* erpRotation.InterpolatedValue;
			//Debug.Log (Mathf.Round (erpRotation.InterpolatedValue));
			yield return null;
		}
		if (currentCardState == SideCardState.Opening) 
		{
			currentCardState = SideCardState.Open;
		}
		if (currentCardState == SideCardState.Closing) 
		{
			currentCardState = SideCardState.Close;
		}
		onTransition = false;
	}




}
