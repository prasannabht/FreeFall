using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSlideBegaviour : MonoBehaviour {

	Vector3 pos;

	bool hasRotated = false;
	bool isMoving = true;
	bool autoMove = false;
	bool autoSlide = false;
	bool soundPlayed = false;

	float distX;
	float myX;
	float myY;
	float tempX;
	float initY;
	float initZ;
	float initX;
	float minBoundary = -1.65f;
	float maxBoundary = 1.65f;

	bool fadeAwayInstruction = false;
	float alphaLevel = 1f;

	float ang;
	BallBehaviour ballScript;

	void Start () {
		ballScript = GameObject.FindObjectOfType (typeof(BallBehaviour)) as BallBehaviour;
		initY = this.transform.localPosition.y;
		initX = this.transform.localPosition.x;
		initZ = this.transform.localPosition.z;
	}

	void Update () {
		myX = Input.mousePosition.x;
		myY = Input.mousePosition.y;
		//determine if game is stopped or paused
		isMoving = !ballScript.getStopMovementFlag () && !ballScript.getGamePausedFlag ();

		if (autoMove) {
			//print (ang);
			if (ang > 0f ) {
				if (ang > 80f && ang < 100f) {
					ang = 90f;
					autoMove = false;
					hasRotated = true;
				} else {
					if (ang < 90f)
						ang += Time.deltaTime * 300f;
					else
						ang -= Time.deltaTime * 300f;
				}
			} else {
				if (ang < -80f && ang > -100f) {
					ang = -90f;
					autoMove = false;
					hasRotated = true;
				} else {
					if (ang < -90f) {
						ang += Time.deltaTime * 300f;
					} else {
						ang -= Time.deltaTime * 300f;
					}
				}
			}

			//print (ang);
			if (isMoving) {
				transform.rotation = Quaternion.AngleAxis (ang, Vector3.forward);
			}
		}

		if (autoSlide) {

			if (tempX > 0f) {
				if (tempX > maxBoundary) {
					tempX = maxBoundary;
					autoSlide = false;
				} else {
					tempX += Time.deltaTime * 10f;
				}
			} else {
				if (tempX < minBoundary) {
					tempX = minBoundary;
					autoSlide = false;
				} else {
					tempX -= Time.deltaTime * 10f;
				}
			}

			pos.x = tempX;
			pos.y = initY;
			pos.z = initZ;

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

	void OnMouseDown(){
		if (hasRotated) {
			distX = Camera.main.ScreenToWorldPoint (Input.mousePosition).x - this.transform.localPosition.x;
		}
	}

	void OnMouseDrag () {

		if (isMoving && !hasRotated) {

			//play sound
			if (!soundPlayed) {
				//GetComponent<AudioSource> ().Play();
				FindObjectOfType<AudioManager>().Play("Rotater");
				//soundPlayed = true;
			}

			pos = Camera.main.WorldToScreenPoint (transform.position);
			pos = Input.mousePosition - pos;

			ang = Mathf.Atan2 (pos.y, pos.x) * Mathf.Rad2Deg - 90f;
			if (ang <= -180f)
				ang = 360f + ang;

			if (ang > 10.0f && ang < 170.0f) {
				autoMove = true;
			}

			if (ang < -10.0f && ang > -170.0f) {

				autoMove = true;
			}

			transform.rotation = Quaternion.AngleAxis (ang, Vector3.forward);
		}

		if (isMoving && hasRotated && !autoSlide) {
			
			tempX = Camera.main.ScreenToWorldPoint (new Vector2 (myX, myY)).x;
			tempX = (tempX - distX);

			//play sound
			if (!soundPlayed) {
				//GetComponent<AudioSource> ().Play();
				FindObjectOfType<AudioManager>().Play("Slider");
				soundPlayed = true;
			}

			if (Mathf.Abs (tempX) > 0.2f)
				autoSlide = true;
			else {
				pos.x = tempX;
				pos.y = initY;
				pos.z = initZ;

				transform.localPosition = (pos);
			}


		}

		if (transform.root.FindChild ("Instruction").gameObject.activeSelf) {
			fadeAwayInstruction = true;
		}

	}
}
