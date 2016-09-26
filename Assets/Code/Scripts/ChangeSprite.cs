using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class ChangeSprite : MonoBehaviour {

	// Use this for initialization
	Image image;

	[SerializeField]
	Sprite Rapa,Esp;
	int lenguage;
	void Awake ()
	{
		image=GetComponent<Image> ();


	}

	public void ChangeSpriteItem(int lenguage)
	{
		if (lenguage == 1) 
		{
			image.sprite = Esp;
		}
		if (lenguage == 2) 
		{
			image.sprite = Rapa;
		}
		
	}

}
