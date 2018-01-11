using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour 
{
	private float time;
	private AudioSource audio;

	void Start()
	{
		time = 0f;
		audio = GetComponent<AudioSource> ();
	}
	void OnCollisionEnter(Collision collision)
	{
		audio.Play ();

		PaddleController paddle = GameObject.Find ("PaddleController").GetComponent<PaddleController> ();
		paddle.RockHit (collision.contacts[0].point);
		StartCoroutine (Hide ());
	}

	IEnumerator Hide()
	{
		while(time < 0.6f)
		{
			time += Time.deltaTime;
			yield return null;
		}

		time = 0f;

		gameObject.SetActive (false);
	}
}
