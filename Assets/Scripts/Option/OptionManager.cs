using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour {

	/*
	 * SelectedButtons 
	 * 0: Whistle
	 * 1: RockThrow
	 * 2: Teleport
	 * 3: Invisible
	 * 4: Disguise
	*/
	public int[] selectedButtons;

	void Awake()
	{
		selectedButtons = new int[3];
		selectedButtons [0] = 0;
		selectedButtons [1] = 1;
		selectedButtons [2] = 2;		
	}

	void Start () 
	{
		

	}

	public void DiffucltyChanged(string val)
	{

		if (val.Equals ("Easy")) 
		{
			
		} 
		else if (val.Equals ("Hard")) 
		{
			
		}
	}

}
