using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarController : MonoBehaviour {

	private static float timeLimit = 2f;

	private Material mat;
	private bool isRadarActivated = false;
	private float timePassed = 0.0f;
	private float timeForReactive = 0f;

	private SkillUtil skills;


	void Start()
	{
		mat = GetComponent<SpriteRenderer> ().material;
		mat.SetFloat ("_Radar", 1.0f);

		skills = GetComponentInParent <SkillUtil>();
	}

	void Update()
	{
		if (skills.radarClicked && !isRadarActivated) 
		{
			isRadarActivated = true;
			mat.SetFloat ("_Radar", 0.0f);

			//TODO: move it to GameRadarController or WorldRadarController
			SetEnemyAura(true); // on

			//Play the radar sound
			GetComponent<AudioSource>().Play();


		}

		if (isRadarActivated) 
		{
			timePassed += Time.deltaTime/10.0f;
			timeForReactive += Time.deltaTime;

			if (timeForReactive >= timeLimit) 
			{
				//reset radar
				timeForReactive = 0f;
				timePassed = 0.0f;
				isRadarActivated = false;
				mat.SetFloat ("_TimeControl", 0.0f);
				mat.SetFloat ("_Radar", 1.0f);

				skills.radarClicked = false;
			}
			else
			{
				mat.SetFloat ("_TimeControl", timePassed);
			}
		}
	}


	void SetEnemyAura(bool val)
	{

		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		int length = enemies.Length;
		for (int i = 0; i < length; i++) 
		{
			RangeModifierScript rangeModify = enemies [i].GetComponent<RangeModifierScript> ();
			if (rangeModify != null)
				rangeModify.activateRangeSprites ();
			else
				enemies [i].GetComponent<LittleMRangeModifier> ().activateRangeSprites();
			//enemies [i].GetComponent<HighlightScript>().ActivateHighlight(val);

		}		
	}
}
