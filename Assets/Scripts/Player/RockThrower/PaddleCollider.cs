using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleCollider : MonoBehaviour {

	private RockThrowerScript rockThrower;
	private PaddleController paddleController;

	private SphereCollider sCollider;
	private float maxRange = 0f;

	// Use this for initialization
	void Start () 
	{
		maxRange = GetComponentInParent<RockThrowerScript> ().effectRange;
		sCollider = GetComponent<SphereCollider> ();

		paddleController = transform.parent.GetComponentInChildren<PaddleController> ();
	}

	public void ActivateCollider(bool val)
	{
		sCollider.radius = val ? maxRange : 0f;
//		Debug.Log ("PaddleCollider: Radiues has been set: "+sCollider.radius);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Enemy")) 
		{
			paddleController.AddEnemy (other.gameObject);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag ("Enemy")) 
		{
			paddleController.RemoveEnemy (other.gameObject);
		}		
	}
	
}
