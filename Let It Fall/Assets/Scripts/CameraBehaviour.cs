using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

	private Transform ball;
	private float initDistY;
	// Use this for initialization
	void Start () {
		ball = GameObject.Find ("Ball").transform;
		initDistY = transform.position.y - ball.position.y; 
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 camPos = ball.position;
		camPos.x = transform.position.x;
		camPos.z = transform.position.z;
		camPos.y = camPos.y + initDistY;
		transform.position = camPos;
	}
}
