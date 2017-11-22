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
	Vector3 currScale;

	void Start () {
		alphaLevel = 0;
		currScale = transform.localScale;
	}
	

	void Update () {
		if (GameManager.IsBallFalling() || !UIManager.GameStarted()) {

			if (alphaLevel < 1) {
				alphaLevel += Time.deltaTime * 5;
				gameObject.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, alphaLevel);
			}

			if (FindObjectOfType<UIManager> ().IsSuperSpeed) {
				if (transform.localScale.y < 1.3f) {
					currScale.y += Time.deltaTime;
					transform.localScale = currScale;
				} 
			} else {
				if (transform.localScale.y > 1f) {
					currScale.y -= Time.deltaTime;
					transform.localScale = currScale;
				}
			}
		}

		if (!GameManager.IsBallFalling() && UIManager.GameStarted()) {
			
			if (alphaLevel > 0) {
				alphaLevel -= Time.deltaTime * 5f;
				gameObject.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, alphaLevel);
			}
		} 

		transform.localEulerAngles = new Vector3 (0, 0, Mathf.PingPong(Time.time*moveSpeed, rot) - rot/2);

	}
}
