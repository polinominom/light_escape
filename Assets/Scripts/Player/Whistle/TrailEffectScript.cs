using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEffectScript : MonoBehaviour 
{
	public void StartAnimation()
	{
//		GetComponentInChildren<Animator> ().Play ();
		GetComponent<Animator>().SetBool("playable", true);
	}

	public void EndAnimations()
	{
		GetComponent<Animator>().SetBool("playable", false);
	}
}
