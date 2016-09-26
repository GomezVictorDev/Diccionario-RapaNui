using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LoadLevelCamera : MonoBehaviour {

	// Use this for initialization
	public string menuSection;

	// Update is called once per frame
	public void Press ()
	{
		PlayerPrefs.SetString ("Menu", menuSection);
		SceneManager.LoadScene ("Menu");
	
	}
}
