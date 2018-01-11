using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhistleCollider : MonoBehaviour {

	// Use this for initialization
	private WhistleController whistle;
	private SphereCollider sCollider;

	void Start () 
	{
		whistle = transform.parent.GetComponentInChildren<WhistleController> ();
		sCollider = GetComponent<SphereCollider> ();
		sCollider.radius = whistle.range;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Enemy")) 
		{
			whistle.AddEnemy (other.gameObject);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag ("Enemy")) 
		{
			whistle.RemoveEnemy (other.gameObject);
		}		
	}
}
