using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightScript : MonoBehaviour {

	private Material highLightMap;
	private static float red = 0.03970589f;
	private static float green = 0.3913389f;
	private static float blue = 0.6f;
	private static Color emissionColor = new Color(red, green, blue);
	private static Color indistractableColor= Color.red;
	private static Color deactiveColor = new Color(0f,0f,0f);

	private bool distractable;

	void Start () 
	{
		distractable = GetComponent<Enemy> ().distractable;
		 // mesh renderer for monster in transform.GetChild (0).GetChild (1);

		highLightMap = transform.GetChild (0).GetChild (1).GetComponent<SkinnedMeshRenderer> ().materials [1];
		highLightMap.SetColor ("_EmissionColor", deactiveColor);
						

	}

	public void ActivateHighlight(bool val)
	{
		Color emission;
		if (val) {
			if (distractable) {
				emission = emissionColor;
			} else {
				emission = indistractableColor;
			}
		} else
			emission = deactiveColor;

		//DynamicGI.SetEmissive(highLightMap, emission);
		highLightMap.SetColor ("_EmissionColor", emission);

//		Debug.Log ("enemy color: " + emission);
		
	}

}
