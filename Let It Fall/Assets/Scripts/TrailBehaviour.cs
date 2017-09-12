using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailBehaviour : MonoBehaviour {

	bool isClicked = false;
	bool stopTrail = false;
	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
			if (Input.GetMouseButtonDown (0))
				isClicked = true;
			if (Input.GetMouseButtonUp (0))
				isClicked = false;


		if (isClicked) {
			rb.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		}
	}
}
