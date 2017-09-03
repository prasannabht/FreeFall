using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaterBehaviour : MonoBehaviour {

	Vector3 pos;
	Vector3 rotation;
	float ang;
	BallBehaviour ballScript;
	bool autoMove = false;
	bool isMoving = true;
	//bool isClicked = false;
	bool fadeAwayInstruction = false;
	float alphaLevel = 1f;

	bool soundPlayed = false;


	void Start () {
		ballScript = GameObject.FindObjectOfType (typeof(BallBehaviour)) as BallBehaviour;
		//audioSource.clip = rotaterSound;

	}

	void Update(){

		//determine if game is stopped or paused
		isMoving = !ballScript.getStopMovementFlag () && !ballScript.getGamePausedFlag ();

		if (autoMove) {

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
				} else {
					
					if (ang < -90f) {
						ang += Time.deltaTime * 300f;
					} else {
						ang -= Time.deltaTime * 300f;
					}
				}
			}

			if (isMoving) {
				transform.rotation = Quaternion.AngleAxis (ang, Vector3.forward);
			}
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

			ang = Mathf.Atan2 (pos.y, pos.x) * Mathf.Rad2Deg;
			if (ang > 10.0f && ang < 170.0f) {
				autoMove = true;
			}

			if (ang < -10.0f && ang > -170.0f) {
				autoMove = true;
			}
			
			transform.rotation = Quaternion.AngleAxis (ang, Vector3.forward);
		}

		if (transform.root.FindChild ("Instruction").gameObject.activeSelf) {
			fadeAwayInstruction = true;
		}

	}

	//void OnMouseDown(){
		//isClicked = true;
	//}
		
}
