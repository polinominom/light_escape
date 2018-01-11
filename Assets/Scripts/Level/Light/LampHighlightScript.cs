using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampHighlightScript : MonoBehaviour {

	private Material highLightMap;
	private static float red = 0.03970589f;
	private static float green = 0.3913389f;
	private static float blue = 0.6f;
	private static Color emissionColor = new Color(red, green, blue);
	private static Color deactiveColor = new Color(0f,0f,0f);

	void Start () 
	{
		// mesh renderer for light source in transform.parent.parent;
		highLightMap = transform.parent.parent.GetComponent<MeshRenderer> ().material;
		highLightMap.SetColor ("_EmissionColor", deactiveColor);
	}

	public void ActivateHighlight(bool val)
	{
		Color emission = val ? emissionColor: deactiveColor;

//		DynamicGI.SetEmissive(highLightMap, emission);
		highLightMap.SetColor ("_EmissionColor", emission);

	}
}
