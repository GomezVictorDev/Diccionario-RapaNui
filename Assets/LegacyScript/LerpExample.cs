using UnityEngine;
using System.Collections;

public class LerpExample : MonoBehaviour {
	float lerpTime = 3f;
	float currentLerpTime;

	float moveDistance = 10f;

	Vector3 startPos;
	Vector3 endPos;

	protected void Start() {
		startPos = transform.position;
		endPos = transform.position + transform.up * moveDistance;
	}

	protected void Update() {
		//reset when we press spacebar
		if (Input.GetKeyDown(KeyCode.Space)) {
			currentLerpTime = 0f;
		}

		//increment timer once per frame
		currentLerpTime += Time.deltaTime;
		if (currentLerpTime > lerpTime) {
			currentLerpTime = lerpTime;
		}

		//lerp!

		float t = currentLerpTime / lerpTime;
		Debug.Log ("Current Lerp Time: " + currentLerpTime);
		Debug.Log ("Lerp Time: " + lerpTime);
		Debug.Log ("Percentage: " + t);
		t = t * t * t * (t * (6f * t - 15f) + 10f);
		//t= t*t * (3f - 2f*t);
		//t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
		transform.position = Vector3.Lerp(startPos, endPos, t);
	}
}
