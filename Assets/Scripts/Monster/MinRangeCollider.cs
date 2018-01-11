using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinRangeCollider : MonoBehaviour {

	private Enemy _parent;
	// Use this for initialization
	void Start () 
	{
		// get the range from player
		_parent = GetComponentInParent<Enemy>();
		GetComponent<SphereCollider> ().radius =_parent.rangeValue;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Player")) 
		{
			if (_parent.enemyName.Equals ("LittleMonster")) 
			{
				//draw line _parent.transform.forward

				float angle = Vector3.Angle (_parent.transform.forward, (other.transform.position-_parent.transform.position));
				Debug.Log ("angle: " + angle);
				Debug.Log ("_parent.rangeAngle: " + _parent.rangeAngle);
				if (angle <= _parent.rangeAngle / 2f) {
					Debug.Log ("asfşadgaşdlgkadgiadkga");
					_parent.SetPlayerInMin (true);
				}
			}
			else
				_parent.SetPlayerInMin (true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag ("Player")) 
		{
			_parent.SetPlayerInMin (false);
		}
	}
}
