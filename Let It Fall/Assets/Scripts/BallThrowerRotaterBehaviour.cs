using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrowerRotaterBehaviour : MonoBehaviour {

	Vector3 pos;
	float ang;
	float angDiff;
	float initX;
	//BallBehaviour ballScript;
	bool autoMove = false;
	//bool isMoving = true;
	//bool isClicked = false;
	bool fadeAwayInstruction = false;
	float alphaLevel = 1f;

	bool soundPlayed = false;
	bool isNextStopDefined = false;
	int nextStop;
	float initAngle;
	float initAngle1;


	void Start () {
		//ballScript = GameObject.FindObjectOfType (typeof(BallBehaviour)) as BallBehaviour;
		//audioSource.clip = rotaterSound;
		initX = this.transform.position.x;
		initAngle = transform.rotation.eulerAngles.z;
		initAngle1 = transform.rotation.eulerAngles.z;
		//print ("Init: " + initAngle);
	}

	void Update () {
		//determine if game is stopped or paused
		//isMoving = !ballScript.getStopMovementFlag () && !ballScript.getGamePausedFlag ();

		if (autoMove && GameManager.IsBallFalling()) {
			if (!isNextStopDefined) {
				//print ("currAng: " + ang);
				nextStop = CommonFunctions.FindNextStop (initAngle, ang);
				isNextStopDefined = true;
				//print ("Go To: " + nextStop);
			}
//			if (ang > 0f ) {
//				if (ang > 85f && ang < 95f) {
//					ang = 90f;
//					autoMove = false;
//				} else {
//					if (ang < 90f)
//						ang += Time.deltaTime * 300f;
//					else
//						ang -= Time.deltaTime * 300f;
//				}
//			} else {
//				if (ang < -85f && ang > -95f) {
//					ang = -90f;
//					autoMove = false;
//				} else {
//					if (ang < -90f) {
//						ang += Time.deltaTime * 300f;
//					} else {
//						ang -= Time.deltaTime * 300f;
//					}
//				}
//			}

			if (ang < nextStop) {
				if (ang > nextStop - 5f) {
					ang = nextStop;
					autoMove = false;
					initAngle = ang;
					if (initAngle == 360)
						initAngle = 0;
				} else
					ang += Time.deltaTime * 300f;
			} else {
				if (ang < nextStop + 5f) {
					ang = nextStop;
					autoMove = false;
					initAngle = ang;
					if (initAngle == 360)
						initAngle = 0;
				} else
					ang -= Time.deltaTime * 300f;
			}

			transform.rotation = Quaternion.AngleAxis (ang, Vector3.forward);

			if (fadeAwayInstruction) {
				if (alphaLevel > 0.0f) {
					alphaLevel -= Time.deltaTime * 10f;
					transform.root.FindChild ("Instruction").gameObject.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, alphaLevel);
				}

				if (alphaLevel <= 0f) {
					transform.root.FindChild ("Instruction").gameObject.SetActive(false);
					fadeAwayInstruction = false;
				}
			}

		}
	}

	void OnMouseDrag () {

		if (GameManager.IsBallFalling()) {

			//play sound
			if (!soundPlayed) {
				//GetComponent<AudioSource> ().Play();
				FindObjectOfType<AudioManager>().Play("Rotater");
				soundPlayed = true;
			}

			pos = Camera.main.WorldToScreenPoint (transform.position);
			pos = Input.mousePosition - pos;

			//ang = Mathf.Atan2 (pos.y, pos.x) * Mathf.Rad2Deg;
			ang = CommonFunctions.GetAngle (pos);
//			if (ang < 0)
//				ang1 = ang + 360;
//			else
//				ang1 = ang;
			
			angDiff = Mathf.Abs(Mathf.Abs (initAngle1) - Mathf.Abs (ang));

			if (angDiff <= 90) {
				if (Mathf.Abs (ang) > Mathf.Abs (initAngle) + 5f || Mathf.Abs (ang) < Mathf.Abs (initAngle) - 5f) {
					autoMove = true;
					isNextStopDefined = false;
				}

				transform.rotation = Quaternion.AngleAxis (ang, Vector3.forward);
			}
		}

		if (!transform.root.gameObject.name.Contains ("Fake")) {
			if (transform.root.FindChild ("Instruction").gameObject.activeSelf) {
				fadeAwayInstruction = true;
			}
		}

	}
}
