using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCameraEffect : MonoBehaviour {

	private Vector3 target;
	private Vector3 oldPos;
	private Vector3 currentPos;
	private CameraContoller cameraController;

	private float incremental;
	public float smooth = 2f;

	void Start () 
	{
		cameraController = GetComponent<CameraContoller> ();
		incremental = 0f;
	}

	public void SetProperties(Vector3 playerOldPos, Vector3 playerNewPos)
	{
		Vector3 offset =  transform.position - playerOldPos;

		this.oldPos = transform.position;
		this.target = offset + playerNewPos;

		this.currentPos = oldPos;

		transform.position = currentPos;

		incremental = 0f;

		StartCoroutine (teleportEffect ());
	}


	IEnumerator teleportEffect()
	{
		while (incremental < 1)
		{
			currentPos = Vector3.Lerp (currentPos, target, incremental);
			incremental += Time.deltaTime * smooth;

			transform.position = currentPos;
			yield return null;
		}

		currentPos = Vector3.Lerp (currentPos, target, 1);
		transform.position = currentPos;
		cameraController.ActivateTeleport (false, Vector3.zero, Vector3.zero);
	}
}
