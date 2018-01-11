using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxRangeCollider : MonoBehaviour {

	private Enemy _parent;

	// Use this for initialization
	LineRenderer lineRenderer;

	void Start () 
	{
		// get the range from player
		_parent = GetComponentInParent<Enemy>();
		GetComponent<SphereCollider> ().radius = 2 * _parent.rangeValue;
		lineRenderer = _parent.GetComponent<LineRenderer>();
	}

	//TODO: create LittleMonster class that always contols the players position if its out of the collider.
	void OnTriggerEnter(Collider other)
	{
		
		if (other.CompareTag ("Player")) 
		{
			if (_parent.enemyName.Equals ("LittleMonster")) 
			{
				//draw line _parent.transform.forward

				float angle = Vector3.Angle (_parent.transform.forward, (other.transform.position-_parent.transform.position));
				Debug.Log ("angle: " + angle);
				if (angle <= _parent.rangeAngle / 2f)
					_parent.SetPlayerInMax (true);
			}
			else
				_parent.SetPlayerInMax (true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag ("Player")) 
		{
			_parent.SetPlayerInMax(false);
		}
	}
}
