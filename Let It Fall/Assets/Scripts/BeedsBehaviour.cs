using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeedsBehaviour : MonoBehaviour {

	float initY, initX, myX, myY, distX, tempX;
	Vector2 pos;
	bool fadeAwayInstruction = false;
	float minBoundary = -2.32f;
	float maxBoundary = 2.32f;
	bool soundPlayed = false;

	void Start () {

		//ballScript = GameObject.FindObjectOfType (typeof(BallBehaviour)) as BallBehaviour;

		initY = transform.parent.localPosition.y;
		initX = transform.localPosition.x;


	}


	void Update () {
		myX = Input.mousePosition.x;
		myY = Input.mousePosition.y;

	}

	void OnMouseDown () {
		soundPlayed = false;
		distX = Camera.main.ScreenToWorldPoint (Input.mousePosition).x - transform.parent.localPosition.x;
	}

	void OnMouseDrag () {
		print ("here");
		if (GameManager.IsBallFalling()) {

			//play sound
			if (!soundPlayed) {
				//GetComponent<AudioSource> ().Play();
				FindObjectOfType<AudioManager>().Play("Slider");
				soundPlayed = true;
			}

			tempX = Camera.main.ScreenToWorldPoint (new Vector2 (myX, myY)).x;


			//if (pos.x >= minBoundary && pos.x <= maxBoundary){
			if ((tempX - distX) > minBoundary && (tempX - distX) < maxBoundary ) {

				pos.x = tempX - distX;
				pos.y = transform.parent.localPosition.y;
				print ("pos x: " + pos.x);

				//transform.localPosition = (pos);
				transform.parent.localPosition = (pos);

			}
		}

		if (transform.root.FindChild ("Instruction").gameObject.activeSelf) {
			fadeAwayInstruction = true;
		}
	}

}
