using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisguiseRangeAnimScript : MonoBehaviour {

	private ParticleSystem effect;

	void Awake()
	{
		effect = GameObject.Find ("DisguiseEffect").GetComponent<ParticleSystem> ();
	}


	public void StartParticleEffect()
	{
		effect.Play ();
	}


	public void OpenRange()
	{
		AnimationPlay ("DisguiseOpenRange");
	}


	public void CloseArea()
	{
		AnimationPlay ("DisguiseCloseRange");
	}

	void AnimationPlay(string name)
	{
		GetComponent<Animation> ().Play (name);
	}
}
