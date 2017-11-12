using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBallsBehaviour : MonoBehaviour {

	public GameObject FallingBall;

	GameObject currFallingBall;
	float minTime = 2;
	float maxTime = 6;
	float currSpeed = 4;
	float time;
	float spawnTime;
	float ballLifeTime = 1;
	float spawnDuration;
	float minAlpha = 0.1f;
	float maxAlpha = 0.4f;

	void Start () {
		SetRandomTime ();
	}

	void FixedUpdate(){
		time += Time.deltaTime;
		spawnDuration += Time.deltaTime;

		if (time > spawnTime) {
			SpawnObject ();
			SetRandomTime ();
		}

		if (currFallingBall) {
			currFallingBall.transform.Translate (0, -Time.deltaTime * currSpeed, 0, Space.World);
		}

		if (spawnDuration > ballLifeTime) {
			Destroy (currFallingBall);
		}
	}

	void SetRandomTime(){
		spawnTime = Random.Range (minTime, maxTime);
	}

	void SpawnObject(){
		time = 0;
		spawnDuration = 0;
		currFallingBall = Instantiate (FallingBall, new Vector3(Random.Range(GameManager.leftX, GameManager.rightX), GameManager.topY, 0.1f), Quaternion.identity);
		currFallingBall.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, Random.Range(minAlpha, maxAlpha));
	}
}
