using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TeleporthandlerScript : MonoBehaviour 
{

	public Transform player;
	public Transform effect;
	public TeleportAreaAnimScript secondArea;

	private bool chooseStart;
	Vector3 newPosition;
	private ParticleSystem butterfly;
	private SpriteRenderer sRenderer;
	private SpriteRenderer sRenderer2;


	private Vector3 localStartPos;

	private CameraContoller cameraContoller;
	private SkillUtil skills;
	private UIScript ui;

	private AudioSource audio;

	void Awake()
	{
		ui = GameObject.FindGameObjectWithTag ("UI").GetComponent<UIScript>();

		audio = GetComponent<AudioSource> ();
	}

	void Start () 
	{
		chooseStart = false;
		newPosition = new Vector3(player.position.x, 0.1f, player.position.z);
		butterfly = effect.GetComponent<ParticleSystem> ();

		sRenderer = GetComponent<SpriteRenderer> ();
		sRenderer2 = transform.GetChild (0).GetComponent<SpriteRenderer> ();
		sRenderer.enabled = false;
		sRenderer2.enabled = false;

		secondArea.GetComponent<SpriteRenderer> ().enabled = false;
		secondArea.transform.GetChild (0).GetComponent<SpriteRenderer> ().enabled = false;

		localStartPos = new Vector3 (0f, 0.1f, 0f);

		cameraContoller = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraContoller> ();

		skills = transform.parent.parent.GetComponent<SkillUtil> ();
	}
	
	void Update () 
	{
		if (skills.teleportClicked && !chooseStart && (ui.teleportCount > 0)) 
		{
			chooseStart = true;

			secondArea.OpenRange ();

			transform.position = player.position;
		}

		// choosing teleport area
		if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0) && chooseStart)
		{
			RaycastHit hit;
			int layermask = (1 << 8) | (1 << 5);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask))
			{
				newPosition = new Vector3 (hit.point.x, 0.1f, hit.point.z);
				sRenderer.enabled = true;
				AnimationPlay ("TeleportOpenArea");

			}
		}

		if (skills.teleportActivateClicked && chooseStart) {

			//decrease the ui teleport count text and update the button
			ui.teleportCount -= 1;
			if (ui.teleportCount == 0) {
				ui.PermaDisableSkill ("Teleport");
			}

			//play the auido
			audio.Play();

			skills.teleportActivateClicked = false;
			StartCoroutine (closeAnimations ());


		} else if (skills.teleportCanceled) {
			chooseStart = false;
			skills.teleportCanceled = false;
			AnimationPlay ("TeleportCloseArea");
			secondArea.CloseArea ();
		}

		transform.position = newPosition;
			
	}

	IEnumerator closeAnimations()
	{
		secondArea.CloseArea();

		yield return new WaitForSeconds (0.15f);

		AnimationPlay ("TeleportCloseArea");

		yield return new WaitForSeconds (0.15f);

		chooseStart = false;
		Vector3 temp = transform.position;

		cameraContoller.ActivateTeleport (true, player.position, transform.position);

		player.position = transform.position;
		skills.teleportClicked = false;


	}

	public void StartParticleEffect()
	{
		// play the butterflies
		butterfly.Play();		
	}


	void AnimationPlay(string name)
	{
		GetComponent<Animation> ().Play (name);
	}
}
