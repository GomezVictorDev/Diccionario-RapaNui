using UnityEngine;



public class SwipeManager : MonoBehaviour//tambien probar con un modelo mas de drag que de swipe.	
{
	public float minSwipeLength = 200f;
	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;
	public enum SwipeDirection { None, Up, Down, Left, Right };
	public enum SwipeInput{Finger,Mouse};
	public enum SwipeState{None,OnStart,OnComplete,OnInterrump}

	public static SwipeDirection swipeDirection;
	public SwipeInput swipeInput;
	public static SwipeState swipeState;
	void Update ()
	{
		DetectSwipe();

	}

	public void DetectSwipe ()
	{
		swipeState=SwipeState.None;
		switch(swipeInput)
		{
		case SwipeInput.Finger:
			
			if (Input.touches.Length > 0) {
				Touch t = Input.GetTouch(0);

				if (t.phase == TouchPhase.Began) {
					firstPressPos = new Vector2(t.position.x, t.position.y);
				}

				if (t.phase == TouchPhase.Ended) {
					secondPressPos = new Vector2(t.position.x, t.position.y);
					currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

					// Make sure it was a legit swipe, not a tap
					if (currentSwipe.magnitude < minSwipeLength) {
						swipeDirection = SwipeDirection.None;
						return;
					}

					currentSwipe.Normalize();

					// Swipe up
					if (currentSwipe.y > 0 && currentSwipe.x > -0.5f  && currentSwipe.x < 0.5f) {
						swipeDirection = SwipeDirection.Up;
						// Swipe down
					} else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f &&  currentSwipe.x < 0.5f) {
						swipeDirection = SwipeDirection.Down;
						// Swipe left
					} else if (currentSwipe.x < 0  && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
						swipeDirection = SwipeDirection.Left;
						// Swipe right
					} else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f  && currentSwipe.y < 0.5f) {
						swipeDirection = SwipeDirection.Right;
					}
				}
			} else {
				swipeDirection = SwipeDirection.None;   
			}
			break;
		case SwipeInput.Mouse:
			
			if (Input.GetMouseButtonDown (0)) {
				Vector3 mousePos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10);
				firstPressPos = Camera.main.ScreenToWorldPoint (mousePos);
				swipeState = SwipeState.OnStart;

			}
			if (Input.GetMouseButtonUp (0))
			{
				Vector3 mousePos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10);
				secondPressPos=Camera.main.ScreenToWorldPoint (mousePos);
				currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
				Debug.Log ("current magnitude: "+currentSwipe.magnitude);
				swipeState = SwipeState.OnComplete;

				if (currentSwipe.magnitude < minSwipeLength) {

					swipeDirection = SwipeDirection.None;
					swipeState = SwipeState.OnInterrump;
					return;
				
				} else 
				{
					currentSwipe.Normalize();

					if (currentSwipe.y > 0 && currentSwipe.x > -0.5f  && currentSwipe.x < 0.5f) {
						swipeDirection = SwipeDirection.Up;
						// Swipe down
					} else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f &&  currentSwipe.x < 0.5f) {
						swipeDirection = SwipeDirection.Down;
						// Swipe left
					} else if (currentSwipe.x < 0  && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
						swipeDirection = SwipeDirection.Left;
						// Swipe right
					} else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f  && currentSwipe.y < 0.5f) {
						swipeDirection = SwipeDirection.Right;
					}


				}

			}


			break;

		}
		Debug.Log ("Swipe: " + swipeDirection);
	}
}
