using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class InputDetector : MonoBehaviour
{

	// Use this for initialization

	public ScrollControler scrollcontroler;
	public ScrollRect detectionArea;

	public float minSwipeLength = 120f;
	public float touchDelayTime=.6f;//esto debe ser igual a la suma de lo que se demora en moverse más el waiting time de cardcontroller

	private float currentTouchDelayTime;
	private bool startWaiting=false;

	public float minTimeTouch = .7f;
	float currentTimeTouch;
	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;

	public enum SwipeDirection { None, Up, Down, Left, Right };
	public static SwipeDirection swipeDirection;

	public enum TypeInput{Mouse,Touch};
	public TypeInput typeInput= TypeInput.Mouse;


	void Start ()
	{
		currentTouchDelayTime = touchDelayTime;
		currentTimeTouch = minTimeTouch;
	}
	
	// Update is called once per frame

	void Update () 
	{
	


		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			Application.Quit ();
		}



		if (!startWaiting) //este tiempo de espera debe mejorar dado que tambien hay que congelar si es se está entre eventos.
		{
			DetectingInput ();
		}

		if (startWaiting) 
		{
			currentTouchDelayTime -= Time.deltaTime;
//			Debug.Log ("CurrentDelay: " + currentTouchDelayTime);
			if (currentTouchDelayTime <= 0) 
			{
				currentTouchDelayTime = touchDelayTime;
				startWaiting = false;
			}
		}
	
		
	}




	void DetectingInput( )
	{
		if (Input.GetKeyUp (KeyCode.LeftArrow)) 
		{
			scrollcontroler.MoveBarStepBackward ();
			startWaiting = true;
		}
		if (Input.GetKeyUp (KeyCode.RightArrow)) 
		{
			scrollcontroler.MoveBarStepFoward ();
			startWaiting = true;	
		}
		switch (typeInput) 
		{
		case TypeInput.Mouse:
		//	if (t.position.x < boundLimits.max.x && t.position.x > boundLimits.min.x && t.position.y < boundLimits.max.y && t.position.y > boundLimits.min.y) 
				
		//	if (Input.mousePosition.x < boundLimits.max.x && Input.mousePosition.x > boundLimits.min.x && Input.mousePosition.y < boundLimits.max.y && Input.mousePosition.y > boundLimits.min.y) 
		//	{
				if (Input.GetMouseButtonDown(0)) 
				{
					firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
					scrollcontroler.StartDrag ();
				}
				if (Input.GetMouseButton(0)) 
				{
						//Input.mousePosition
					if (Input.mousePosition.x > 0) 
					{

					}
					if (Input.mousePosition.x < 0) 
					{

					}
			

						
				}

				if (Input.GetMouseButtonUp(0))
				{
					secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
					currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
					

					// nos aseguramos que el swipe no es un tap y tampoco un drag
				//Debug.Log("swipeMagnitude:"+ currentSwipe.magnitude);
				if (minTimeTouch <= 0 ||currentSwipe.magnitude < minSwipeLength )
					{
						swipeDirection = SwipeDirection.None;
						scrollcontroler.MoveBarToCenter ();
						scrollcontroler.EndDrag ();
						return;
					}
					//pasando este punto es un drag

					currentSwipe.Normalize();
					scrollcontroler.EndDrag ();
					// Swipe up
					if (currentSwipe.y > 0 && currentSwipe.x > -0.5f  && currentSwipe.x < 0.5f) 
					{
						swipeDirection = SwipeDirection.Up;
						Debug.Log("Swipe up");
						// Swipe down
					} else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f &&  currentSwipe.x < 0.5f) 
					{
						swipeDirection = SwipeDirection.Down;
						Debug.Log("Swipe down");
						// Swipe left
					} else if (currentSwipe.x < 0  && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) 
					{
						swipeDirection = SwipeDirection.Left;
						scrollcontroler.MoveBarStepFoward();
						Debug.Log("Swipe left");
						// Swipe right
					} else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f  && currentSwipe.y < 0.5f) 
					{
						swipeDirection = SwipeDirection.Right;
						scrollcontroler.MoveBarStepBackward();
						Debug.Log("Swipe right");
					}
				}

		//	}

			break;

		case TypeInput.Touch:
			
			if (Input.touches.Length > 0) 
			{
				Touch t = Input.GetTouch (0);
			//	if (t.position.x < boundLimits.max.x && t.position.x > boundLimits.min.x && t.position.y < boundLimits.max.y && t.position.y > boundLimits.min.y)
			//	{
				/*if (minTimeTouch > 0)
				{
					minTimeTouch = minTimeTouch - Time.deltaTime;
					minTimeText.text = "timeToSwipe: "+minTimeTouch;
				}*/
				currentTimeTouch -= Time.deltaTime;
			/*	minTimeTouch += Time.deltaTime;
					if(minTimeTouch>8)
					{
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
					}*/
					if (t.phase == TouchPhase.Began) 
					{
						firstPressPos = new Vector2(t.position.x, t.position.y);
						scrollcontroler.StartDrag ();

					}
					if (t.phase == TouchPhase.Moved) 
					{
						


					}

					if (t.phase == TouchPhase.Ended)
					{
						secondPressPos = new Vector2(t.position.x, t.position.y);
						currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

						
					// nos aseguramos que el swipe no es un tap y tampoco un drag
					//Debug.Log("swipeMagnitude:"+ currentSwipe.magnitude);
					/*if (minTimeTouch <= 0 || currentSwipe.magnitude < minSwipeLength || currentSwipe.magnitude > maxSwipeLength )
							swipeDirection = SwipeDirection.None;
							scrollcontroler.EndDrag ();
							scrollcontroler.MoveBarToCenter ();
							minTimeTouch = .5f;
							// drag 
							return;
					*/
					if ( currentTimeTouch  <= 0 ||currentSwipe.magnitude < minSwipeLength 	)
						{
							swipeDirection = SwipeDirection.None;
							scrollcontroler.EndDrag ();
							currentTimeTouch = minTimeTouch;
							//scrollcontroler.MoveBarToCenter ();
							//minTimeTouch = .5f;
							// drag
							return;
						}
						//pasando este punto es un drag
						
						currentSwipe.Normalize();
						scrollcontroler.EndDrag ();
						startWaiting = true;
							// Swipe up
						if (currentSwipe.y > 0 && currentSwipe.x > -0.5f  && currentSwipe.x < 0.5f) 
						{
							swipeDirection = SwipeDirection.Up;
							Debug.Log("Swipe up");
							// Swipe down
						} else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f &&  currentSwipe.x < 0.5f) 
						{
							swipeDirection = SwipeDirection.Down;
							Debug.Log("Swipe down");
							// Swipe left
						} else if (currentSwipe.x < 0  && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) 
						{
							swipeDirection = SwipeDirection.Left;
							scrollcontroler.MoveBarStepFoward();
							Debug.Log("Swipe left");
							// Swipe right
						} else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f  && currentSwipe.y < 0.5f) 
						{
							swipeDirection = SwipeDirection.Right;
							scrollcontroler.MoveBarStepBackward();
							Debug.Log("Swipe right");
						}
					}
				//}

			}


			
			break;
		}

		
	}


}


