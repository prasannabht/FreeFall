using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour {

	bool isMoving = true;
	bool fadeAwayInstruction = false;
	float alphaLevel = 1f;
	float bombAlphaLevel = 1f;


	bool isBombTouched = false;
	float bombSpeed = 0.06f;
	bool soundPlayed = false;
	BallBehaviour ballScript;

	// Use this for initialization
	void Start () {
		ballScript = GameObject.FindObjectOfType (typeof(BallBehaviour)) as BallBehaviour;

	}
	
	// Update is called once per frame
	void Update () {

		if (isBombTouched) {
			//determine if game is stopped or paused
			isMoving = !ballScript.getStopMovementFlag () && !ballScript.getGamePausedFlag ();

			if (isMoving) {

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
				transform.root.FindChild ("Instruction").gameObject.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, alphaLevel);
			}

			if (alphaLevel <= 0f) {
				transform.root.FindChild ("Instruction").gameObject.SetActive(false);
				fadeAwayInstruction = false;
			}
		}
	}

	void OnMouseDown () {
		isBombTouched = true;

		if (transform.root.FindChild ("Instruction").gameObject.activeSelf) {
			fadeAwayInstruction = true;
		}
	}
}
