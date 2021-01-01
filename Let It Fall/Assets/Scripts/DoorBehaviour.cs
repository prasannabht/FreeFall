using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour {

	Vector3 pos;
	float ang;
	float initAng;
	float angAbs;
	bool autoMove = false;
	//bool isMoving = true;
	//BallBehaviour ballScript;

	bool fadeAwayInstruction = false;
	float alphaLevel = 1f;

	bool soundPlayed = false;

	void Start(){
		
		initAng = transform.rotation.eulerAngles.z;
		//initAng = Mathf.Atan2 (transform.position.y, transform.position.x) * Mathf.Rad2Deg;
		if (initAng > 180f) {
			initAng = initAng - 360;
		}
		//ballScript = GameObject.FindObjectOfType (typeof(BallBehaviour)) as BallBehaviour;
	}

	void Update(){
		//determine if game is stopped or paused
		//isMoving = !ballScript.getStopMovementFlag () && !ballScript.getGamePausedFlag ();

		if (autoMove && GameManager.IsBallFalling()) {
			if (initAng > 0) {
				if (ang < 170.0f)
					ang += Time.deltaTime*300f;
				else
					autoMove = false;
			} else {
				if (ang > -170.0f)	
					ang -= Time.deltaTime*300f;
				else
					autoMove = false;
			}
			transform.rotation = Quaternion.AngleAxis (ang, Vector3.forward);

			if (fadeAwayInstruction) {
				if (alphaLevel > 0.0f) {
					alphaLevel -= Time.deltaTime * 5;
					transform.root.Find ("Instruction").gameObject.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, alphaLevel);
				}

				if (alphaLevel <= 0f) {
					transform.root.Find ("Instruction").gameObject.SetActive(false);
					fadeAwayInstruction = false;
				}
			}
		}
	}

	void OnMouseDrag () {

		if (GameManager.IsBallFalling()) {
			pos = Camera.main.WorldToScreenPoint (transform.position);

			//play sound
			if (!soundPlayed) {
				//GetComponent<AudioSource> ().Play();
				FindObjectOfType<AudioManager>().Play("Door");
				soundPlayed = true;
			}

			if (initAng > 0f) {
				pos = pos - Input.mousePosition;
				ang = Mathf.Atan2 (pos.y, pos.x) * Mathf.Rad2Deg;

				if (ang > 30f && ang < 170f) {
					if (ang > 33f) {
						autoMove = true;
					} else {
						transform.rotation = Quaternion.AngleAxis (ang, Vector3.forward);
					}
				}

			} else {
				pos = Input.mousePosition - pos;
				ang = Mathf.Atan2 (pos.y, pos.x) * Mathf.Rad2Deg;
				if (ang < -30f && ang > -170f) {
					if (ang < -33f) {
						autoMove = true;
					} else {
						transform.rotation = Quaternion.AngleAxis (ang, Vector3.forward);
					}

				}

			}

		}

		if (transform.root.Find ("Instruction").gameObject.activeSelf) {
			fadeAwayInstruction = true;
		}
	}
}
