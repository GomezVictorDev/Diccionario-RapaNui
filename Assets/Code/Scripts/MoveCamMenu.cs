using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MoveCamMenu : MonoBehaviour {

	// Use this for initialization
	public Vector2 positionToMove;
	Button button;

	void Start () 
	{
		
		button=GetComponent<Button> ();
	}

	private void Update()
	{
		Vector2 camPosition = CamController.Instance.transform.position;

		if (camPosition == positionToMove) 
		{
			//if (button.interactable) 
			//{
				button.interactable = false;
			//}

		}
		if (camPosition != positionToMove) 
		{
			//if (!button.interactable) 
			//{
				button.interactable = true;
			//}

		}
	}
	// Update is called once per frame
	public void Move()
	{
		
			CamController.Instance.GoToPosition (positionToMove, Camera.main.orthographicSize);// esto funcionara correctamente si camcontroller está en la camara principal
			
		
	}
}
