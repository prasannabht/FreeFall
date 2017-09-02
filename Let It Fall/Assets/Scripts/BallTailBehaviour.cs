using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTailBehaviour : MonoBehaviour {

	Vector3 size;
	float initYSize;
	BallBehaviour ballScript;

	float moveSpeed = 30f;
	float rot = 5f;

	void Start () {
		size = transform.localScale;
		initYSize = transform.localScale.y;

		size.y = 0.0f;
		transform.localScale = size;

		ballScript = GameObject.FindObjectOfType (typeof(BallBehaviour)) as BallBehaviour;
	}
	

	void Update () {
		if (ballScript.getStopMovementFlag () == false) {
			if (transform.localScale.y < initYSize) {
				size.y += Time.deltaTime;
				transform.localScale = size;
			}

			transform.localEulerAngles = new Vector3 (0, 0, Mathf.PingPong(Time.time*moveSpeed, rot) - rot/2);

		}

		if (ballScript.getStopMovementFlag () == true) {
			
			if (transform.localScale.y > 0.0f) {
				size.y -= Time.deltaTime * 4;
				transform.localScale = size;
			}
		}


	}
}
