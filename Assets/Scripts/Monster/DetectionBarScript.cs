using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionBarScript : MonoBehaviour {

	private float width = 0f;
	private float maxWidth = 0.6f;
	private Enemy e;
	// Use this for initialization
	void Start () {
		e = transform.parent.parent.GetComponent<Enemy> ();
		Reset ();		
	}
	
	// Update is called once per frame
	void Update () {
		float currentTime = e.DetectionTime();
		float maxTime = e.DetectionMaxTime();

		width = (currentTime * maxWidth) / maxTime;

		transform.localScale = new Vector3 (width, transform.localScale.y,0.1f);
		transform.LookAt (Camera.main.transform.position, Vector3.up);
	}

	public void Reset()
	{
		width = 0;
		transform.localScale = new Vector3 (width,transform.localScale.y,0.1f);
	}
}
