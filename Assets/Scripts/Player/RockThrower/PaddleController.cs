using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PaddleController : MonoBehaviour {

	public float closeRange;

	private RockThrowerScript rock;
	private RockRangeAreaScript rangeArea;

	private static float fixThickness = 0.005f;
	private static float radiusModifier = 0.006f;
	private static float startRadius = 0.01f;
	private Material throwerMat;

	private PaddleCollider paddleCollider;

	private float range;

	private List<GameObject> enemiesWithinRange;
	Vector3 newPosition;
	Vector3 startPosition;

	private SpriteRenderer sRenderer;
	void Start () 
	{
		newPosition = transform.position;
		startPosition = transform.position;

		rock = GetComponentInParent<RockThrowerScript> ();
		range = rock.effectRange;

		throwerMat = GetComponent<SpriteRenderer> ().material;

		throwerMat.SetFloat ("_Thickness", fixThickness);
		throwerMat.SetFloat ("_Radius", 0f);

		paddleCollider = transform.parent.GetComponentInChildren<PaddleCollider> ();

		enemiesWithinRange = new List<GameObject> ();

//		line = transform.parent.GetComponentInChildren<LineRendererScript> ();
		rangeArea = GetComponentInChildren<RockRangeAreaScript> ();
		sRenderer = GetComponent<SpriteRenderer> ();
		sRenderer.enabled = false;


	}
	void Update()
	{
		if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0) && rock.isThrowable ())
		{
			RaycastHit hit;
			int layermask = 1 << 8;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask))
			{
				newPosition = new Vector3 (hit.point.x, 0.1f, hit.point.z); 
				transform.position = newPosition;
				paddleCollider.transform.position = newPosition;
				rangeArea.ActivateRange (true);
//				line.DrawLine(transform.parent.parent.position, newPosition);
//				sRenderer.enabled = true;
			}
		}

		if (newPosition != transform.position) 
		{
			transform.position = newPosition;
			paddleCollider.transform.position = newPosition;
		}
	}


	public void ActivateRange(bool val)
	{
		
		float radius = 0;
		if (val) 
		{
			radius += startRadius + range * radiusModifier;
			throwerMat.SetFloat ("_Radius", radius);
			GetComponent<SpriteRenderer> ().enabled = true;
		} 
		else
		{
			throwerMat.SetFloat ("_Radius", 0f);
			GetComponent<SpriteRenderer> ().enabled = false;
		}

		paddleCollider.ActivateCollider (val);

		rangeArea.ActivateRange (val);

	}


	public void AddEnemy(GameObject enemy)
	{
		enemiesWithinRange.Add (enemy);
		if (rock.isThrowable ()) {
			enemy.GetComponent<Enemy> ().ActivateHighlight (true);
		}
	}

	public void RemoveEnemy(GameObject enemy)
	{
		

		enemy.GetComponent<Enemy> ().ActivateHighlight (false);
		enemiesWithinRange.Remove (enemy);
	}


	public void RockHit(Vector3 hitPosition)
	{
		//reset the rock throwing meta
		ActivateRange (false);


		foreach (GameObject enemy in enemiesWithinRange) 
		{
			Vector3 pos = (enemy.transform.position - hitPosition ).normalized * closeRange;	
			enemy.GetComponent<EnemyWhistleController> ().HearWhistle (hitPosition + pos);
		}
	}


	public void AreaReset()
	{
		
		newPosition = startPosition;

//		line.HideLine ();

		foreach (GameObject enemy in enemiesWithinRange) 
		{
			enemy.GetComponent<Enemy> ().ActivateHighlight (false);
		}

		enemiesWithinRange.Clear ();

		rock.SetThrowable (false);
	}

	public void SetNewPosition(Vector3 pos)
	{
		this.newPosition = pos;
	}

}