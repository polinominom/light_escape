using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillContoller : MonoBehaviour {

	public SkillUtil skills;
	public float radarRechargeTime;

	Button radarButton;
	Button whistleButton;
	Button teleportButton;
	Button rockButton;
	Button disguiseButton;
	Button invisibilityButton;
	/* 0: whistleButton
	 * 1: rockButton
	 * 2: Teleport
	 * 3: Invisible
	 * 4: Disguise
	*/
	Button[] buttons;

	Button activate;
	Button cancel;

	Button zoom;
	private bool zoomActiave;
	private CameraFollow cameraFollow;
	public float zoomIN;
	public float zoomOUT;

	private bool permaDisableWhistle;
	private bool permaDisableTeleport;
	private bool permaDisableRock;
	private bool permaDisableInvisibility;
	private bool permaDisableDisguise;

	private OptionManager optionManager;

	void Awake()
	{
		buttons = new Button[5];

		buttons[0] = GameObject.Find ("WhistleButton").GetComponent<Button> ();
		buttons[1] = GameObject.Find ("ThrowRockButton").GetComponent<Button> ();
		buttons[2] = GameObject.Find ("TeleportButton").GetComponent<Button> ();
		buttons[3] = GameObject.Find ("InvisibilityButton").GetComponent<Button> ();
		buttons[4] = GameObject.Find ("DisguiseButton").GetComponent<Button> ();

		radarButton = GameObject.Find ("RadarButton").GetComponent<Button> ();

		Transform activateButtons = GameObject.Find ("ActivateButtons").transform;
		activate = activateButtons.GetChild (0).GetComponent<Button> ();
		cancel = activateButtons.GetChild (1).GetComponent<Button> ();

		zoom = GameObject.Find ("ZoomButton").GetComponent<Button> ();		

		cameraFollow = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow> ();
		optionManager = GameObject.Find ("OptionManager").GetComponent<OptionManager> ();
	}

	void Start()
	{
		zoomActiave = false;

		permaDisableWhistle = false;
		permaDisableTeleport = false;
		permaDisableRock = false;
		permaDisableInvisibility = false;
		permaDisableDisguise = false;

		// close and re-calculate locations
		InitializeAppearance ();

	}

	void InitializeAppearance()
	{
		int limit = optionManager.selectedButtons.Length;
		Vector2 xy = GameObject.Find ("Canvas").GetComponent<RectTransform> ().sizeDelta;

		float x = -1 * (xy.x / 2f - 50f);
		float y = -160.5f;
		// -I- recalculate locations of selected buttons -I- 

		float yModifier = 75;
		//y1 = -160.5, y2 = -85.5, y3 = -10.5 (+75 each)
		for (int i = 0; i < limit; i++) 
		{
			Button b = buttons [optionManager.selectedButtons [i]];
			b.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (x, y + yModifier * i);

		}

		radarButton.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (x, y + yModifier *  limit);
		zoom.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (x, y + yModifier *  (limit+1));

		//close all the buttons
		for (int i = 0; i < 5; i++) 
		{
			buttons[i].gameObject.SetActive (false);
		}
		radarButton.gameObject.SetActive (false);
		zoom.gameObject.SetActive (false);
	}

	public void WhistleButtonClicked()
	{
		// use the skil
		skills.whistleClicked = true;
		skills.whistleCanActivate = true;
		ActivateSkillButtons (false);

		StartCoroutine (SkillUsed ());
	}

	public void TeleportButtonClicked()
	{
		// use the skil
		skills.teleportClicked = true;
		ActivateSkillButtons (false);

		StartCoroutine (SkillUsed ());
	}

	public void RockButtonClicked()
	{
		skills.rockThrowClicked = true;
		skills.rockThrowCanActivate = true;
		ActivateSkillButtons (false);

		StartCoroutine (SkillUsed ());
	}

	public void RadarButtonClicked()
	{
		radarButton.interactable = false;
		skills.radarClicked = true;
		// player use radar

		StartCoroutine (RadarUsed ());
	}

	public void InvisibleButtonClicked()
	{
		skills.invisibleClicked = true;

		ActivateSkillButtons (false);

		StartCoroutine (SkillUsed ());
	}

	public void DisguiseButtonClicked()
	{
		skills.disguiseClicked = true;

		ActivateSkillButtons (false);

		StartCoroutine (SkillUsed ());
	}

	IEnumerator RadarUsed()
	{
		yield return new WaitForSeconds (radarRechargeTime);
		radarButton.interactable = true;

	}

	//TODO: BURALARA HEP BAKILACAK
	void ActivateSkillButtons(bool val)
	{
//		buttons[optionManager.selectedButtons[0]].interactable = permaDisableWhistle ? false : val;
//		buttons[optionManager.selectedButtons[1]].interactable = permaDisableRock ? false : val;
//		buttons[optionManager.selectedButtons[2]].interactable = permaDisableTeleport ? false : val;

		buttons[0].interactable = permaDisableWhistle ? false : val;
		buttons[1].interactable = permaDisableRock ? false : val;
		buttons[2].interactable = permaDisableTeleport ? false : val;
		buttons[3].interactable = permaDisableInvisibility ? false : val;
		buttons[4].interactable = permaDisableDisguise ? false : val;

		activate.interactable = !val;
		activate.gameObject.SetActive (!val);

		cancel.interactable = !val;
		cancel.gameObject.SetActive (!val);

	}

	// TODO: add the invis and disguise skills
	IEnumerator SkillUsed()
	{
		bool possibleOne = skills.rockThrowCanActivate || skills.teleportClicked || skills.whistleCanActivate || skills.invisibleClicked || skills.disguiseClicked;
		while (possibleOne) 
		{
			possibleOne = skills.rockThrowCanActivate || skills.teleportClicked || skills.whistleCanActivate || skills.invisibleClicked || skills.disguiseClicked;
			yield return null;	
		}

		ActivateSkillButtons (true);
	}


	public void PermaDisableButton(string skillName)
	{
		Button b;
		if (skillName.Equals ("Rock")) {
			b = FindButton ("ThrowRockButton");
			if (b != null) {
				b.interactable = false;
				permaDisableRock = true;				
			}

		} else if (skillName.Equals ("Whistle")) {
			b = FindButton ("WhistleButton");
			if (b != null) {
				b.interactable = false;
				permaDisableWhistle = true;
			}
		} else if (skillName.Equals ("Teleport")) {
			b = FindButton ("TeleportButton");
			if (b != null) {
				b.interactable = false;
				permaDisableTeleport = true;
			}
		} else if (skillName.Equals ("Invisible")) {
			b = FindButton ("InvisibilityButton");
			if (b != null) {
				b.interactable = false;
				permaDisableInvisibility = true;
			}
		} else if (skillName.Equals ("Disguise")) {
			b = FindButton ("DisguiseButton");
			if (b != null) {
				b.interactable = false;
				permaDisableDisguise = true;
			}
		}		
			
	}

/* 
	 * -> WhistleButton
	 * -> ThrowRockButton
	 * -> TeleportButton
	 * -> InvisibilityButton
	 * -> DisguiseButton
*/
	Button FindButton(string name)
	{
		int limit = buttons.Length;
		for (int i = 0; i < limit; i++) 
		{
			if (buttons [i].transform.name.Equals (name)) 
			{
				return buttons [i];
			}
		}
		return GameObject.Find (name).GetComponent<Button> ();

	}


	public void ZoomClicked()
	{
		zoomActiave = !zoomActiave;
		if (zoomActiave) {
			cameraFollow.height = zoomIN;
		} else
			cameraFollow.height = zoomOUT;
	}

	public void StartGame()
	{
		InitializeAppearance ();

		for (int i = 0; i < optionManager.selectedButtons.Length; i++) 
		{
			buttons [optionManager.selectedButtons[i]].gameObject.SetActive (true);
		}

		radarButton.gameObject.SetActive (true);
		zoom.gameObject.SetActive (true);
	}
}
