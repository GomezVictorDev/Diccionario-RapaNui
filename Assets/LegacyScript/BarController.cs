using UnityEngine;
using System.Collections;

public class BarController : MonoBehaviour {

	// Use this for initialization

	public  MoveBarElement[]moveBarElements;
	//private SwipeManager swipeManager;

	int actualElement=0;
	SpriteRenderer spriteR ;
	float distanceBetweenSprite;
	void Start () 
	{
		spriteR = moveBarElements[0].GetComponent<SpriteRenderer> ();
		distanceBetweenSprite = (spriteR.sprite.rect.width * transform.localScale.x) / spriteR.sprite.pixelsPerUnit; //obtenemos la cantidad de espacio en unidades de unity que ocupa el sprite
		// si los elementos tienen diferentes tamaños habrá que crear una funcion para recorrer las distancias a mover a la derecha e izquerda
		//lo que provocara que cada vez que se muevan se re asignen los valores de moveDistance.
		foreach(MoveBarElement mbe in moveBarElements)
		{
			mbe.moveDistance = distanceBetweenSprite;
		}
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (SwipeManager.swipeState == SwipeManager.SwipeState.OnComplete && SwipeManager.swipeDirection== SwipeManager.SwipeDirection.Left) 
		{

			// mover la barra a la izq
			if (actualElement >= 0 && actualElement < moveBarElements.Length -1) 
			{
				actualElement++;
				MoveBarLeft ();
			}
		}

		if (SwipeManager.swipeState == SwipeManager.SwipeState.OnComplete && SwipeManager.swipeDirection == SwipeManager.SwipeDirection.Right) 
		{
			//mover la barra a la derecha
			if (actualElement <= moveBarElements.Length -1 && actualElement > 0) 
			{
				actualElement--;
				MoveBaRight ();

			}

		}
		Debug.Log (actualElement);
	}
	private void MoveBarLeft()
	{
		foreach(MoveBarElement mbe in moveBarElements)
		{
			mbe.MovingLeft();
		}
	}
	private void MoveBaRight()
	{
		foreach(MoveBarElement mbe in moveBarElements)
		{
			mbe.MovingRight();
		}
	}
}
