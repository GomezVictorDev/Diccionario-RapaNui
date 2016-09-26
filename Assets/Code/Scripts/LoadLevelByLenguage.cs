using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LoadLevelByLenguage : MonoBehaviour {

	// Use this for initialization

	public string levelName;

	void Start () 
	{
	
	}
	

	public void LoadLevel()
	{
		
			
				SceneManager.LoadScene (levelName);


		
	}
}
