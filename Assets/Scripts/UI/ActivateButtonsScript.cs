using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateButtonsScript : MonoBehaviour 
{
	private SkillUtil skills;

	void Awake()
	{
		this.skills = GameObject.FindGameObjectWithTag("PlayerNode").GetComponent<SkillUtil> ();
	}

	void Start()
	{
		transform.GetChild(0).gameObject.SetActive(false);
		transform.GetChild(1).gameObject.SetActive(false);
	}


	public void ActivateButton()
	{
		if (skills.whistleCanActivate) {
			skills.whistleActivateClicked = true;
			skills.whistleCanActivate = false;
		} else if (skills.teleportClicked) {
			skills.teleportActivateClicked = true;
		} else if (skills.rockThrowCanActivate) {
			skills.rockThrowActivateClicked = true;
			skills.rockThrowCanActivate = false;
		} else if (skills.invisibleClicked) {
			skills.invisibleActivateClicked = true;
		} else if (skills.disguiseClicked) {
			skills.disguiseActivateClicked = true;
		}
	}

	public void CancelButton()
	{
		if (skills.whistleCanActivate) {
			skills.whistleCanActivate = false;
			skills.whistleCanceled = true;
		} else if (skills.teleportClicked) {
			skills.teleportClicked = false;
			skills.teleportCanceled = true;
		} else if (skills.rockThrowCanActivate) {
			skills.rockThrowCanActivate = false;
			skills.rockThrowCanceled = true;
		} else if (skills.invisibleClicked) {
			skills.invisibleClicked = false;
			skills.invisibleCanceled = true;
		}else if (skills.disguiseClicked) {
			skills.disguiseClicked = false;
			skills.disguiseCanceled = true;
		}
	}
}
