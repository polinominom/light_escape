using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockRangeAreaScript : MonoBehaviour {

	public float radius;
	public float limit;

	private bool animPlaying;
	private bool canPlayClosing;

	private int state = 0;
	private Material mat;

	private SpriteRenderer sRenderer;
	private SpriteRenderer sRenderer2;
	// Use this for initialization
	void Start () 
	{
		animPlaying = false;
		state = 0;

		mat = GetComponent<SpriteRenderer> ().material;
		mat.SetFloat ("_Radius", 0f);
		mat.SetFloat ("_Limit", 0f);

		sRenderer = GetComponent<SpriteRenderer> ();
		sRenderer2 = transform.GetChild (0).GetComponent<SpriteRenderer> ();
		sRenderer.enabled = false;
		sRenderer2.enabled = false;
	}

	public void ActivateRange(bool val)
	{
		if (val && !animPlaying) 
		{
			AnimationPlay ("RockAreaOpen");
			animPlaying = true;

		} else if (!val && !animPlaying) 
		{
			AnimationPlay ("RockAreaClose");
			animPlaying = true;
		} else {
			Debug.Log ("SOMETHING WRONG: v:" + val + " anim: "+animPlaying);
		}
	}


	public void ClosingAnimEnd()
	{
		animPlaying = false;
		state = 0;
	}

	public void OpeningAnimEnd()
	{
		animPlaying = false;
		state = 1;
	}

	void AnimationPlay(string name)
	{
		GetComponent<Animation> ().Play (name);
	}
}
