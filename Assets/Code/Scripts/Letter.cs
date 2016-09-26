using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Button))]
public class Letter : InteractiveButton, IPointerUpHandler, IPointerDownHandler {

	//este script permitira viajar al punto donde comienza la letra en el fold por lo que todas las c deberian tener el numero de tarjeta donde comienza la c
	/*[SerializeField]
	float distanceBetweenLetter;
	public int numberLetter;
	float scalePorcentage=0.1f;
	RectTransform rectTransform;*/
	private Text text;
	private bool pointerDown=false;
	public Color startColor;
	//public Color32 colorDisable;
	[SerializeField]
	private bool isInFold = false;
	[SerializeField]
	private int IndexOnFold;
	[SerializeField]
	private ABCButton abcButton;

	/*public void IsInteractive()
	{
		return abcButton.i
	}*/
	void Start () 
	{
	//	rectTransform = GetComponent<RectTransform> ();
	//	Vector2 newSize = new Vector2 (rectTransform.sizeDelta.x - rectTransform.sizeDelta.x * scalePorcentage,
	//		                 rectTransform.sizeDelta.y - rectTransform.sizeDelta.y * scalePorcentage);
	//	rectTransform.sizeDelta = newSize;

		base.Start ();
		if (!isInFold) 
		{
			base.button.interactable = false;
		}
		text = GetComponentInChildren<Text> ();
		text.color = startColor;
		DisableInteractionButton ();

		//tambien desactivar la capcacidad de la tecla para cambiar de color
	
	}
	public void OnPointerUp(PointerEventData data)
	{
		//letra
	//	text.color = colorPresed;

		pointerDown=false;
		
	}
	public bool WasPressed()
	{
		return pointerDown;
	}
	public void OnPointerDown(PointerEventData data)
	{
		//letra
		//	text.color = colorPresed;
	
		pointerDown=true;

	}
	public Color GetStartColor()
	{
		return startColor;
	}
	public void ChangeTextColor(Color color)
	{
		if (base.IsInteractive ()) {
			Debug.Log ("cambia color!!!!!");
			text.color = color;
		}
	}
	public void SetAbcButton(ABCButton abcButton)
	{
		this.abcButton = abcButton;
	}
	
	// Update is called once per frame
	public void GoToIndexFold()
	{
		if (isInFold )
		{
			abcButton.BoxOff ();
			ScrollControler.Instance.MoveBarToIndex (IndexOnFold);

		}
	}
	public void EnabledInteractionButton()
	{
		if (isInFold)
			base.Interactive ();	
	}
	public void DisableInteractionButton()
	{
		if (isInFold)
			base.NonInteractive ();
	}
}
