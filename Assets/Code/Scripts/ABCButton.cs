using UnityEngine;
using System.Collections;

public class ABCButton : MonoBehaviour {

	// Use this for initialization
	public GameObject ESP_ABC;
	public GameObject Rapa_ABC;

	private static Animator letterContainer;
	private static Animator [] lettersAnimator;
	private static Letter[] letters;

	private static GameObject abcContainer;
	Delay delay;
	void Start () 
	{
		delay = new Delay (1);
		if (gameObject.tag.Equals("ABCBUTTON")) 
		{
			
		
		Lenguages.LenguagesType lenguage = Lenguages.GetActualLenguageType ();
		GameObject abcContainer = new GameObject();
		if (lenguage == Lenguages.LenguagesType.Esp)
		{
				abcContainer =(GameObject) Instantiate (ESP_ABC);
		}
		if (lenguage == Lenguages.LenguagesType.Rapa) 
		{
				abcContainer =(GameObject) Instantiate (Rapa_ABC);
		}

			letterContainer = abcContainer.GetComponent<Animator> ();
			lettersAnimator = new Animator[abcContainer.transform.childCount];
			letters= new Letter[abcContainer.transform.childCount];
			for (int i = 0; i < abcContainer.transform.childCount; i++) 
		{
				lettersAnimator [i] = abcContainer.transform.GetChild (i).GetComponent<Animator> ();
				letters[i]= abcContainer.transform.GetChild (i).GetComponent<Letter> ();
			letters [i].SetAbcButton (this);
		}
			abcContainer.transform.parent = gameObject.transform.parent;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		delay.Update ();
	}

	public void Press()
	{
		//podemos obtener el boton y desabilitar su interactividad mientras delayend sea falso	
		//letterContainer
		if(delay.DelayEnd())
		{
			if (letterContainer.GetBool ("Open")) 
			{
				BoxOff ();
			} else
			{
				BoxOn ();
			}
			delay.StartDelay();
		}

	}
	private void BoxOn()
	{
		letterContainer.SetBool ("Open", true);
		foreach(Animator letter in lettersAnimator)
		{
			letter.SetBool ("Open", true);
		}

	}
	public void BoxOff()
	{
		letterContainer.SetBool ("Open", false);
		foreach(Animator letter in lettersAnimator)
		{
			letter.SetBool ("Open", false);
		}
	}
}
