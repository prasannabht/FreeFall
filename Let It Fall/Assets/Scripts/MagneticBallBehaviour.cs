using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticBallBehaviour : MonoBehaviour {

	float initX, initY, initXSlider, initYSlider;
	float maxX, maxXSlider;
	float distX;
	float myX, myY, tempX;
	Vector2 pos, posSlider;
	GameObject magneticSlider;
	bool magnetActive = false;
	bool fadeAwayInstruction = false;
	float alphaLevel = 1f;

	void Start () {
		initX = this.transform.localPosition.x;
		initY = this.transform.localPosition.y;
		if (initX < 0)
			maxX = initX + 1.2f;
		else
			maxX = initX - 1.2f;

		magneticSlider = transform.parent.FindChild ("MagneticSlider").gameObject;

		initXSlider = magneticSlider.transform.localPosition.x;
		initYSlider = magneticSlider.transform.localPosition.y;
		posSlider = magneticSlider.transform.localPosition;
		if (initXSlider < 0)
			maxXSlider = initXSlider - 0.6f;
		else
			maxXSlider = initXSlider + 0.6f;
	}
	
	// Update is called once per frame
	void Update () {
		myX = Input.mousePosition.x;
		myY = Input.mousePosition.y;

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
		
	void OnMouseDown () {
		distX = Camera.main.ScreenToWorldPoint (Input.mousePosition).x - this.transform.localPosition.x;
	}

	void OnMouseDrag () {

		if (GameManager.IsBallFalling()) {

			tempX = Camera.main.ScreenToWorldPoint (new Vector2 (myX, myY)).x;

			if (Mathf.Abs(initX) >= Mathf.Abs(tempX - distX) && Mathf.Abs(tempX - distX) >= Mathf.Abs(maxX)) {
				pos.x = tempX - distX;
				pos.y = initY;

				transform.localPosition = (pos);

			} 

			if (Mathf.Abs (transform.localPosition.x) <= Mathf.Abs (maxX) + 0.6f)
				magnetActive = true;
			else
				magnetActive = false;

			if (magnetActive) {
				if (Mathf.Abs (magneticSlider.transform.localPosition.x) < Mathf.Abs (maxXSlider)) {
					if (initXSlider < 0) {
						posSlider.x -= Time.deltaTime * 10;
					} else {
						posSlider.x += Time.deltaTime * 10;
					}
				}
				else
					posSlider.x = maxXSlider;

				posSlider.y = initYSlider;
				magneticSlider.transform.localPosition = (posSlider);
			}

			if (!transform.root.gameObject.name.Contains("Fake")) {
				if (transform.root.FindChild ("Instruction").gameObject.activeSelf) {
					fadeAwayInstruction = true;
				}
			}
		}
	}
}
