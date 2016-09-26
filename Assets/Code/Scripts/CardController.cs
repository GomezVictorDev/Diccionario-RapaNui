using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class CardController : MonoBehaviour {

	// Use this for initialization
//	[SerializeField]
//	ScrollRect scrollRect;
	Card [] cards;
	//Transform [] transformContent;
	public FalseCards rightFalseCards;// si no se ingresan las falsas tarjetas estas no se moveran al abrirse o cerrarse la tarjeta
	public FalseCards leftFalseCards;
	int currentActive=0;
	int oldActive=-1;

	float waitingTime=.4f ;// este tiempo debe ser la suma de sidecar closecardtime + opencardtime + speedErp  del scrollcontroller
	float currentWaitTime;
	bool startWait=false;

	public enum CardControllerState
	{
		OnStay,OnMove
	};
	public  CardControllerState cardControllerState= CardControllerState.OnStay;
	private static CardController instance;
	public static CardController Instance
	{
		get
		{

			return instance;
		}	
	}
	void Awake()
	{
		
			instance = this;
			currentWaitTime = waitingTime;
			
	}
	void Start () 
	{
		RectTransform[] temp = ScrollControler.Instance.CardsRectTransform;
		cards = new Card[temp.Length];
		for (int i = 0; i < temp.Length; i++) 
		{
			cards [i]=temp[i].transform.GetComponent<Card> ();
		//	Debug.Log (cards [i].name);
		}
		//valores por default. crear un me
		DefaultState();


	}
	private void DefaultState()
	{
		
		//startWait = true;
		cards [0].OpenCard ();
		WaveExpand ();
		//cards [currentActive].GoToEvent ();

	}
	public void CardActivator(int index)
	{
		if(index != currentActive)
		{
			
			oldActive = currentActive;	
			cards [oldActive].CloseCard ();
			WaveContract ();// cuando todas se terminen de cerrar  ejecutar el resto 
			startWait=true;// empieza una espera para abrir la tarjeta  nueva 
			
			
		}
		currentActive = index;

		//tiene que entrar una vez y lo hace multiples veces

	} 

	
	// Update is called once per frame
	void Update () 
	{
		if (startWait) 
		{
			currentWaitTime =currentWaitTime - Time.deltaTime;

			if (currentWaitTime <= 0) {
				cards [currentActive].OpenCard ();
			//	cards [currentActive].GoToEvent ();
				WaveExpand ();
				startWait = false;
				currentWaitTime = waitingTime;

			}
		}
		if (oldActive == -1) {
			if (cards [currentActive].cardState == Card.CardState.Accessible) 
			{
				cardControllerState = CardControllerState.OnStay;
			}
		} else
		{
			if (cards [currentActive].cardState == Card.CardState.Accessible && cards [oldActive].cardState == Card.CardState.UnAccessible) 
			{
				//solo en este momento podemos hacer el swipe
				cardControllerState = CardControllerState.OnStay;
			} 
			else
			{
				cardControllerState= CardControllerState.OnMove;
			}
		}

		Debug.Log ("CardController STATE: "+cardControllerState );

		/*if (currentActive >= 0) 
		{
			if ( cards [currentActive].cardState == Card.CardState.UnAccessible) {
				scrollRect.horizontal = false;//llamar al scroll controler para que realice está funcion
			} else 
			{
				scrollRect.horizontal = true;//llamar al scroll controler para que realice está funcion
			}
			if ( scrollRect.velocity.x > 0 || scrollRect.velocity.x < 0) {//llamar al scroll controler para que realice está funcion
					cards [currentActive].CloseCard ();
					WaveContract ();
					currentActive = -1;
					
				}
			
		}*/

	
	
	}

	private void WaveContract()
	{
		//deben contraerse las tarjetas falsas 

		rightFalseCards.LeftMove (cards[currentActive].SideB.GetCloseCardTime);
		leftFalseCards.RightMove (cards[currentActive].SideA.GetCloseCardTime);
		if (currentActive > 0 && currentActive < cards.Length) {

			for (int i = currentActive + 1; i < cards.Length; i++) {
				cards [i].LeftMove (cards[currentActive].SideB.GetCloseCardTime);
			//	cards [i].ShowShadowA (cards [currentActive].SideB.GetCloseCardTime);
			//	cards [i].ShowShadowB (cards [currentActive].SideB.GetCloseCardTime);

			}
			for (int i = currentActive- 1; i >= 0; i--) {
				cards [i].RightMove (cards[currentActive].SideA.GetCloseCardTime);
			//	cards [i].ShowShadowA (cards [currentActive].SideB.GetCloseCardTime);
			//	cards [i].ShowShadowB (cards [currentActive].SideB.GetCloseCardTime);

			}

		} else {
			if (currentActive == 0) {
				//mover a la izq  del 1 en adelante;
				for (int i = 1; i < cards.Length; i++) {
					cards [i].LeftMove (cards[currentActive].SideB.GetCloseCardTime);
					//cards [i].ShowShadowA (cards [currentActive].SideB.GetCloseCardTime);
					//cards [i].ShowShadowB (cards [currentActive].SideB.GetCloseCardTime);

				}
			}
			if (currentActive == cards.Length - 1) {
				//mover a la derecha  del total de cartas menos uno hacia atras;
				for (int i = currentActive - 1; i >= 0; i--) {
					cards [i].RightMove (cards[currentActive].SideA.GetCloseCardTime);
					//cards [i].ShowShadowA (cards [currentActive].SideB.GetCloseCardTime);
					//cards [i].ShowShadowB (cards [currentActive].SideB.GetCloseCardTime);


				}
			}

		}


	}
	private void WaveExpand()
	{
		rightFalseCards.RightMove (cards[currentActive].SideB.GetOpenCardTime);
		leftFalseCards.LeftMove(cards[currentActive].SideA.GetOpenCardTime);
		//deben expandirse las tarjetas falsas 
		if (currentActive > 0 && currentActive < cards.Length -1) {
			for (int i = currentActive + 1; i < cards.Length; i++) {

				cards [i].RightMove (cards[currentActive].SideB.GetOpenCardTime);
				cards [i].ShowShadowA (cards [currentActive].SideB.GetOpenCardTime);
				cards [i].ClearShadowB (cards [currentActive].SideB.GetOpenCardTime);
			}
			for (int i = currentActive - 1; i >= 0; i--) {
				cards [i].LeftMove (cards[currentActive].SideA.GetOpenCardTime);
				cards [i].ShowShadowB (cards [currentActive].SideB.GetOpenCardTime);
				cards [i].ClearShadowA (cards [currentActive].SideB.GetOpenCardTime);
			}

		} 
		else
		{

			if (currentActive == 0)
			{
				for (int i = 1; i < cards.Length; i++) 
				{
					cards [i].RightMove (cards[currentActive].SideB.GetOpenCardTime);
					cards [i].ShowShadowA (cards [currentActive].SideB.GetOpenCardTime);
					cards [i].ClearShadowB (cards [currentActive].SideB.GetOpenCardTime);
				}
			}
			if (currentActive  == cards.Length-1) 
			{

				for (int i = currentActive-1; i >= 0; i--)
				{
					cards [i].LeftMove (cards[currentActive].SideA.GetOpenCardTime);
					cards [i].ShowShadowB (cards[currentActive].SideA.GetOpenCardTime);
					cards [i].ClearShadowA(cards[currentActive].SideA.GetOpenCardTime);

				}
			}
		}




	}
}
