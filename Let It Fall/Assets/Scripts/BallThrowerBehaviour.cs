using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrowerBehaviour : MonoBehaviour {

	float ballThrowerSpeed = 5f;
	float ballThrowerAngle = 27f * Mathf.Deg2Rad;
	int xDir = 1;

	// Use this for initialization
	void Start () {
		//transform.Rotate (Vector3.forward);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Time.deltaTime * Mathf.Cos(ballThrowerAngle) * ballThrowerSpeed * xDir, Time.deltaTime * Mathf.Sin(ballThrowerAngle) * ballThrowerSpeed, 0);

		if (!GameManager.IsBallFalling ()) {
			this.gameObject.GetComponent<Collider2D> ().enabled = false;
		}

		if (Mathf.Abs (transform.position.x) > 3.5f)
			Destroy (gameObject);
	}

	void OnCollisionEnter2D(Collision2D Col){
		if (Col.gameObject.name == "BallThrower Rotater") {
			xDir = -1;
		}
	}
}
