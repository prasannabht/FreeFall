using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailBehaviour : MonoBehaviour {

	bool isClicked = false;
	[HideInInspector]
	public bool isEnabled = true;
	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		gameObject.GetComponent<TrailRenderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		//if (isEnabled) {
			if (Input.GetMouseButtonDown (0)) {
				isClicked = true;
				gameObject.GetComponent<TrailRenderer> ().enabled = true;
			}
			if (Input.GetMouseButtonUp (0)) {
				isClicked = false;
				gameObject.GetComponent<TrailRenderer> ().enabled = false;
			}

			if (isClicked) {
				rb.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			}
		//} 

		if(!isEnabled && !isClicked){
			gameObject.GetComponent<TrailRenderer> ().enabled = false;
		}
	}
		
}
