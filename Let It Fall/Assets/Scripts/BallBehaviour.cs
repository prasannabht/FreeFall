using UnityEngine;
using System.Collections;

public class BallBehaviour : MonoBehaviour {

	float min = -0.025f;
	float max = 0.025f;
	float wigglingSpeed = 0.25f;

	bool soundPlayed = false;

	void Start (){
		transform.position = new Vector3 (0, GameManager.topY - 1, 0);
	}

	void Update(){
		//wiggling motion
		if (GameManager.IsBallFalling () || !UIManager.GameStarted()) {
			soundPlayed = false;
			transform.position = new Vector3 (Mathf.PingPong (Time.time * wigglingSpeed, max - min) + min, transform.position.y, transform.position.z);
		}
	}

	void OnCollisionEnter2D(Collision2D ballCollider){

		//play sound
		if (!soundPlayed) {
			FindObjectOfType<AudioManager>().Play("Collision");
			soundPlayed = true;
		}

		//stopMoving = true;
		FindObjectOfType<GameManager>().collisionFlag = true;
	}

		
}
