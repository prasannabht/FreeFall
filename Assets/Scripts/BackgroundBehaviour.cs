using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehaviour : MonoBehaviour {

	public Transform background1;
	public Transform background2;

	float shiftHeight = 10.0f;

	void Update () {

		if(background1.transform.position.y > Camera.main.transform.position.y + shiftHeight){
			background1.transform.position = new Vector3 (0, Camera.main.transform.position.y - shiftHeight, 0);
		}

		if(background2.transform.position.y > Camera.main.transform.position.y + shiftHeight){
			background2.transform.position = new Vector3 (0, Camera.main.transform.position.y - shiftHeight, 0);
		}
	}
}
