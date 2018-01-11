using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderCameraScript : MonoBehaviour {

	public Material mat;
	private float detectionStartRadius = 1.2f;
	private float detectedRadius = 0.9f;

	private float currentRadius = 0.0f;
	private GameObject player;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		mat.SetFloat ("_Radius", 2.0f);
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		Graphics.Blit (src, dest, mat);
	}

	public void startDetectionEffect(float detectionTime, float detectionMaxTime)
	{
		float val = comptueTimePercentage (detectionTime, detectionMaxTime);
		currentRadius = detectionStartRadius - val;

		mat.SetFloat ("_Radius", currentRadius);
	}

	public void startLostInterestEffect(float interestLostTime, float interestLostMaxTime)
	{
		float val = comptueTimePercentage (interestLostTime, interestLostMaxTime);
		currentRadius += val/2f;

		mat.SetFloat ("_Radius", currentRadius);
	}

	private float comptueTimePercentage(float time, float maxTime)
	{
		if (time == 0f) {
			return detectionStartRadius;
		} else {
			float result = (detectionStartRadius - detectedRadius);
			result *= time;
			result /= maxTime;

			return result;
		}
	}




}