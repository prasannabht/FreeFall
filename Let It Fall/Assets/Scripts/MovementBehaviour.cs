using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour {

	//float speed = 0.0f;
	GameObject ball;
	float score = 0;
	float currSpeed;
	bool hasCrossedBall = false;

	void Start () {
		ball = GameObject.Find ("Ball");
		//ballScript = GameObject.FindObjectOfType (typeof(BallBehaviour)) as BallBehaviour;
		//gameControllerScript = GameObject.FindObjectOfType (typeof(GameControllerBehaviour)) as GameControllerBehaviour;

	}
	

	void Update () {

		currSpeed = GameManager.GetSpeed();
		//print ("Is Ball Falling: " + gameControllerScript.isBallFalling);
		//if (ballScript.getStopMovementFlag () == false && ballScript.dontMove == false) {
		if (GameManager.IsBallFalling()) {

//			if (transform.name.Contains ("Cloud"))
//				currSpeed = currSpeed / 3;

			transform.Translate (0, Time.deltaTime * currSpeed, 0, Space.World);


		}

		if (!hasCrossedBall) {
			if (transform.position.y > ball.transform.position.y) {
				hasCrossedBall = true;
				++score;
				//ballScript.slowDown = false;
				//scoreObject.GetComponent<UpdateScoreBehaviour> ().updateScore (1);
				FindObjectOfType<GameManager>().UpdateScore(score);
			}
		}

		if (transform.position.y > ball.transform.position.y + 2.0f) {
			Destroy (gameObject);

		}
	}
}