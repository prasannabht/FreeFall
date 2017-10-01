using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour {

	//float speed = 0.0f;

	float currSpeed;

	BallBehaviour ballScript;

	void Start () {
		ballScript = GameObject.FindObjectOfType (typeof(BallBehaviour)) as BallBehaviour;

	}
	

	void Update () {

		currSpeed = ballScript.speed;

		if (ballScript.getStopMovementFlag () == false && ballScript.dontMove == false) {

			if (transform.name.Contains ("Cloud"))
				currSpeed = currSpeed / 3;

			transform.Translate (0, Time.deltaTime * currSpeed, 0, Space.World);

		}
	}
}