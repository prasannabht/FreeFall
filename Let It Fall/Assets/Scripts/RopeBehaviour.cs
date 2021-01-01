using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeBehaviour : MonoBehaviour {

	//public bool isRopeTouched = false;
	bool isClicked = false; 
	bool fadeAwayInstruction = false;
	float alphaLevel = 1f;
	bool soundPlayed = false;

	// Use this for initialization
	void Start () {
		isClicked = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			isClicked = true;
		}
		if (Input.GetMouseButtonUp (0))
			isClicked = false;

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

	void OnMouseOver(){
		//isRopeTouched = true;
		//FindObjectOfType<RopeRotaterBehaviour>().isRopeTouched = true;


		if (isClicked){

			//play sound
			if (!soundPlayed) {
				//GetComponent<AudioSource> ().Play();
				FindObjectOfType<AudioManager>().Play("Rope");
				soundPlayed = true;
			}

			//isClicked = true;
			transform.GetComponentInParent<RopeRotaterBehaviour> ().isRopeTouched = true;
			if (transform.root.Find ("Instruction").gameObject.activeSelf) {
				fadeAwayInstruction = true;
			}

			//hide rope
			gameObject.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, 0);
			gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		}
	}
}
