using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhistleRangeArea : MonoBehaviour {

	public float radius;
	public float limit;

	private bool animPlaying;
	private bool canPlayClosing;

	private int state = 0;
	private Material mat;

	// Use this for initialization
	void Start () 
	{
		animPlaying = false;
		state = 0;

		mat = GetComponent<SpriteRenderer> ().material;
		mat.SetFloat ("_Radius", 0f);
		mat.SetFloat ("_Limit", 0f);

		GetComponent<SpriteRenderer> ().enabled = false;
	}

	public void ActivateRange(bool val)
	{
		if (val && !animPlaying) 
		{
			AnimataionPlay ("WhistleRange");
			animPlaying = true;

		} 
		else if(!val && !animPlaying)
		{
			AnimataionPlay ("WhistleRangeBackwards");
			animPlaying = true;
		}
	}
		

	public void ClosingAnimEnd()
	{
		animPlaying = false;
		state = 0;

		mat.SetFloat ("_Radius", 0f);
		mat.SetFloat ("_Limit", 0f);
	}

	public void OpeningAnimEnd()
	{
		animPlaying = false;
		state = 1;

		mat.SetFloat ("_Radius", radius);
		mat.SetFloat ("_Limit", limit);
	}

	public void AnimataionPlay(string name)
	{
		GetComponent<Animation> ().Play (name);
	}

}
