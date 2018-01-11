using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrowerScript : MonoBehaviour {

	private bool throwable;
	public float effectRange;
	private SkillUtil skills;

	private PaddleController paddleController;
	private bool deactivated;



	void Awake()
	{
		paddleController = GetComponentInChildren<PaddleController> ();
		skills = transform.parent.GetComponent<SkillUtil> ();		

	}

	void Start () 
	{
		throwable = false;
		deactivated = false;
	}
	
	void Update () 
	{
		if (skills.rockThrowClicked)
		{
			//paddleController.SetNewPosition (new Vector3(transform.position.x, 0.1f, transform.position.z));
			skills.rockThrowClicked = false;
			throwable = !throwable;
			paddleController.ActivateRange (throwable);

		} 
		else if (deactivated && throwable) 
		{
			deactivated = false;
			throwable = false;
			paddleController.ActivateRange (throwable);
		}
	}
		
	public bool isThrowable()
	{
		return throwable;
	}

	public void SetThrowable(bool val)
	{
		this.throwable = val;
	}

	public void Deactivated()
	{
		deactivated = true;
	}
}
