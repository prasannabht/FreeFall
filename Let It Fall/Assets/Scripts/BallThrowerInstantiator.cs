using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrowerInstantiator : MonoBehaviour {

	public GameObject throwingBall;

	float spawnTime = 0.5f;

	void Start () {
		InvokeRepeating ("SpawnThrowingBall", 0f, spawnTime);
	}
		
	void SpawnThrowingBall(){
		if (GameManager.IsBallFalling ()) {
			GameObject newThrowingBall = Instantiate (throwingBall, this.transform);
		}
		//Destroy (newThrowingBall, 1.5f);
	}
}
