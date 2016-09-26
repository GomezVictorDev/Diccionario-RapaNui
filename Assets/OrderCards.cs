using UnityEngine;
using System.Collections;

public class OrderCards : MonoBehaviour {

	// Use this for initialization
	public int EspOrder;
	public int EspToRapaOrder;

	private ChangeSprite[] changeSprite = new ChangeSprite[2];

	private RectTransform rectTransform;
	void Start () 
	{
		rectTransform = GetComponent<RectTransform> ();
		for (int i = 0; i < transform.childCount; i++) 
		{
			if (transform.GetChild (i).tag.Equals ("SideA"))
				changeSprite[0]=transform.GetChild (i).GetComponent<ChangeSprite> ();

			if(transform.GetChild(i).tag.Equals("SideB"))
				changeSprite[1]=transform.GetChild (i).GetComponent<ChangeSprite> ();
		}
		changeSprite[0].ChangeSpriteItem (PlayerPrefs.GetInt("Lenguage"));
		changeSprite[1].ChangeSpriteItem (PlayerPrefs.GetInt("Lenguage"));
//	
	//	EspOrder= EspOrder-1;
	//	EspToRapaOrder = EspToRapaOrder - 1;
	

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

}
