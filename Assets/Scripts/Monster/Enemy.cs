using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour 
{
	public float speed = 7f;

	private GameObject player;
	private PlayerController playerController;

	private ShaderCameraScript camShader;

	private Vector3 startPosition;
	private NavMeshAgent agent;
	private Transform minRange;
	private Transform maxRange;

	public float rangeValue;
	private float minRangeValue;
	private float maxRangeValue;

	[Range(0.0f, 360.0f)]
	public float rangeAngle;

	private bool detected;
	private float detectionTime;
	public float detectionMaxTime;


	private float interestLostTime;
	public float interestLostMaxTime;

	private bool playerEnteredLight;

	private bool isStartPosition;

	private bool pursueWhistle;

	private UIScript ui;

	public bool distractable;

	private bool playerInMax;
	private bool playerInMin;

	private string anim_name = "";
	private bool attacking;

	public string enemyName;
	private bool isLittleMonster;

	private AudioSource attackAudio;
	private AudioSource followAudio;
	private AudioSource[] audios;
	private bool[] audioStates;

	void Awake()
	{
		// set ui
		ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIScript>();

		//set the player
		player = GameObject.FindGameObjectWithTag ("PlayerNode");
		playerController = player.GetComponent<PlayerController> ();

		camShader = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<ShaderCameraScript> ();

	}
	void Start ()
	{
		attacking = false;

		anim_name = AnimationNamesUtil.GetAnimName(enemyName, "idle");
		//set the ui

		agent = GetComponent<NavMeshAgent> ();

		startPosition = transform.position;

		// set danger sprite hidden
		HideDangerSprite (true);
		HideDetectionLostSprite (true);

		// get the max and min range transforms
		minRangeValue = rangeValue;
		maxRangeValue = rangeValue * 2;
		 
		//set the time
		detectionTime = 0f;
		interestLostTime = 0f;

		detected = false;
		playerEnteredLight = false;

		isStartPosition = true;

		pursueWhistle = false;

		playerInMax = false;
		playerInMin = false;

		isLittleMonster = enemyName.Equals ("LittleMonster") ? true : false;

		agent.speed = this.speed;

		audios = GetComponents<AudioSource> ();
		audioStates = new bool[audios.Length];

		for(int i = 0; i<audios.Length; i++)
		{
			audioStates [i] = true;
		}
	}
	
	void Update ()
	{
		if (ui.isGameOver || attacking)
			return;

		anim_name = AnimationNamesUtil.GetAnimName(enemyName, "idle");
		//try to detect, or lost
		detectionMeta();

		// move towards detected player
		// also handles the returning back situation if the player is lost!
		moveTowardsDetectedPlayer();


		AnimationPlay (anim_name);
			
	}
		
	//Tries to detect the player with detection time. Cooldowns also handled here!
	void detectionMeta()
	{
		// check all possible situations
		if (playerInMin) 
		{
			// player in min
			detectPlayer();
		}

		if (!playerInMin && playerInMax) 
		{
			if (playerEnteredLight) 
			{
				detectPlayer ();	
			}
			else
			{
				losePlayer ();
			}

		}

		if (!playerInMax) 
		{
			losePlayer ();
		}		
	}

	//Tries to move towards player if its detected
	//or if the enemy lost the player,it tries to return back to its post
	void moveTowardsDetectedPlayer()
	{
		
		bool disguisedDistract = playerController.disguised && distractable;
		bool invisibleDistract = playerController.invisible;
		if (detected && (!disguisedDistract && !invisibleDistract))
		{
			PlaySound (0);

			agent.destination = player.transform.position;

			//play the walk anim
			anim_name = AnimationNamesUtil.GetAnimName(enemyName, "walk");
		} 
		else 
		{
			bool wait = GetComponent<EnemyWhistleController> ().waitWhistle;
			// try to go back
			if (!isStartPosition && !pursueWhistle && !wait) {
				agent.destination = startPosition;
				anim_name = AnimationNamesUtil.GetAnimName(enemyName, "walk");
			}

			else if (pursueWhistle && !wait) {
				anim_name = AnimationNamesUtil.GetAnimName(enemyName, "walk");
			}
			else if (wait) {
				anim_name = AnimationNamesUtil.GetAnimName(enemyName, "idle");
			}
		}

		if (Vector3.Distance (startPosition, transform.position) <= 0.1f && !pursueWhistle) 
		{
			// reset the situation that the monster has been doing
			isStartPosition = true;

		} 
		else 
		{
			isStartPosition = false;
		}		
	}


	//Tries to wait the detecting process.
	void detectPlayer()
	{
		bool disguisedDistract = playerController.disguised && distractable;
		bool invisibleDistract = playerController.invisible;
		if (disguisedDistract || invisibleDistract) 
		{
			PlayerLost ();
			return;
		}

		if ((detectionTime < detectionMaxTime) ) 
		{
			HideDangerSprite (false);
			HideDetectionLostSprite (true);
			detectionTime += 0.05f;

			PlaySound (2);
				

			//set the camera effect that triggers shader
			camShader.startDetectionEffect(detectionTime, detectionMaxTime);
		} 
		else 
		{
			// PLAYER IS DETECTED FOLLOW IT
			HideDangerSprite(true);
			HideDetectionLostSprite (true);
			detected = true;
			interestLostTime = 0f;

		}		
	}

	//Tries to wait for monster to lose its interest
	void losePlayer()
	{
		if (detected) {
			if (interestLostTime < interestLostMaxTime) {
				HideDangerSprite (true);
				HideDetectionLostSprite (false);
				interestLostTime += 0.05f;

				//set the camera effect that triggers shader
				camShader.startLostInterestEffect(interestLostTime, interestLostMaxTime);

			} else {
				PlayerLost ();
			}			
		} else {
			// the player got out before detected!
			detectionTime = 0f;
			HideDangerSprite (true);
			HideDetectionLostSprite (true);
		}

	}

	void PlayerLost()
	{
		// player have been lost
		HideDangerSprite (true);
		HideDetectionLostSprite (true);
		detected = false;
		detectionTime = 0f;

		for (int i = 0; i < audios.Length; i++) 
		{
			audioStates [i] = true;
		}		
	}

	void LittleMonsterRangeControl()
	{
//		// the forward vector
//		Vector3 f = transform.forward;
//
//		// the direction of player's pos to this enemy
//		Vector3 direction = player.transform.position - transform.position;
//
//		// find angle
//		float angle = Vector3.Angle (f, direction);
//
//		// check if the player is out
//		if ((playerInMax && angle > rangeAngle) || (playerInMin && angle > rangeAngle)) 
//		{
//			playerInMax = false;
//			playerInMin = false;
//		}
	}



	void HideDangerSprite(bool val)
	{
		transform.GetChild (3).gameObject.SetActive (!val);
		transform.GetChild (3).GetComponent<DangerSpriteScript> ().Reset();
		transform.GetChild (3).GetComponentInChildren<DetectionBarScript> ().Reset ();
	}

	void HideDetectionLostSprite(bool val)
	{
		transform.GetChild (4).gameObject.SetActive (!val);
		transform.GetChild (4).GetComponent<DangerSpriteScript> ().Reset();
		transform.GetChild (4).GetComponentInChildren<DetectionLostBarScript> ().Reset ();
	}

	public void PlayerEnteredLight()
	{
		playerEnteredLight = true;
	}

	public void PlayerExitedLight()
	{
		playerEnteredLight = false;
	}


	public float DetectionTime()
	{
		return detectionTime;
	}

	public float DetectionMaxTime()
	{
		return detectionMaxTime;
	}

	public float InterestLostTime()
	{
		return interestLostTime;
	}

	public float InterestLostMaxTime()
	{
		return interestLostMaxTime;
	}

	public void SetPursueWhistle(bool pursueWhistle)
	{
		
		this.pursueWhistle = pursueWhistle;
	}

	public bool GetPursueWhistle()
	{
		anim_name = AnimationNamesUtil.GetAnimName(enemyName ,"walk");
		return this.pursueWhistle;
	}


	public void SetPlayerInMin(bool val)
	{
		playerInMin = val;
		Debug.Log ("Player here: " + enemyName);
	}

	public void SetPlayerInMax(bool val)
	{
		playerInMax = val;
	}

	public bool PlayerInMax()
	{
		return playerInMax;
	}

	public bool PlayerInMin()
	{
		return playerInMin;
	}


	public void AnimationPlay(string name)
	{
		Animation anim = transform.GetChild (0).GetComponent<Animation> ();
		if (anim != null)
			anim.Play (name);
		else
			transform.GetChild (0).GetChild (0).GetComponent<Animation> ().Play (name);
	}

	public void Attack()
	{
		attacking = true;
		agent.Stop ();
		AnimationPlay (AnimationNamesUtil.GetAnimName(enemyName, "attack"));

		//play the attack audio
		if(!isLittleMonster)
			audios[1].Play ();
	}

	public void Stop()
	{
		agent.Stop ();
		AnimationPlay (AnimationNamesUtil.GetAnimName(enemyName, "idle"));
	}

	public void ActivateHighlight(bool val)
	{
		if (isLittleMonster) {
			GetComponent<LittleMHighlightScript> ().ActivateHighlight (val);
		} else {
			GetComponent<HighlightScript> ().ActivateHighlight (val);
		}
	}

	void PlaySound(int id)
	{
		if (isLittleMonster)
			return;
		
		if (audioStates[id]) {
			audioStates [id] = false;
			audios [id].Play ();
		}
	}

	IEnumerator StartSound(int id)
	{
		yield return new WaitForSeconds(0.3f);

		audioStates [id] = true;


	}
}
