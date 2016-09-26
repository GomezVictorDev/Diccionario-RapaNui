using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class ScrollControler : MonoBehaviour {// pasar el order de las posiciones del cardcontroller acá. Luego desde el cardcontroller pedir los transform de las diferentes tarjetas

	// Use this for initialization

	//float speedDrag=22;
	float speedErp=.2f;
	[SerializeField]
	private RectTransform center;//el centro para comparar la distancia del scroll panel
	[SerializeField]
	private RectTransform scrollPanel;// el padre de las tarjetas en el cual se efectuara el scroll
	//public ScrollRect scrollRect;
	[SerializeField]
	private  RectTransform [] cardsRectTransform;// los rect trasform de las "tarjetas"
	public RectTransform [] CardsRectTransform
	{
		get{return cardsRectTransform; }
	}
	private float[] distances;// todos las distancia de los botones desde el centro
	private bool dragging=true;
	private int cardDistance;
	private int minDistanceIndex;//tendremos el valor del elemento de cardrectransform que está más cerca del centro

	FloatInterpolation interpolationX;
	float minClampX,maxClampX;

	bool onTransition=false;



	private static ScrollControler instance;
	public static ScrollControler Instance
	{
		get
		{

			return instance;
		}	
	}

	void Awake()
	{

		instance = this;
		cardDistance = (int)Mathf.Abs (cardsRectTransform [2].anchoredPosition.x - cardsRectTransform [1].anchoredPosition.x); // la distancia es entre el 2 y el 1 porque se debe tomar la distancia entre tarjetas cerradas y la 0 comienza "abierta", eso hace que la distancia sea mayor
		// obtenemos la distancia entre dos rectTransforms
		OrderByNumbers ();

	}
	void Start () 
	{	
		interpolationX = new FloatInterpolation ();
		interpolationX.SwitchLerp (FloatInterpolation.LerpMode.Sinerp);
		interpolationX.LerpTime = speedErp ;
		interpolationX.StarPos = scrollPanel.anchoredPosition.x;
		interpolationX.EndPos = scrollPanel.anchoredPosition.x;
		// ordenamos las posiciones por si no se han agregado correctamente al arreglo
		distances = new float[cardsRectTransform.Length];//inicializamos el arreglo con el tamaño necesario


		minClampX = scrollPanel.anchoredPosition.x - cardDistance *( cardsRectTransform.Length-1);  //definimos para el minimo el valor actual del scroll panel el cual deberia estar con el pivot a la izq y en el centro de la pantalla
		maxClampX = scrollPanel.anchoredPosition.x ;//el final del scroll sera igual a la posicion actual más la distancia entre tarjeta por la cantidad de estas


	}
	public void MoveBarToCenter()
	{
		if (!onTransition) 
		{
			int distanceToMove = minDistanceIndex * -cardDistance;// esta es la distancia a la que se tiene que mover el panel para dejar la tarjeta más cercana al centro en el centro
			interpolationX.StarPos = scrollPanel.anchoredPosition.x;
			interpolationX.EndPos = distanceToMove;
			interpolationX.ResetartTime ();
			StartCoroutine (Move ());
			/*if (interpolationX.CurrentLerpTime >= interpolationX.LerpTime) 
		{
			
		}*/
		}

	}
	public void MoveBarToIndex(int index)
	{
		if (!onTransition && CardController.Instance.cardControllerState== CardController.CardControllerState.OnStay) 
		{
			//minDistanceIndex = index;
			//al ocupar este metodo debemos hacegurarnos que min distance index no se reacalculara en el update, en vez de eso será igual a index
			int distanceToMove = index * -cardDistance;// esta es la distancia a la que se tiene que mover el panel para dejar la tarjeta más cercana al centro en el centro
			interpolationX.StarPos = scrollPanel.anchoredPosition.x;
			interpolationX.EndPos = distanceToMove;
			interpolationX.ResetartTime ();
			StartCoroutine (Move (index));
			/*if (interpolationX.CurrentLerpTime >= interpolationX.LerpTime) 
		{
			
		}*/
		}

	}
	public void MoveBarStepFoward()
	{
		if(!onTransition && CardController.Instance.cardControllerState== CardController.CardControllerState.OnStay)
		if (minDistanceIndex + 1 < cardsRectTransform.Length) 
		{
//			Debug.Log("MoveStepFoward Enter");
			int distanceToMove = (minDistanceIndex + 1) * -cardDistance;

			interpolationX.StarPos = scrollPanel.anchoredPosition.x;
			interpolationX.EndPos = distanceToMove;
			interpolationX.ResetartTime ();
			StartCoroutine (Move ());
			/*if (interpolationX.CurrentLerpTime >= interpolationX.LerpTime) {
				interpolationX.ResetartTime ();
			}*/
		}
	}
	public void MoveBarStepBackward()
	{
		if(!onTransition && CardController.Instance.cardControllerState== CardController.CardControllerState.OnStay)
		if (minDistanceIndex - 1 >= 0)
		{
		//	Debug.Log("MoveStepBack Enter");
			int distanceToMove = (minDistanceIndex - 1) * -cardDistance;
			interpolationX.StarPos = scrollPanel.anchoredPosition.x;
			interpolationX.EndPos = distanceToMove;
			interpolationX.ResetartTime ();
			StartCoroutine (Move ());
			/*if (interpolationX.CurrentLerpTime >= interpolationX.LerpTime) 
			{
				interpolationX.ResetartTime ();

			}*/
		}
	}

	void OrderByNumbers()
	{
		OrderCards[] orderCards = new OrderCards[cardsRectTransform.Length];
		int[] RapaOrder= new int[orderCards.Length];
		int[] EspOrder= new  int[orderCards.Length];
		for (int i = 0; i < cardsRectTransform.Length; i++) 
		{
			orderCards [i] = cardsRectTransform [i].GetComponent<OrderCards> ();
			orderCards [i].EspOrder-= 1;
			orderCards [i].EspToRapaOrder-=1;
		}
		for(int i = 0; i < orderCards.Length; i++)
		{
			EspOrder[i]= orderCards [i].EspOrder;
			RapaOrder [i] = orderCards [i].EspToRapaOrder;
		}
		Array.Sort (EspOrder);
		Array.Sort (RapaOrder);
		RectTransform[] tempCardsRect= new RectTransform[cardsRectTransform.Length];
		Lenguages.LenguagesType lenguage = Lenguages.GetActualLenguageType ();


		if (lenguage == Lenguages.LenguagesType.Rapa) 
		{
			//Debug.Log ("RAPA!");

			for (int i = 0; i <RapaOrder.Length; i++) 
			{
				//Debug.Log (RapaOrder[i]);

				for (int j = 0; j < orderCards.Length; j++) 
				{
					
					if (RapaOrder[i] == orderCards [j].EspToRapaOrder) 
					{
						tempCardsRect [i] = cardsRectTransform [j];
						tempCardsRect [i].anchoredPosition = new Vector2(RapaOrder [i] * cardDistance,tempCardsRect [i].anchoredPosition.y);
//						Debug.Log ("Name: "+tempCardsRect[i].name +" Value: "+tempCardsRect[i]);
						//Debug.Log("ENCONTRADA: "+orderCards [j].EspToRapaOrder);
					}


				}
			}
			
		}
		if (lenguage == Lenguages.LenguagesType.Esp) 
		{

			for (int i = 0; i <EspOrder.Length; i++) 
			{
				for (int j = 0; j < orderCards.Length; j++) 
				{

					if (EspOrder[i] == orderCards [j].EspOrder) 
					{
						tempCardsRect [i] = cardsRectTransform [j];
						tempCardsRect [i].anchoredPosition = new Vector2(EspOrder [i] * cardDistance,tempCardsRect [i].anchoredPosition.y);

					}


				}
			}
			
		}

		cardsRectTransform = tempCardsRect;




	
	}
	void Order()
	{
		//se deberan ordear por orden alfabetico
		float [] XpositionCards = new float[cardsRectTransform.Length];




		for (int i = 0; i < cardsRectTransform.Length; i++) 
		{

			XpositionCards[i] =cardsRectTransform[i].anchoredPosition.x;



		}
		Array.Sort (XpositionCards);
		RectTransform[] temp =new RectTransform[ XpositionCards.Length]; 
		for (int i = 0; i < XpositionCards.Length; i++) 
		{
			for (int j = 0; j < cardsRectTransform.Length; j++) 
			{
				if (XpositionCards [i] ==cardsRectTransform [j].anchoredPosition.x) 
				{
					temp [i] = cardsRectTransform [j];
					//Debug.Log (temp[i].name);
				}
			}

		}
		cardsRectTransform = temp;

	}
	
	// Update is called once per frame
	void Update () //comentar algoritmo
	{
		
		
		for (int i = 0; i < cardsRectTransform.Length; i++) 
		{
			distances [i] = Mathf.Abs (center.transform.position.x - cardsRectTransform [i].transform.position.x); //saca la diferencia global entre todos los elementos y el centro 

		}
		float minDistance = Mathf.Min (distances);//obtiene el menor del total de distancias


		for (int i = 0; i < cardsRectTransform.Length; i++) 
		{
			if (minDistance == distances [i]) //si min distance coincide con distances[i] podremos obtener esa posicion para saber cual es la distancia en el arreglo
			{
				minDistanceIndex = i;// el index en el arreglo cardsRectTransform  de la posicion actual más cercana al centro
			}
		}
	
	//	float newX = xVelocity * speed * Time.deltaTime;
	
		// si el drag a terminado y no es un swipe permitir ir a la más cercana
		Debug.Log("MIN index:"+minDistanceIndex);
		/*if (!dragging) 
		{
			Lerping ();
		}*/

	//	scrollPanel.anchoredPosition= new Vector2( Mathf.Clamp (scrollPanel.anchoredPosition.x,minClampX,maxClampX),scrollPanel.anchoredPosition.y);	
	
	}
	/*public ValueChange()
	{
		
	}*/
	private IEnumerator Move()
	{
		//bool start
		onTransition=true;
		//scrollRect.horizontal = false;

		while(interpolationX.LerpTime != interpolationX.CurrentLerpTime)
		{
			interpolationX.UpdateLerp ();
			scrollPanel.anchoredPosition= new Vector2 (interpolationX.InterpolatedValue,scrollPanel.anchoredPosition.y);
			yield return null;
		}

		//abrir la tarjeta actual
	//	scrollRect.horizontal = true;
		CardController.Instance.CardActivator(minDistanceIndex);
		onTransition = false;

	}
	private IEnumerator Move(int index)
	{
		//bool start
		onTransition=true;
		//scrollRect.horizontal = false;

		while(interpolationX.LerpTime != interpolationX.CurrentLerpTime)
		{
			interpolationX.UpdateLerp ();
			scrollPanel.anchoredPosition= new Vector2 (interpolationX.InterpolatedValue,scrollPanel.anchoredPosition.y);
			yield return null;
		}

		//abrir la tarjeta actual
		//	scrollRect.horizontal = true;
		CardController.Instance.CardActivator(index);
		onTransition = false;

	}

	public void StartDrag()
	{
		Debug.Log ("star Drag");
		dragging = true;

	}
	public void EndDrag()
	{
		Debug.Log ("end drag");
		dragging = false;


	}





}
