using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;

	public float speed = 5f;
	public float turnSpeed;

	public bool dead;
	private string animName;

	private bool throwing;
	private bool whistling;
	private bool teleporting;

	public bool disguised;
	public bool invisible;

	void Start () 
	{
		rb = transform.GetChild (0).GetComponent<Rigidbody> ();

		dead = false;
		throwing = false;
		whistling = false;
		teleporting = false;

	}
		
	void FixedUpdate () 
	{

		animName = "idle";
		if (dead)
			return;

		if (!throwing && !whistling && !teleporting) 
		{
			movement ();
		}

		if(throwing)
		{
			animName = "punch";
		}


		AnimationPlay (animName);
	}
		

	void movement()
	{
		Vector2 vel = new Vector2 (0f, 0f);
		if (Input.GetKey (KeyCode.W)) 
		{
			vel.x -= 1f;
			animName = "walk";

		}

		if (Input.GetKey (KeyCode.S)) 
		{
			vel.x += 1f;
			animName = "walk";
		}

		if (Input.GetKey (KeyCode.A)) 
		{
			//change the rotation
			transform.Rotate(new Vector3(0f, -1f, 0f) * turnSpeed);
		}

		if (Input.GetKey (KeyCode.D)) 
		{
			transform.Rotate(new Vector3(0f, 1f, 0f) * turnSpeed);
		}

		vel = vel * speed*Time.fixedDeltaTime;
		transform.Translate (new Vector3(vel.y, 0f, -vel.x));		
	}

	// idle
	// walk
	// run
	// jump
	// flip
	// punch
	// kick
	// death
	public void AnimationPlay(string name)
	{
		if(!dead || name.Equals("death") || name.Equals("kick"))
			transform.GetChild (0).GetComponent<Animation> ().Play (name);	
	}

	public void throwRock()
	{
		throwing = true;
		StartCoroutine (throwingrock ());
	}

	IEnumerator throwingrock()
	{
		yield return new WaitForSeconds (1f);

		throwing = false;
		Debug.Log ("Throwing done!");
	}
}
