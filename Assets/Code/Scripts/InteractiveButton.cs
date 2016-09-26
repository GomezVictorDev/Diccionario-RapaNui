using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class InteractiveButton : MonoBehaviour {

	// Use this for initialization
	//[SerializeField]
	protected Button button;
	protected void Start () 
	{
		if (button == null) 
		{
			button = GetComponent<Button> ();
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public bool IsInteractive()
	{
		return button.interactable;
	}
	public void Interactive()
	{
		
			button.interactable = true;
	}
	public void NonInteractive()
	{
		
			button.interactable = false;
	}
}
