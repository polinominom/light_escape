using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandlerScript : MonoBehaviour {

	private Animator animator;
	private int debugcounter;

	// Use this for initialization
	void Start () {
		animator = transform.GetChild(0).GetComponent<Animator> ();
	}
	

	public void idleToWalk()
	{
		animator.SetBool ("idleToWalk", true);
		animator.SetBool ("walkToIdle", false);
		animator.SetBool ("walkToRun", false);
		animator.SetBool ("runToWalk", false);
		animator.SetBool ("idleToRun", false);
		animator.SetBool ("runToIdle", false);

//		Debug.Log ("idleToWalk anim");
	}

	public void walkToIdle()
	{
		animator.SetBool ("idleToWalk", false);
		animator.SetBool ("walkToIdle", true);
		animator.SetBool ("walkToRun", false);
		animator.SetBool ("runToWalk", false);
		animator.SetBool ("idleToRun", false);
		animator.SetBool ("runToIdle", false);

//		Debug.Log ("walkToIdle  anim");
		
	}

	public void walkToRun()
	{
		animator.SetBool ("idleToWalk", false);
		animator.SetBool ("walkToIdle", false);
		animator.SetBool ("walkToRun", true);
		animator.SetBool ("runToWalk", false);
		animator.SetBool ("idleToRun", false);
		animator.SetBool ("runToIdle", false);
	}

	public void runToWalk()
	{
		animator.SetBool ("idleToWalk", false);
		animator.SetBool ("walkToIdle", false);
		animator.SetBool ("walkToRun", false);
		animator.SetBool ("runToWalk", true);
		animator.SetBool ("idleToRun", false);
		animator.SetBool ("runToIdle", false);	
	}

	public void idleToRun()
	{
		animator.SetBool ("idleToWalk", false);
		animator.SetBool ("walkToIdle", false);
		animator.SetBool ("walkToRun", false);
		animator.SetBool ("runToWalk", false);
		animator.SetBool ("idleToRun", true);
		animator.SetBool ("runToIdle", false);	
	}

	public void runToIdle()
	{
		animator.SetBool ("idleToWalk", false);
		animator.SetBool ("walkToIdle", false);
		animator.SetBool ("walkToRun", false);
		animator.SetBool ("runToWalk", false);
		animator.SetBool ("idleToRun", false);
		animator.SetBool ("runToIdle", true);
	}

	public void Update()
	{
//		float limitOffset = 0.0001f;
//		Rigidbody rb = GetComponent<Rigidbody> ();
//		if (rb.velocity.magnitude < limitOffset) 
//		{
//			animator.SetBool ("idleToWalk", false);
//			animator.SetBool ("walkToIdle", true);
//			animator.SetBool ("walkToRun", false);
//			animator.SetBool ("runToWalk", false);
//			animator.SetBool ("idleToRun", false);
//			animator.SetBool ("runToIdle", true);
//		}
	}
}
