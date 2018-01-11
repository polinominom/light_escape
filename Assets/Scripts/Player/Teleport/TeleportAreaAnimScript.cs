using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAreaAnimScript : MonoBehaviour {

	public void StartParticleEffect()
	{
		transform.GetChild (1).GetComponent<ParticleSystem> ().Play ();
	}

	public void OpenRange()
	{
		AnimationPlay ("TeleportOpenArea");
	}

	public void CloseArea()
	{
		AnimationPlay ("TeleportCloseArea");
	}


	void AnimationPlay(string name)
	{
		GetComponent<Animation> ().Play (name);
	}
}
