using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour {

	float score = 0;
	float currSpeed;
	bool hasCrossedBall = false;

	void Start () {

	}
	

	void Update () {
	//void FixedUpdate () {

		currSpeed = GameManager.GetSpeed();
		if (GameManager.StopMovement()) {
			transform.Translate (0, Time.deltaTime * currSpeed, 0, Space.World);
		}

		if (gameObject.tag == "obstacle" && !hasCrossedBall) {
			if (transform.position.y > GameManager.topY - 1) {
				hasCrossedBall = true;
				++score;
				FindObjectOfType<GameManager>().UpdateScore(score);
			}
		}

		if (transform.position.y > GameManager.topY + 1) {
			Destroy (gameObject);
		}
	}
}