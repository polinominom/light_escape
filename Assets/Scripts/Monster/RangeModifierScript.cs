using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeModifierScript : MonoBehaviour {

	private OutlineManager outlineManager;
	// min and max range float static range modifiers for shader
	private static float minRangeThicknessModifier = 0.0125f;
	private static float maxRangeThicknessModifier = 0.006f;
	private static float maxRangeRadiusModifier = 0.009375f;


	//shoow or hide ranges
	private bool rangesVisible = false;

	// time limit for visibility of ranges the 
	private static float visibilityTimeLimit = 3f;
	private float visibilityTimePassed = 0f;

	void Awake()
	{
		outlineManager = GameObject.Find ("OutlineManager").GetComponent<OutlineManager> ();
	}

	void Start () 
	{
		float min = GetComponent<Enemy> ().rangeValue;

		float minThick = minRangeThicknessModifier * min;
		float maxThick = maxRangeThicknessModifier * min;
		float maxRadius = maxRangeRadiusModifier * min;

		// min range
		transform.GetChild(1).GetComponent<SpriteRenderer>().material.SetFloat("_Thickness", minThick);

		//max range
		transform.GetChild(2).GetComponent<SpriteRenderer>().material.SetFloat("_Thickness", maxThick);
		transform.GetChild(2).GetComponent<SpriteRenderer>().material.SetFloat("_Radius", maxRadius);

		HideRangeSprites (true);

		Outline (false);

	}
	void Update()
	{
		if (rangesVisible) 
		{
			visibilityTimePassed += Time.deltaTime;
			if (visibilityTimePassed >= visibilityTimeLimit) {
				rangesVisible = false;
				visibilityTimePassed = 0f;
				HideRangeSprites (true);
				Outline (false);
			}
		}
	}

	public void activateRangeSprites()
	{
		visibilityTimePassed = 0f;
		rangesVisible = true;
		HideRangeSprites (false);
		Outline (true);
	}

	void HideRangeSprites(bool val)
	{
		transform.GetChild (1).gameObject.SetActive(!val);
		transform.GetChild (2).gameObject.SetActive(!val);
	}

	void Outline(bool val)
	{
		Material mat = val ? outlineManager.outlinemat : outlineManager.normalmat;
		Debug.Log ("mat: " + mat);

		Material[] mats = new Material[2];
		mats [0] = mat;
		mats [1] = transform.GetChild (0).GetChild (1).GetComponent<SkinnedMeshRenderer> ().materials [1];

		transform.GetChild (0).GetChild (1).GetComponent<SkinnedMeshRenderer> ().materials = mats;
	}

}
