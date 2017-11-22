using UnityEngine;
using System.Collections;

public class BallBehaviour : MonoBehaviour {

	float min = -0.025f;
	float max = 0.025f;
	float wigglingSpeed = 0.25f;
	float spinSpeed = 100;
	bool soundPlayed = false;


	void Start (){

	}

	void Update(){
		//wiggling motion
		if (GameManager.IsBallFalling () || !UIManager.GameStarted()) {
			soundPlayed = false;
			transform.position = new Vector3 (Mathf.PingPong (Time.time * wigglingSpeed, max - min) + min, transform.position.y, transform.position.z);
			if (!FindObjectOfType<UIManager> ().IsSuperSpeed)
				transform.Rotate (Vector3.back * spinSpeed * Time.deltaTime);
			else
				transform.Rotate (Vector3.back * spinSpeed * Time.deltaTime * 2);
		}
	}

	void OnCollisionEnter2D(Collision2D ballCollider){

		if (!FindObjectOfType<UIManager> ().IsSuperSpeed) {
			print ("Collision");
			//play sound
			if (!soundPlayed) {
				FindObjectOfType<AudioManager> ().Play ("Collision");
				soundPlayed = true;
			}

			//stopMoving = true;
			FindObjectOfType<GameManager> ().collisionFlag = true;
		}
	}

		
}
