using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleHighlightScript : MonoBehaviour {

	private Material mat;

	private static float red = 0.03970589f;
	private static float green = 0.3913389f;
	private static float blue = 0.6f;
	private static Color emissionColor = new Color(red, green, blue);
	// Use this for initialization
	void Start () 
	{
		
		mat = GetComponentInChildren<MeshRenderer> ().material;
//		Debug.Log ("PoleHighlightScript: mat:" + mat);
		mat.SetColor ("_EmissionColor", Color.black);
	}


	public void ActivateHighlight(bool val)
	{
		Color emission = val ? emissionColor: Color.black;

		mat.SetColor ("_EmissionColor", emission);
//		Debug.Log ("PoleHighlightScript: mat:" + mat+" val: "+val);

	}


}
