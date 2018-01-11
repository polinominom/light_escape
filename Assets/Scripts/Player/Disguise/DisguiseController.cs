using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisguiseController : MonoBehaviour {

	private ShopManager shopManager;

	private Transform player;
	private DisguiseRangeAnimScript disguiseArea;

	private CameraContoller cameraContoller;
	private SkillUtil skills;
	private UIScript ui;

	private AudioSource audio;
	private bool chooseStart;

	private Material outlineMat;
	private Color orangeColor = new Color (1f, 165f / 255f, 0f);
	// Use this for initialization
	void Awake () 
	{
		shopManager = GameObject.Find ("ShopManager").GetComponent<ShopManager> ();

		player = GameObject.FindGameObjectWithTag ("PlayerNode").transform;

		disguiseArea = GetComponent<DisguiseRangeAnimScript> ();

		//TODO: camera effects shaking
		//		cameraContoller = GameObject.Find ("MainCamera").GetComponent<CameraContoller> ();

		skills = player.GetComponent<SkillUtil> ();

		ui = GameObject.FindGameObjectWithTag ("UI").GetComponent<UIScript>();

		audio = GetComponent<AudioSource> ();	

		outlineMat = GameObject.Find ("FULL_MAX005").GetComponent<SkinnedMeshRenderer> ().materials [1];
	}
	
	void Start ()
	{
		chooseStart = false;
	}

	void Update () 
	{
		if (skills.disguiseClicked && !chooseStart && (ui.disguiseCount > 0)) 
		{
			// disguise open range anim start
			chooseStart = true;

			disguiseArea.OpenRange ();

			transform.position = player.position;

		}

		if (skills.disguiseActivateClicked && chooseStart) 
		{

			//decrease the ui teleport count text and update the button
			ui.disguiseCount -= 1;
			if (ui.disguiseCount == 0) {
				ui.PermaDisableSkill ("Disguise");
			}

			//play the auido
			//			audio.Play();

			skills.disguiseActivateClicked = false;
			StartCoroutine (closeAnimations ());

		} else if (skills.disguiseCanceled) {
			chooseStart = false;
			skills.disguiseCanceled = false;
			disguiseArea.CloseArea ();
		}

		transform.position = player.position;
	}

	IEnumerator closeAnimations()
	{
		disguiseArea.CloseArea();

		yield return new WaitForSeconds (0.15f);

		disguiseArea.StartParticleEffect ();

		chooseStart = false;

		//cameraContoller.ActivateTeleport (true, player.position, transform.position);

		//player.position = transform.position;
		skills.disguiseClicked = false;

		yield return new WaitForSeconds (0.5f);

		// draw the outline and inform the player
		outlineMat.SetFloat ("_Outline", 0.03f);
		outlineMat.SetColor ("_OutlineColor", orangeColor);

		transform.parent.GetComponent<PlayerController> ().disguised = true;

		yield return new WaitForSeconds (shopManager.disguiseTime);

		// disguise effect is finished!
		// restart the stuations;
		outlineMat.SetFloat ("_Outline", 0.0f);
		transform.parent.GetComponent<PlayerController> ().disguised = false;

	}
}
