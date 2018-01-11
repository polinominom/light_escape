using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPoleCooldown : MonoBehaviour 
{

	private float width = 0f;
	private float maxWidth = 0.6f;

	public bool deactive;
	private float deactiveTime;
	private static float deactiveMaxTime = 2.0f;

	// Use this for initialization
	void Start () {
		deactive = false;
		Reset ();		
	}

	// Update is called once per frame
	void Update () {
		if (deactive) {
			deactiveTime += Time.deltaTime;
			if (deactiveTime >= deactiveMaxTime) {
				deactive = false;
				deactiveTime = 0f;
				Reset ();
				transform.parent.GetComponent<SpriteRenderer> ().enabled = false;
			} else {
				width = (deactiveTime * maxWidth) / deactiveMaxTime;
				transform.localScale = new Vector3 (width, transform.localScale.y,0.1f);
			}

			
		}

	}

	public void Reset()
	{
		width = 0;
		transform.localScale = new Vector3 (width,transform.localScale.y,0.1f);
	}
}
