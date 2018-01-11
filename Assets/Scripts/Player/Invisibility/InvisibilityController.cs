using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilityController : MonoBehaviour 
{
	private ShopManager shopManager;

	private Transform player;
	private InvisibilityAnimScript invisArea;

	private CameraContoller cameraContoller;
	private SkillUtil skills;
	private UIScript ui;

	private AudioSource audio;
	private bool chooseStart;

	private Material outlineMat;
	private Color extraRedColor = new Color (2f, 0f, 0f);
	void Awake()
	{
		shopManager = GameObject.Find ("ShopManager").GetComponent<ShopManager> ();

		player = GameObject.FindGameObjectWithTag ("PlayerNode").transform;

		invisArea = GetComponent<InvisibilityAnimScript> ();

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
		if (skills.invisibleClicked && !chooseStart && (ui.invisibleCount > 0)) 
		{
			// invisible open range anim start
			chooseStart = true;

			invisArea.OpenRange ();

			transform.position = player.position;
		}

		if (skills.invisibleActivateClicked && chooseStart) 
		{

			//decrease the ui teleport count text and update the button
			ui.invisibleCount -= 1;
			if (ui.invisibleCount == 0) {
				ui.PermaDisableSkill ("Invisible");
			}

			//play the auido
//			audio.Play();

			skills.invisibleActivateClicked = false;
			StartCoroutine (closeAnimations ());

		} else if (skills.invisibleCanceled) {
			chooseStart = false;
			skills.teleportCanceled = false;
			invisArea.CloseArea ();
		}

		transform.position = player.position;
	}

	IEnumerator closeAnimations()
	{
		invisArea.CloseArea();

		yield return new WaitForSeconds (0.15f);

		invisArea.StartParticleEffect ();

		chooseStart = false;

		//cameraContoller.ActivateTeleport (true, player.position, transform.position);
		skills.invisibleClicked = false;

		yield return new WaitForSeconds (0.5f);

		// draw the outline and inform the player 
		outlineMat.SetFloat ("_Outline", 0.03f);
		outlineMat.SetColor ("_OutlineColor", extraRedColor);

		transform.parent.GetComponent<PlayerController> ().invisible = true;
	
		yield return new WaitForSeconds (shopManager.invisibilityTime);

		// effect of invisibility is finished! Restart the status!
		outlineMat.SetFloat ("_Outline", 0.0f);
		transform.parent.GetComponent<PlayerController> ().invisible = false;

	}
}
