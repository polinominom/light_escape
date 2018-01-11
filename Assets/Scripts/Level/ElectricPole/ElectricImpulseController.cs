using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricImpulseController : MonoBehaviour {

	private Material mat;

	private bool electricActive;
	private bool playerCanActivate;
	private bool eyeMode;
	// Use this for initialization

	private GameObject[] lightSources;
	void Start () 
	{
		GetComponentInChildren<SpriteRenderer> ().enabled = false;
		electricActive = true;
		playerCanActivate = false;
		eyeMode = false;

		lightSources= GameObject.FindGameObjectsWithTag("LightSource");


//		Debug.Log ("lightSources: " + lightSources.Length);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (playerCanActivate) 
		{
			if (Input.GetKeyDown (KeyCode.N)) 
			{
				
				eyeMode = !eyeMode;
				ActivateElectricImpulseRange (eyeMode, false);
				Debug.Log ("eyeMode: "+eyeMode);

			}

			if (eyeMode) 
			{
				
				// highlight all the light sources
				if (Input.GetKeyDown (KeyCode.M)) 
				{
					//activate the sound
					GetComponent<AudioSource>().Play();

					//activate the sprite
					GetComponentInChildren<ElectricPoleCooldown>().Reset();
					GetComponentInChildren<ElectricPoleCooldown> ().deactive = true;
					GetComponentInChildren<SpriteRenderer> ().enabled = true;

					if (lightSources != null) 
					{
						//Close The Light Sources
						ActivateElectricImpulseRange(false, true);
					}
				}
			}
		}
	}

	void ActivateElectricImpulseRange(bool val, bool deactivateLight)
	{

		// activate the highlight
		GetComponent<PoleHighlightScript>().ActivateHighlight (val);

		//activate the light sources;
		int limit = lightSources.Length;
		for (int i = 0; i < limit; i++) 
		{
			GameObject o = lightSources [i];
			o.GetComponent<LampHighlightScript> ().ActivateHighlight (val);
			if (deactivateLight) 
			{
				// send the closing signal
				o.GetComponent<LightDetectionScript>().Deactivate();
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Player")) 
		{
//			Debug.Log ("YES: Player Can Activate!");
			playerCanActivate = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag ("Player")) 
		{
//		{Debug.Log ("NO: Player Cannot Activate!");
			playerCanActivate = false;
		}
	}
}
