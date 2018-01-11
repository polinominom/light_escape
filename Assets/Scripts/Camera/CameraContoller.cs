using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour {


	private TeleportCameraEffect teleportCam;
	private CameraFollow normalCam;

	private SkillUtil skills;
	void Start () 
	{
		normalCam = GetComponent<CameraFollow> ();
		teleportCam = GetComponent<TeleportCameraEffect> ();

		teleportCam.enabled = false;
		normalCam.enabled = true;

		skills = GameObject.FindGameObjectWithTag ("PlayerNode").GetComponent<SkillUtil> ();
	}
	

	public void ActivateTeleport(bool val, Vector3 playerOldPos, Vector3 playerNewPos)
	{
		teleportCam.enabled = val;
		if (val) {
			teleportCam.SetProperties (playerOldPos, playerNewPos);
		} else {
			//reset the skill conditions
//			skills.teleportClicked = false;
//			skills.teleportActivateClicked = false;
		}
		ActivateNormal (!val);
	}

	public void ActivateNormal(bool val)
	{
		normalCam.enabled = val;
	}
}
