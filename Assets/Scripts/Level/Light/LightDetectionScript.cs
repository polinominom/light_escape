using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetectionScript : MonoBehaviour {


    private GameObject[] enemies;
	private GameObject player;

	private bool deactive;
	private float deactiveTime;
	private static float deactiveMaxTime = 2.0f;

    void Start()
    {
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		player = GameObject.FindGameObjectWithTag ("Player");

		deactive = false;
		deactiveTime = 0.0f;
    }

	void Update()
	{
		if (deactive) {
			Debug.Log ("LightDetectionScript: deactiveTime:" + deactiveTime);
			deactiveTime += Time.deltaTime;
			if (deactiveTime >= deactiveMaxTime) {
				deactive = false;
				deactiveTime = 0.0f;
				GetComponentInParent<Light> ().enabled = true;
			}
		}
	}

    void OnTriggerEnter(Collider other)
    {
		if (deactive)
			return;
		if (other.CompareTag ("Player")) 
		{
			for (int i = 0; i < enemies.Length; i++) 
			{
				enemies [i].GetComponent<Enemy> (). PlayerEnteredLight();
			}			
		}
    }

	void OnTriggerExit(Collider other)
	{
		if (deactive)
			return;
		
		if (other.CompareTag ("Player")) 
		{
			for (int i = 0; i < enemies.Length; i++) 
			{
				enemies [i].GetComponent<Enemy> (). PlayerExitedLight();
			}			
		}
	}

	public void Deactivate()
	{
		deactive = true;
		deactiveTime = 0.0f;
		GetComponentInParent<Light> ().enabled = false;
	}

}
