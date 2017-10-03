using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchBehaviour : MonoBehaviour {

	Vector3 pos;
	float ang;
	float zRotation;
	BallBehaviour ballScript;
	Transform puncher;
	bool autoMove = false;
	bool isMoving = true;
	//bool isClicked = false;
	bool fadeAwayInstruction = false;
	float alphaLevel = 1f;

	bool soundPlayed = false;

	float max = 0.5f;
	float min = -2f;
	Vector3 minPos;
	Vector3 maxPos;
	float fraction;
	float puncherSpeed = 5f;

	void Start () {
		ballScript = GameObject.FindObjectOfType (typeof(BallBehaviour)) as BallBehaviour;
		puncher = transform.FindChild ("Puncher");
		zRotation = transform.rotation.eulerAngles.z;

		minPos = new Vector3(min, 0, 0); 
		maxPos = new Vector3(max, 0, 0);


	}

	void Update () {

		//determine if game is stopped or paused
		isMoving = !ballScript.getStopMovementFlag () && !ballScript.getGamePausedFlag ();

		fraction = Mathf.Sin(Time.time * puncherSpeed);
		puncher.localPosition = Vector3.Lerp (minPos, maxPos, fraction);

		if (autoMove && isMoving) {
			if (ang > 0f ) {
				if (ang > 85f && ang < 95f) {
					ang = 90f;
					autoMove = false;
				} else {
					if (ang < 90f)
						ang += Time.deltaTime * 300f;
					else
						ang -= Time.deltaTime * 300f;
				}
			} else {
				if (ang < -85f && ang > -95f) {
					ang = -90f;
					autoMove = false;
				}
				else if(ang < -265f && ang > -275f){
					ang = -270f;
					autoMove = false;
				}
				else {

					if (ang < -90f) {
						ang += Time.deltaTime * 300f;
					} else {
						ang -= Time.deltaTime * 300f;
					}
				}
			}
				
			transform.rotation = Quaternion.AngleAxis (ang, Vector3.back);


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

	void OnMouseDrag () {

		if (isMoving) {

			//play sound
			if (!soundPlayed) {
				//GetComponent<AudioSource> ().Play();
				FindObjectOfType<AudioManager>().Play("Rotater");
				soundPlayed = true;
			}

			pos = Camera.main.WorldToScreenPoint (transform.position);
			pos = Input.mousePosition - pos;

			ang = 180f - Mathf.Atan2 (pos.y, pos.x) * Mathf.Rad2Deg;
			if (ang > 180f)
				ang = ang - 360f;

			if ( Mathf.Abs(ang) > Mathf.Abs(zRotation) + 10f || Mathf.Abs(ang) < Mathf.Abs(zRotation) - 10f) {
				autoMove = true;
			}

			transform.rotation = Quaternion.AngleAxis (ang, Vector3.back);
		}

		if (transform.root.FindChild ("Instruction").gameObject.activeSelf) {
			fadeAwayInstruction = true;
		}

	}
}
