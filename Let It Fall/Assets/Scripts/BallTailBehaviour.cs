using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTailBehaviour : MonoBehaviour {

	//Vector3 size;
	//float initYSize;
	//BallBehaviour ballScript;
	float alphaLevel;
	float moveSpeed = 30f;
	float rot = 4f;

	void Start () {
		//size = transform.localScale;
		//initYSize = transform.localScale.y;

		//size.y = 0.0f;
		//transform.localScale = size;
		alphaLevel = 0;
		//ballScript = GameObject.FindObjectOfType (typeof(BallBehaviour)) as BallBehaviour;
	}
	

	void Update () {
		if (GameManager.IsBallFalling() || !UIManager.GameStarted()) {
//			if (transform.localScale.y < initYSize) {
//				size.y += Time.deltaTime;
//				transform.localScale = size;
//			}

			if (alphaLevel < 1) {
				alphaLevel += Time.deltaTime * 5;
				gameObject.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, alphaLevel);
				print (gameObject.GetComponent<SpriteRenderer> ().color.a);
			}


			transform.localEulerAngles = new Vector3 (0, 0, Mathf.PingPong(Time.time*moveSpeed, rot) - rot/2);

		}

		if (!GameManager.IsBallFalling() && UIManager.GameStarted()) {
			
//			if (transform.localScale.y > 0.0f) {
//				size.y -= Time.deltaTime * 4;
//				transform.localScale = size;
//			}
//
			if (alphaLevel > 0) {
				alphaLevel -= Time.deltaTime * 5f;
				gameObject.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, alphaLevel);
			}
		} 


	}
}
