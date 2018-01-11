using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleMRangeModifier : MonoBehaviour {

	public float rangeAppearanceTime;
	private float range;
	private float angle;
	private Transform minR;
	private Transform maxR;

	private SphereCollider minCol;
	private SphereCollider maxCol;
	// Use this for initialization
	void Start () 
	{
		range = GetComponent<Enemy> ().rangeValue;
		angle = GetComponent<Enemy> ().rangeAngle;
		minR = transform.GetChild (1).transform;
		maxR = transform.GetChild (2).transform;

		//set the range scales
		minR.localScale *= range;
		maxR.localScale *= range;

		//set the angle
		Material minMat = minR.GetComponent<SpriteRenderer> ().material;
		Material maxMat = maxR.GetComponent<SpriteRenderer> ().material;
		minMat.SetFloat ("_Angle", angle);
		maxMat.SetFloat ("_Angle", angle);

		//rotate the areas
		float rotateAngle = (180f-angle)/2f;
		minR.localRotation = Quaternion.AngleAxis (rotateAngle, Vector3.up) * Quaternion.AngleAxis (90f, Vector3.right);
		maxR.localRotation = Quaternion.AngleAxis (rotateAngle, Vector3.up) * Quaternion.AngleAxis (90f, Vector3.right);

		//adjust colliders
		//minCol = transform.GetChild (5).GetComponent<SphereCollider> ();
		//maxCol = transform.GetChild (6).GetComponent<SphereCollider> ();

		//minCol.radius *= range;
		//maxCol.radius *= range;
	}

	public void activateRangeSprites()
	{
		HideRangeSprites (false);
		StartCoroutine (ActivateRanges ());
	}


	void HideRangeSprites(bool val)
	{
		minR.gameObject.SetActive(!val);
		maxR.gameObject.SetActive(!val);
	}

	IEnumerator ActivateRanges()
	{
		yield return new WaitForSeconds (rangeAppearanceTime);

		HideRangeSprites (true);
	}
}
