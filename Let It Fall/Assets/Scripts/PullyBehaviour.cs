using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullyBehaviour : MonoBehaviour {

	bool autoMove = false;
	bool isMoving = true;

	BallBehaviour ballScript;
	bool fadeAwayInstruction = false;
	float alphaLevel = 1f;

	Transform Slider;
	float maxBoundary, minBoundary;
	float initX, initY, myX, myY, distY, tempY, initSliderY, initSliderX;
	Vector2 pos, posSlider;

	bool soundPlayed = false;

	// Use this for initialization
	void Start () {
		ballScript = GameObject.FindObjectOfType (typeof(BallBehaviour)) as BallBehaviour;
		initX = transform.localPosition.x;
		initY = transform.localPosition.y;

		Slider = transform.parent.FindChild ("Pully Slider");
		initSliderY = Slider.localPosition.y;
		initSliderX = Slider.localPosition.x;

		minBoundary = -0.314f;
		maxBoundary = -0.834f;
		 
	}

	// Update is called once per frame
	void Update () {
		myX = Input.mousePosition.x;
		myY = Input.mousePosition.y;

		//determine if game is stopped or paused
		isMoving = !ballScript.getStopMovementFlag () && !ballScript.getGamePausedFlag ();

		if (autoMove && isMoving) {
			pos.x = initX;
			pos.y -= Time.deltaTime * 10f;

			if (pos.y + initY < maxBoundary) {
				pos.y = maxBoundary;
				autoMove = false;
			}

			transform.localPosition = (pos);

			posSlider.y = initSliderY;
			if (initSliderX > 0f)
				posSlider.x = initSliderX + Mathf.Abs(initY - pos.y);
			else
				posSlider.x = initSliderX - Mathf.Abs(initY - pos.y);


			Slider.localPosition = (posSlider);

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
		soundPlayed = false;
		distY = Camera.main.ScreenToWorldPoint (Input.mousePosition).y - this.transform.localPosition.y;
	}

	void OnMouseDrag () {

		if (isMoving) {

			//play sound
			if (!soundPlayed) {
				//GetComponent<AudioSource> ().Play();
				FindObjectOfType<AudioManager>().Play("Pully");
				soundPlayed = true;
			}


			tempY = Camera.main.ScreenToWorldPoint (new Vector2 (myX, myY)).y;

			if ((tempY - distY) > minBoundary)
				pos.y = minBoundary;
			else if ((tempY - distY) < maxBoundary)
				pos.y = maxBoundary;
			else if ((tempY - distY) < -0.44)
				autoMove = true;
			else 
				pos.y = tempY - distY;

			pos.x = initX;
			transform.localPosition = (pos);


			posSlider.y = initSliderY;
			if (initSliderX > 0f)
				posSlider.x = initSliderX + Mathf.Abs(initY - pos.y);
			else
				posSlider.x = initSliderX - Mathf.Abs(initY - pos.y);
			

			Slider.localPosition = (posSlider);

		}

		if (transform.root.FindChild ("Instruction").gameObject.activeSelf) {
			fadeAwayInstruction = true;
		}
	}
		
}
