using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleMMinRange : MonoBehaviour 
{
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
			if (distance < self.rangeValue * 2) {
				float angle = Vector3.Angle (self.transform.forward, (player.transform.position - self.transform.position));
				if (angle <= self.rangeAngle / 2f) {
					Debug.Log ("player in min area");
					self.SetPlayerInMin(true);
					pin = true;
				}
			}
		}
		else
		{
			if (distance >= self.rangeValue * 2) {
				self.SetPlayerInMax (false);
				pin = false;
			} else {
				float angle = Vector3.Angle (self.transform.forward, (player.transform.position - self.transform.position));
				if (angle > self.rangeAngle / 2f) {
					Debug.Log ("player out of min area");
					self.SetPlayerInMin (false);
					pin = false;
				}
			}
		}
	}
}
