using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class Locucion : MonoBehaviour {


	private AudioClip clip;
	private AudioSource audioSource;

	public void SetClip(AudioClip clip)
	{
		this.clip = clip;
		audioSource.clip = this.clip;
	}
	void Start () 
	{
		audioSource = GetComponent<AudioSource> ();
	
	}
	


	public void Play()
	{
		if(!audioSource.isPlaying && clip!=null)
		audioSource.Play ();
		
	}

	public void Stop()
	{
		if(audioSource.isPlaying  && clip!=null)
		audioSource.Stop ();
	}


}
