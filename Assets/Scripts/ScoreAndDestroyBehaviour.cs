using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAndDestroyBehaviour : MonoBehaviour {

	GameObject ball;
	GameObject scoreObject;
	bool hasCrossed = false;
	float score = 0;
	BallBehaviour ballScript;

	void Start(){
		ball = GameObject.Find ("Ball");
		scoreObject = GameObject.Find ("GameController");
		ballScript = GameObject.FindObjectOfType (typeof(BallBehaviour)) as BallBehaviour;
	}

	// Update is called once per frame
	void Update () {
		if(!hasCrossed){
			if (transform.position.y > ball.transform.position.y) {
				hasCrossed = true;
				++score;
				//ballScript.slowDown = false;
				scoreObject.GetComponent<UpdateScoreBehaviour> ().updateScore (1);
			}
		}

		if (transform.position.y > ball.transform.position.y + 2.0f) {
			Destroy (gameObject);

		}
	}
}
