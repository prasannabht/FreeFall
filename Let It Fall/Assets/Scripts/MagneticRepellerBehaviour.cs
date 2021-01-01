using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticRepellerBehaviour : MonoBehaviour {

	float myX, myY, initX, initY, maxX, minX;
	bool soundPlayed = false;
	float distX, tempX;
	Vector2 pos;
	bool autoMove = false;
	bool fadeAwayInstruction = false;
	float alphaLevel = 1f;

	void Start () {
		//ballScript = GameObject.FindObjectOfType (typeof(BallBehaviour)) as BallBehaviour;

		initY = this.transform.localPosition.y;
		initX = this.transform.localPosition.x;

		if (initX < 0) {
			maxX = 2.65f;
			minX = -1.6f;
		} else {
			maxX = 1.6f;
			minX = -2.65f;
		}

	}

	void Update () {
		myX = Input.mousePosition.x;
		myY = Input.mousePosition.y;

		if (autoMove && GameManager.IsBallFalling ()) {

			pos.y = initY;
			
			if (transform.localPosition.x > 0) {
				if (transform.localPosition.x < maxX) {
					pos.x += Time.deltaTime * 20f;
				} else {
					pos.x = maxX;
					autoMove = false;
				}
				transform.localPosition = (pos);
			} else {
				if (transform.localPosition.x > minX) {
					pos.x -= Time.deltaTime * 10f;
				} else {
					pos.x = minX;
					autoMove = false;
				}
				transform.localPosition = (pos);
			}
		}

		if (fadeAwayInstruction && GameManager.IsBallFalling()) {
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

	void OnMouseDown () {
		soundPlayed = false;
		distX = Camera.main.ScreenToWorldPoint (Input.mousePosition).x - this.transform.localPosition.x;
	}

	void OnMouseDrag () {
		if (GameManager.IsBallFalling ()) {
			//play sound
			if (!soundPlayed) {
				//GetComponent<AudioSource> ().Play();
				FindObjectOfType<AudioManager>().Play("Slider");
				soundPlayed = true;
			}

			tempX = Camera.main.ScreenToWorldPoint (new Vector2 (myX, myY)).x;

			if ((tempX - distX) >= minX && (tempX - distX) <= maxX) {

				if (Mathf.Abs (tempX - distX) > 0.5f) {
					autoMove = true; 
				} else {
					pos.x = tempX - distX;
					pos.y = initY;

					transform.localPosition = (pos);
				}
			} 

		}

		if (!transform.root.gameObject.name.Contains("Fake")) {
			if (transform.root.Find ("Instruction").gameObject.activeSelf) {
				fadeAwayInstruction = true;
			}
		}
	}
}
