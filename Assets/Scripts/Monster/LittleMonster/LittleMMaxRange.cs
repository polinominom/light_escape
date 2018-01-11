using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleMMaxRange : MonoBehaviour {

	private GameObject player;
	private Enemy self;

	private bool pin;
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("PlayerNode");
		self = transform.parent.GetComponent<Enemy> ();
		pin = false;
	}

	void Update () 
	{
		float distance = Vector3.Distance (player.transform.position, self.transform.position);
		if (!pin) {
			if (distance < self.rangeValue * 4) {
				float angle = Vector3.Angle (self.transform.forward, (player.transform.position - self.transform.position));
				if (angle <= self.rangeAngle / 2f) {
					Debug.Log ("player in max area");
					self.SetPlayerInMax (true);
					pin = true;
				}
			}
		}
		else
		{
			if (distance >= self.rangeValue * 4) {
				self.SetPlayerInMax (false);
				pin = false;
			} else {
				float angle = Vector3.Angle (self.transform.forward, (player.transform.position - self.transform.position));
				if (angle > self.rangeAngle / 2f) {
					Debug.Log ("player out max area");
					self.SetPlayerInMax (false);
					pin = false;
				}
			}
		}
	}
}
