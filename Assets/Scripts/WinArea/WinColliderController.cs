using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinColliderController : MonoBehaviour {

	private UIScript ui;
	void Start()
	{
		ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIScript>();
	}
	// Use this for initialization
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Player")) 
		{
			GetComponent<AudioSource> ().Play ();

			ui.playAnimation = false;
			ui.isGameOver = true;

			PlayerController p = other.GetComponentInParent<PlayerController> ();
			p.dead = true;
			p.AnimationPlay ("kick");
			StartCoroutine (win());

		}
	}

	IEnumerator win()
	{
		yield return new WaitForSeconds (1f);

		ui.StartCoroutine (ui.YouWinAnimation ());
	}
		
}
