using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidBehaviour : MonoBehaviour {
	float InitPosY, SuperSpeedPosY;
	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (0, GameManager.topY - 1, 0);
		InitPosY = transform.position.y;
		SuperSpeedPosY = GameManager.topY - 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
		//wiggling motion
		if (GameManager.IsBallFalling () || !UIManager.GameStarted()) {
			if (FindObjectOfType<UIManager> ().IsSuperSpeed) {
				if (transform.position.y > SuperSpeedPosY) {
					transform.Translate (0, -Time.deltaTime, 0, Space.World);	
				}
			} else {
				if (transform.position.y < InitPosY) {
					transform.Translate (0, Time.deltaTime, 0, Space.World);	
				}
			}
		}
	}
}
