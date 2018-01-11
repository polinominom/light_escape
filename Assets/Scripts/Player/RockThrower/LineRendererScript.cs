using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererScript : MonoBehaviour 
{

	private LineRenderer lineRenderer;
	private Vector3[] elements;
	Vector3 _drawto;

	void Start () 
	{
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.enabled = false;
		elements = new Vector3[3];
	}

	void Update()
	{
		if (lineRenderer.enabled) 
		{
			DrawLine (transform.parent.parent.position, _drawto);
		}
	}

	public void DrawLine(Vector3 _from, Vector3 _to)
	{
		lineRenderer.enabled = true;
		Vector3 total = _from + _to;
		float dist = Vector3.Distance (_from, _to);

		elements [0] = _from;
		elements [1] = new Vector3 (total.x/2f, total.y/2f + dist/4f, total.z/2f);
		elements [2] = _to;

		lineRenderer.SetPositions(elements);


		this._drawto = _to;
	}

	public void HideLine()
	{
		lineRenderer.enabled = false;
	}


}
