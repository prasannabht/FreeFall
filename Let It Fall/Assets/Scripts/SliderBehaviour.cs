using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderBehaviour : MonoBehaviour {
	float myX;
	float myY;
	float distX;
	float initY;
	float initX;
	Vector2 pos;
	bool autoMove = false;
	//bool isMoving = true;
	bool fadeAwayInstruction = false;
	float alphaLevel = 1f;

	float minBoundary = -1.6f;
	float maxBoundary = 1.6f;

	float tempX;

	bool soundPlayed = false;

	//BallBehaviour ballScript;

	void Start () {

		//ballScript = GameObject.FindObjectOfType (typeof(BallBehaviour)) as BallBehaviour;
		
		initY = this.transform.localPosition.y;
		initX = this.transform.localPosition.x;


	}

	void Update () {
		myX = Input.mousePosition.x;
		myY = Input.mousePosition.y;

		//determine if game is stopped or paused
		//isMoving = !ballScript.getStopMovementFlag () && !ballScript.getGamePausedFlag ();

		if (autoMove && GameManager.IsBallFalling()){

			pos.y = initY;

			if (initX > 0f){ 
				if (transform.localPosition.x > 0f) {
					pos.x -= Time.deltaTime * 10f;
					transform.localPosition = (pos);
				} else {
					autoMove = false;
				}
			} else {
				if (transform.localPosition.x < 0f) {	
					pos.x += Time.deltaTime * 10f;
					transform.localPosition = (pos);
				}else {
					autoMove = false;
				}
			}

		}

		if (fadeAwayInstruction && GameManager.IsBallFalling()) {
			if (alphaLevel > 0.0f) {
				alphaLevel -= Time.deltaTime * 5;
				transform.root.FindChild ("Instruction").gameObject.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, alphaLevel);
			}

			if (alphaLevel <= 0f) {
				transform.root.FindChild ("Instruction").gameObject.SetActive(false);
				fadeAwayInstruction = false;
			}
		}


	}
		

	void OnMouseDown () {
		soundPlayed = false;
		distX = Camera.main.ScreenToWorldPoint (Input.mousePosition).x - this.transform.localPosition.x;
	}

	void OnMouseDrag () {
			
		if (GameManager.IsBallFalling()) {

			//play sound
			if (!soundPlayed) {
				//GetComponent<AudioSource> ().Play();
				FindObjectOfType<AudioManager>().Play("Slider");
				soundPlayed = true;
			}

			tempX = Camera.main.ScreenToWorldPoint (new Vector2 (myX, myY)).x;

			if (initX > 0f && (tempX - distX) < maxBoundary ) {
				if (tempX - distX < 1.55f) {
					autoMove = true;
				} else {
					pos.x = tempX - distX;
				}

				pos.y = initY;

				transform.localPosition = (pos);

			} //else if(initX < 0f && (tempX - distX) > minBoundary){
			else if(initX < 0f && (tempX - distX) > minBoundary){
				if (tempX - distX > -1.55f) {
					autoMove = true;
				} else {
					pos.x = tempX - distX;
				}

				pos.y = initY;

				transform.localPosition = (pos);
			}

		}

		if (transform.root.FindChild ("Instruction").gameObject.activeSelf) {
			fadeAwayInstruction = true;
		}
	}
		
}
