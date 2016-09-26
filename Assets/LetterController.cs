using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LetterController : MonoBehaviour {

	// Use this for initialization
	public Color pressed;
	private Color initialColor;
	Letter [] textLetters;
	int actualPressed= -1;
	void Start () 
	{
		textLetters = GetComponentsInChildren<Letter> ();
		initialColor = textLetters [1].GetStartColor ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		for(int i=0 ;i< textLetters.Length;i++)
		{
			
			if (textLetters [i].WasPressed() && i != actualPressed) 
			{
				Debug.Log ("actual: "+ actualPressed );
				Debug.Log ("textLetters: "+ i );
				if (actualPressed >= 0) 
				{
					textLetters [actualPressed].ChangeTextColor (initialColor);
					
				}

				textLetters [i].ChangeTextColor (pressed);
				actualPressed = i;
				
			}

		
			
		}

	
	}
}
