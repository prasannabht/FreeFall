using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerBehaviour : MonoBehaviour {

	float spinSpeed = 200f;

	bool isCollision = false;
	StopperBehaviour stopperScript;
	// Use this for initialization
	void Start () {
		stopperScript = GameObject.FindObjectOfType (typeof(StopperBehaviour)) as StopperBehaviour;
	}
	
	// Update is called once per frame
	void Update () {
		//print (transform.rotation.eulerAngles.z);
		if (isCollision == false) {
			transform.Rotate (Vector3.back * spinSpeed * Time.deltaTime);
		} else {
			if (transform.rotation.eulerAngles.z < 180f) {
				isCollision = false;
				transform.Rotate (Vector3.back * spinSpeed * Time.deltaTime);
			} else {
				stopperScript.stopMovement ();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		isCollision = true;

	}
}
