using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour {

	bool fadeAwayInstruction = false;
	float alphaLevel = 1f;

	bool isBombTouched = false;
	float bombSpeed = 0.06f;
	bool soundPlayed = false;

	void Update () {

		if (isBombTouched) {
			//determine if game is stopped or paused
			//isMoving = !ballScript.getStopMovementFlag () && !ballScript.getGamePausedFlag ();

			if (GameManager.IsBallFalling()) {

				//play sound
				if (!soundPlayed) {
					//GetComponent<AudioSource> ().Play();
					FindObjectOfType<AudioManager>().Play("Bomb");
					soundPlayed = true;
				}

				if (transform.localScale.x > 0f) {
					transform.localScale -= new Vector3 (bombSpeed, bombSpeed, bombSpeed);
				}
				if (transform.localScale.x <= 0f) {
					Destroy (gameObject);
				}
			}
		}


		if (fadeAwayInstruction) {
			if (alphaLevel > 0.0f) {
				alphaLevel -= Time.deltaTime * 10;
				transform.root.Find ("Instruction").gameObject.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, alphaLevel);
			}

			if (alphaLevel <= 0f) {
				transform.root.Find ("Instruction").gameObject.SetActive(false);
				fadeAwayInstruction = false;
			}
		}
	}

	void OnMouseDown () {
		isBombTouched = true;

		if (transform.root.Find ("Instruction").gameObject.activeSelf) {
			fadeAwayInstruction = true;
		}
	}
}
