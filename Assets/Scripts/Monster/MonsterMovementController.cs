using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovementController : MonoBehaviour {

	private bool walkingState = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F))
		{
			walkingState = !walkingState;
			GetComponentInChildren<Animator> ().SetBool("isWalking", walkingState);
		}
	}
}
