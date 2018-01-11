using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerSpriteScript : MonoBehaviour {

	void Update () 
	{
		transform.LookAt (Camera.main.transform.position, Vector3.up);	
	}

	public void Reset()
	{
		transform.LookAt (Camera.main.transform.position, Vector3.up);
	}

}
