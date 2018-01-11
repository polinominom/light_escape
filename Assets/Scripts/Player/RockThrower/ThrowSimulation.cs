using UnityEngine;
using System.Collections;

public class ThrowSimulation : MonoBehaviour
{
	public Transform Target;
	private RockThrowerScript thrower;
	public float firingAngle = 45.0f;
	public float gravity = 9.8f;

	public Transform Projectile;
	public float speed;

	public Transform spawnPosition;
	private PlayerController player;
	private SkillUtil skills;

	private UIScript ui;
	private AudioSource auido;

	void Awake()
	{
		ui = GameObject.FindGameObjectWithTag ("UI").GetComponent<UIScript> ();
		Target = GetComponentInChildren<PaddleController> ().transform;

		thrower = GetComponent<RockThrowerScript> ();
		auido = GetComponent<AudioSource> ();

		player = transform.parent.GetComponent<PlayerController> ();
		skills = player.GetComponent<SkillUtil> ();
	}

	void Update()
	{
		if (!thrower.isThrowable ())
			return;
		
		if (skills.rockThrowActivateClicked) {

			//decrease the stone count
			ui.stoneCount -= 1;
			if (ui.stoneCount == 0) {
				ui.PermaDisableSkill ("Rock");
			}



			// set the skill value for buttons
			skills.rockThrowActivateClicked = false;

			// throw the rock
			player.throwRock ();
			thrower.SetThrowable (false);

			StartCoroutine (SimulateProjectile ());

		} 
		else if (skills.rockThrowCanceled) {
			skills.rockThrowCanceled = false;
			thrower.Deactivated ();
		}
	}

	IEnumerator SimulateProjectile()
	{
		// Short delay added before Projectile is thrown
		yield return new WaitForSeconds(0.3f);

		//play the sound
		auido.Play ();

		Physics.IgnoreCollision (Projectile.GetComponent<SphereCollider> (), transform.parent.GetChild(0).GetComponent<BoxCollider>());

		// Move projectile to the position of throwing object + add some offset if needed.
		Projectile.position = spawnPosition.position;

		Projectile.gameObject.SetActive (true);

		// Calculate distance to target
		Vector3 targetPoint = new Vector3(Target.position.x, Target.position.y - 1f, Target.position.z);

		float target_Distance = Vector3.Distance(Projectile.position, targetPoint);

		// Calculate the velocity needed to throw the object to the target at specified angle.
		float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

		// Extract the X  Y componenent of the velocity
		float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
		float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

		// Calculate flight time.
		float flightDuration = target_Distance / Vx;

		// Rotate projectile to face the target.
		Projectile.rotation = Quaternion.LookRotation(targetPoint - Projectile.position);

		float elapse_time = 0;

		while (elapse_time < flightDuration)
		{
			Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

			elapse_time += Time.deltaTime;

			yield return null;
		}

		skills.rockThrowClicked = false;

	}  
}
