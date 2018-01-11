using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhistleController : MonoBehaviour {

	public float closeRange;
	public float range;

	private static float fixThikness = 0.005f;
	private static float radiusModifier = 0.006f;
	private static float startRadius = 0.01f;

	private bool activateRange;

	private List<GameObject> enemiesWithinRange;

	private Material whistleMat;

	private UIScript ui;

	private WhistleCollider collider;

	private SpriteRenderer renderer;
	private WhistleRangeArea whistleRangeArea;

	private TrailEffectScript[] trailEffects;
	private SkillUtil skills;

	void Awake()
	{
		ui = GameObject.FindGameObjectWithTag ("UI").GetComponent<UIScript> ();
		skills = transform.parent.parent.GetComponent<SkillUtil> ();
	}

	void Start () 
	{
		renderer = GetComponent<SpriteRenderer> ();

		whistleRangeArea = GetComponentInChildren<WhistleRangeArea> ();



		// initialize material
		whistleMat = renderer.material;

		whistleMat.SetFloat ("_Thickness", fixThikness);
		whistleMat.SetFloat ("_Radius", 0f);

		activateRange = false;

		collider = transform.parent.GetComponentInChildren<WhistleCollider> ();

		//initialize the enemy list
		enemiesWithinRange = new List<GameObject> ();


		renderer.enabled = false;

		trailEffects = transform.parent.GetChild (2).GetComponentsInChildren<TrailEffectScript> ();
	}
	
	void Update () 
	{
		if (skills.whistleClicked && ui.whistleCount != 0) 
		{
			skills.whistleClicked = false;
			activateRange = !activateRange;
			ActivateWhistleRange (activateRange);
		}

		if (activateRange) 
		{
			if (skills.whistleActivateClicked) 
			{
				skills.whistleActivateClicked = false;
				//Play the animations
				foreach (TrailEffectScript t in trailEffects) {
					t.StartAnimation ();
				}

				// activate the sound
				GetComponent<AudioSource> ().Play ();

				//decrease the whistleMat count
				ui.whistleCount -= 1;
				if (ui.whistleCount == 0) {
					ui.PermaDisableSkill ("Whistle");
				}

				// distract enemies;
				if (enemiesWithinRange != null) {
					activateRange = false;

					foreach (GameObject enemy in enemiesWithinRange) {
						Vector3 h = (enemy.transform.position - transform.parent.position).normalized * closeRange;
						enemy.GetComponent<EnemyWhistleController> ().HearWhistle (transform.parent.position + h);
					}
				}

				activateRange = false;
				ActivateWhistleRange (false);
			} 
			else if (skills.whistleCanceled) 
			{
				skills.whistleCanceled = false;
				activateRange = false;
				ActivateWhistleRange (activateRange);
			}
		}
	}

	void ActivateWhistleRange(bool val)
	{
		float radius = 0f;
		if (val)
		{
			// update the range radius
			radius += startRadius + range * radiusModifier;
			whistleMat.SetFloat ("_Radius", radius);

		} 
		else 
		{
			whistleMat.SetFloat ("_Radius", 0f);
		}

		renderer.enabled = val;
		whistleRangeArea.ActivateRange (val);

		HighLightEnemies (val);

	}

	void HighLightEnemies(bool val)
	{
		if (enemiesWithinRange == null)
			return;
		
		foreach(GameObject enemy in enemiesWithinRange)
		{
			enemy.GetComponent<Enemy> ().ActivateHighlight (val);
		}
	}

	public void AddEnemy(GameObject enemy)
	{
		bool val = true;
		enemiesWithinRange.Add (enemy);
		if (activateRange) 
		{
			enemy.GetComponent<Enemy> ().ActivateHighlight (val);
		}
			
	}

	public void RemoveEnemy(GameObject enemy)
	{
		bool val = false;
		enemiesWithinRange.Remove (enemy);
		if (activateRange) 
		{
			enemy.GetComponent<Enemy> ().ActivateHighlight (val);
		}
	}

}
