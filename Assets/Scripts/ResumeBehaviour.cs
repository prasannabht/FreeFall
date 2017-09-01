using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeBehaviour : MonoBehaviour {

	PauseBehaviour pauseScript;
	BallBehaviour ballScript;

	bool isResumeClicked = false;
	float alphaLevel = 1.0f;

	void Start(){
		pauseScript = GameObject.FindObjectOfType (typeof(PauseBehaviour)) as PauseBehaviour;
		ballScript = GameObject.FindObjectOfType (typeof(BallBehaviour)) as BallBehaviour;
	}

	void Update(){
		if (isResumeClicked) {

			if (alphaLevel > 0.0f) {
				alphaLevel -= Time.deltaTime * 5;
				foreach (SpriteRenderer mySprite in transform.parent.GetComponentsInChildren<SpriteRenderer>()) {
					mySprite.color = new Color (1f, 1f, 1f, alphaLevel);
				}
			}

			if (alphaLevel <= 0f) {
				pauseScript.disableResumeObjects ();
				ballScript.setGamePausedFlag (false);
			}
		}
	}

	void OnMouseDown () {
		Time.timeScale = 1;
		isResumeClicked = true;
	}
}
