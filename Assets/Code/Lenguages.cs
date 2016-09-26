using UnityEngine;
using System.Collections;

public static class Lenguages 
{

	// Use this for initialization
	public  enum LenguagesType
	{
		Null,Esp,Rapa
	};
	private static LenguagesType lenguage;

	public static void InitLenguage()
	{
		if(GetActualLenguageType()== LenguagesType.Esp)
		{
			lenguage = LenguagesType.Esp;
		}
		if(GetActualLenguageType()== LenguagesType.Rapa)
		{
			lenguage = LenguagesType.Rapa;
		}
		if(GetActualLenguageType()== LenguagesType.Null)
		{
			ChangeLenguage( LenguagesType.Esp);
		}
	}
	public static LenguagesType GetActualLenguageType()
	{
		if(PlayerPrefs.GetInt("Lenguage") !=0)
		{
			if(PlayerPrefs.GetInt("Lenguage")==1)
			{
				return LenguagesType.Esp;
			}
			if (PlayerPrefs.GetInt ("Lenguage") == 2) 
			{
				return LenguagesType.Rapa;
			}
			return LenguagesType.Null;
		}
		return LenguagesType.Null;
		
	}
	public static void ChangeLenguage(LenguagesType type)
	{
		switch(type)
		{
		case LenguagesType.Esp:
			lenguage = LenguagesType.Esp;
			PlayerPrefs.SetInt("Lenguage",1);
			break;
		case LenguagesType.Rapa:
			lenguage = LenguagesType.Rapa;
			PlayerPrefs.SetInt("Lenguage",2);
			break;



		}
		
	}
}
