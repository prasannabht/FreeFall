using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchBehaviour : MonoBehaviour {

	Vector3 pos;
	float ang;
	float initAngle;
	//BallBehaviour ballScript;
	Transform puncher;
	bool autoMove = false;
	//bool isMoving = true;
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
	int nextStop;
	bool isNextStopDefined = false;

	void Start () {
		//ballScript = GameObject.FindObjectOfType (typeof(BallBehaviour)) as BallBehaviour;
		puncher = transform.FindChild ("Puncher");
		initAngle = transform.rotation.eulerAngles.z;
		if (initAngle >= 180)
			initAngle = initAngle - 360;
		minPos = new Vector3(min, 0, puncher.position.z); 
		maxPos = new Vector3(max, 0, puncher.position.z);

	}

	void Update () {

		//determine if game is stopped or paused
		//isMoving = !ballScript.getStopMovementFlag () && !ballScript.getGamePausedFlag ();

		fraction = Mathf.Sin(Time.time * puncherSpeed);
		puncher.localPosition = Vector3.Lerp (minPos, maxPos, fraction);

		if (autoMove && GameManager.IsBallFalling()) {
			if (!isNextStopDefined) {
				//print ("currAng: " + ang);
				nextStop = CommonFunctions.FindNextStop (initAngle, ang);
				isNextStopDefined = true;
				//print ("Go To: " + nextStop);
			}

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

	void OnMouseDrag () {

		if (GameManager.IsBallFalling()) {

			//play sound
			if (!soundPlayed) {
				//GetComponent<AudioSource> ().Play();
				FindObjectOfType<AudioManager>().Play("Rotater");
				soundPlayed = true;
			}

			pos = Camera.main.WorldToScreenPoint (transform.position);
			pos = pos - Input.mousePosition;

			ang = CommonFunctions.GetAngle (pos);

//			if (initAngle == 360)
//				initAngle = 0;

			if ( Mathf.Abs(ang) > Mathf.Abs(initAngle) + 10f || Mathf.Abs(ang) < Mathf.Abs(initAngle) - 10f) {
				autoMove = true;
				isNextStopDefined = false;
			}

			transform.rotation = Quaternion.AngleAxis (ang, Vector3.forward);
		}

		if (!transform.root.gameObject.name.Contains ("Fake")) {
			if (transform.root.FindChild ("Instruction").gameObject.activeSelf) {
				fadeAwayInstruction = true;
			}
		}

	}

//	int FindNextStop(float initAngle, float currAngle){
//		int floorVal = Mathf.FloorToInt(currAngle / 90) * 90;
//		print("InitAng: "+ initAngle);
//		print ("Floor: " + floorVal);
//		if (currAngle > initAngle)
//			floorVal = floorVal + 90;
//		return floorVal;
//	}
//
//	float GetAngle(Vector3 pos){
//		float myAng;
//		myAng = Mathf.Atan2 (pos.y, pos.x) * Mathf.Rad2Deg;
//
//		if (myAng < 0 && myAng > -180f) {
//			myAng = 360 + myAng;
//		}
//
//		return myAng;
//	}
}
