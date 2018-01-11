using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilityAnimScript : MonoBehaviour {

	private ParticleSystem effect;

	void Awake()
	{
		effect = GameObject.Find ("InvisEffect").GetComponent<ParticleSystem> ();
	}

	public void StartParticleEffect()
	{
		effect.Play ();
	}

	public void OpenRange()
	{
		AnimationPlay ("InvisibleOpenRangeAnim");
	}

	public void CloseArea()
	{
		AnimationPlay ("InvisibleCloseRangeAnim");
	}


	void AnimationPlay(string name)
	{
		GetComponent<Animation> ().Play (name);
	}
}
