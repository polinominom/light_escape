using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWhistleController : MonoBehaviour {

	// for whistle meta
	private NavMeshAgent agent;
	private Vector3 whistlePosition;

	private bool pursueWhistle;
	public bool waitWhistle;

	private static float whistlePursueMaxLimit = 2.0f;
	private float whistleWaitTime;


	private bool distractable;

	void Start () {
		distractable = GetComponent<Enemy> ().distractable;

		agent = GetComponent<NavMeshAgent> ();

		whistleWaitTime = 0.0f;

		pursueWhistle = false;
		waitWhistle = false;


	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!distractable)
			return;
		// if the whistle is heard go towards its position
		PursueWhistle ();

		// inspect area (wait for a while and return)
		InspectWhistle();
	}

	void PursueWhistle()
	{
		if (pursueWhistle) 
		{
			// if the destination is reached!
			if (Vector3.Distance (transform.position, agent.destination) <= 0.1f) 
			{
				pursueWhistle = false;
				waitWhistle = true;

			}
		}
	}

	void InspectWhistle()
	{
		if (waitWhistle) 
		{
			whistleWaitTime += Time.deltaTime;
			if (whistleWaitTime >= whistlePursueMaxLimit) 
			{
				//reach the limit! reset all the modifiers
				whistleWaitTime = 0f;
				pursueWhistle = false;
				waitWhistle = false;
				InformBack ();

			}

		}
	}


	public void HearWhistle(Vector3 whistlePosition)
	{
		if (!distractable)
			return;
				
		agent.destination = whistlePosition;
		pursueWhistle = true;
		waitWhistle = false;


		InformBack ();

	}


	void InformBack()
	{
		GetComponent<Enemy> ().SetPursueWhistle (pursueWhistle);
	}
}
