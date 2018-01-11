using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolishEffectScript : MonoBehaviour {

	private Material mat;
	private float time = 0f;
	private float timeLimit = 0.5f;
	private float startLimit = 0.25f;

	private float radius = 0f;
	private float radiusLimit = 0.3f;

	private bool rangeClosedFinished;

	public PaddleController paddle;
	// Use this for initialization
	void Start () {
		rangeClosedFinished = true;

		mat = GetComponent<SpriteRenderer> ().material;

		time = startLimit;
		mat.SetFloat ("_Timer", 0.5f);


		radius = 0f;
		mat.SetFloat ("_Radius",radius);

	}
		
	public void ActivateEffect(bool val)
	{
		if (val) {
			GetComponent<SpriteRenderer> ().enabled = true;
			StartCoroutine (OpenRange ());
		} else {
			rangeClosedFinished = false;
			StartCoroutine (CloseRange ());
		}
		
	}


	IEnumerator OpenRange()
	{
		while (radius < radiusLimit) 
		{
			float modifier = Time.deltaTime;
			radius += modifier;
			mat.SetFloat ("_Radius",radius);
			yield return null;
		}

	}

	IEnumerator CloseRange()
	{
		while (radius > 0f) 
		{
			float modifier = Time.deltaTime;
			radius -= modifier;
			mat.SetFloat ("_Radius",radius);
			yield return null;
		}

		GetComponent<SpriteRenderer> ().enabled = false;
		rangeClosedFinished = true;
		if (paddle != null)
			paddle.AreaReset ();
	}

	public bool IsRangeClosed()
	{
		return rangeClosedFinished;
	}

}
