using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopperBehaviour : MonoBehaviour {

	bool autoMove = false;
	//bool isMoving = true;
	bool hasCollided = false;
	//BallBehaviour ballScript;
	bool fadeAwayInstruction = false;
	float alphaLevel = 1f;

	float minBoundary, maxBoundary;
	float initX, initY, myX, myY, distX, tempX;
	Vector2 pos;

	// Use this for initialization
	void Start () {
		//ballScript = GameObject.FindObjectOfType (typeof(BallBehaviour)) as BallBehaviour;
		initX = transform.localPosition.x;
		initY = transform.localPosition.y;
		if (initX < 0) {
			minBoundary = -0.6f;
			maxBoundary = 0.6f;
		} 
	}
	
	// Update is called once per frame
	void Update () {
		myX = Input.mousePosition.x;
		myY = Input.mousePosition.y;

		//print (transform.localPosition.x);
		//determine if game is stopped or paused
		//isMoving = !ballScript.getStopMovementFlag () && !ballScript.getGamePausedFlag () && !hasCollided;

		if (autoMove && GameManager.IsBallFalling()) {
			pos.y = initY;

			if (initX < 0f){ 
				
				pos.x += Time.deltaTime * 10f;
				if (pos.x > maxBoundary) {
					pos.x = maxBoundary;
					autoMove = false;
				}
				transform.localPosition = (pos);
			}
			if (fadeAwayInstruction) {
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


	}

	void OnMouseDown(){
		distX = Camera.main.ScreenToWorldPoint (Input.mousePosition).x - this.transform.localPosition.x;
	}

	void OnMouseDrag () {

		if (GameManager.IsBallFalling()) {
			tempX = Camera.main.ScreenToWorldPoint (new Vector2 (myX, myY)).x;

			if(initX < 0f && (tempX - distX) > minBoundary){
				if (tempX - distX > -0.4f) {
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

	public void stopMovement(){
		hasCollided = true;
	}
}
