using UnityEngine;
using System.Collections;

public class CamPositionStart : MonoBehaviour {

	// Use this for initialization
	private Vector3 positionHome,positionAmbitos;
	void Start () 
	{
		positionHome = new Vector3 (-7f, 4.47f,transform.position.z);
		positionAmbitos = new Vector3 (-7f, -4.47f,transform.position.z);
		if (PlayerPrefs.GetString("Menu")!= "") 
		{
			string menu = PlayerPrefs.GetString ("Menu");
			if (menu.Equals ("Home")) 
			{
				transform.position = positionHome;
			}
			if (menu.Equals ("Ambitos")) 
			{
				transform.position = positionAmbitos;
			}
			PlayerPrefs.DeleteKey ("Menu");
		}
	
	}
	

}
