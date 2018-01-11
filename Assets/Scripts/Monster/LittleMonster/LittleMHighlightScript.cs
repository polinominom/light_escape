using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleMHighlightScript : MonoBehaviour {

	private Material highLightMap1;
	private Material highLightMap2;
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

		Transform meshes = transform.GetChild (0).GetChild (0).GetChild (1);

		highLightMap1 = meshes.GetChild(0).GetComponent<SkinnedMeshRenderer> ().materials [1];
		highLightMap1.SetColor ("_EmissionColor", deactiveColor);
//		highLightMap1.EnableKeyword ("_EMISSION"); 

		highLightMap2 = meshes.GetChild (1).GetComponent<SkinnedMeshRenderer> ().materials [1];
		highLightMap2.SetColor ("_EmissionColor", deactiveColor);
//		highLightMap2.EnableKeyword ("_EMISSION"); 


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

		highLightMap1.SetColor ("_EmissionColor", emission);
		highLightMap2.SetColor ("_EmissionColor", emission);
	}
}
