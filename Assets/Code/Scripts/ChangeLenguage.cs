using UnityEngine;
using System.Collections;

public class ChangeLenguage : MonoBehaviour {

	// Use this for initialization

	public  ChangeSprite [] changeSprites;
	//

	void Start ()
	{
		Lenguages.InitLenguage ();
		Debug.Log ("ACTUALLENGUAGE: " + Lenguages.GetActualLenguageType ());
		Debug.Log ("LenguageValue: " + PlayerPrefs.GetInt ("Lenguage"));
		Change ();

	}
	
	// Update is called once per frame
	public void Change ()
	{
		
		//Lenguages.ChangeLenguage (lenguage);
		if (changeSprites.Length > 0) 
		{
			foreach (ChangeSprite changeSprite in changeSprites) 
			{
				changeSprite.ChangeSpriteItem (PlayerPrefs.GetInt("Lenguage"));
			}
		}

	
	}
	public void ChangeRapa ()
	{

		Lenguages.ChangeLenguage (Lenguages.LenguagesType.Rapa);
		if (changeSprites.Length > 0) 
		{
			foreach (ChangeSprite changeSprite in changeSprites) 
			{
				changeSprite.ChangeSpriteItem (PlayerPrefs.GetInt("Lenguage"));
			}
		}


	}
	public void ChangeEsp ()
	{

		Lenguages.ChangeLenguage (Lenguages.LenguagesType.Esp);
		if (changeSprites.Length > 0) 
		{
			foreach (ChangeSprite changeSprite in changeSprites) 
			{
				changeSprite.ChangeSpriteItem (PlayerPrefs.GetInt("Lenguage"));
			}
		}


	}
}
