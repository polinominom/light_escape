using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverControllerScript : MonoBehaviour {

	public Text winText;
	public Text gameOverText;
	public string levelName;

	private UIScript ui;
	void Start()
	{
		ui = GameObject.FindGameObjectWithTag ("UI").GetComponent<UIScript> ();
			
	}

	void Update()
	{
		
	}
		
	void OnTriggerEnter(Collider collider)
	{
		if (ui.isGameOver)
			return;
		
		if (collider.CompareTag ("Enemy")) 
		{
			//GetComponent<AudioSource> ().Play ();
			ui.playAnimation = false;
			ui.isGameOver = true;

			transform.parent.GetComponent<PlayerController> ().dead = true;
			collider.transform.LookAt (transform.parent.GetChild(0));

			Enemy e = collider.GetComponent<Enemy> ();
			e.Attack ();

			// stop the others
			GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
			foreach (GameObject enemy in enemies) 
			{
				if (!enemy.Equals(collider.gameObject)) 
				{
					enemy.GetComponent<Enemy> ().Stop ();
				}

			}

//			collider.transform.parent.GetComponent<Enemy> ().AnimationPlay (AnimationNamesUtil.attackTwo);
//			collider.transform.parent.GetComponent<Enemy> ().AnimationPlay (AnimationNamesUtil.attackThree);
			StartCoroutine (Die());


		}
	}


	IEnumerator Die()
	{
		// wait for the monster attack
		yield return new WaitForSeconds(0.4f);

		//now dead animation
		transform.parent.GetComponent<PlayerController> ().AnimationPlay ("death");

		// wait for death anim
		yield return new WaitForSeconds(1f);
		transform.parent.gameObject.SetActive (false);

		ui.StartCoroutine (ui.GameOverAnimation ());
	}

}
