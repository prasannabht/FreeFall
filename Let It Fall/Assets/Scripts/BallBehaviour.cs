using UnityEngine;
using System.Collections;

public class BallBehaviour : MonoBehaviour {

	Rigidbody2D ball;

	public float speed = 3f;
	float initialSpeed = 3f;
	float finalSpeed = 5f;
	public bool slowDown = false;
	float slowDownTime = 4f;

	bool stopMoving = false;
	bool isPaused = false;

	public GameObject GameOverMenu;
	public GameObject QuitMenu;

	PauseBehaviour pauseScript;
	ObstacleBehaviour obstacleScript;

	float currentScore;
	Vector2 pos;

	float min = -0.025f;
	float max = 0.025f;
	float wigglingSpeed = 0.25f;


	void Start(){
		ball = GetComponent<Rigidbody2D>();

		pauseScript = GameObject.FindObjectOfType (typeof(PauseBehaviour)) as PauseBehaviour;
		obstacleScript = GameObject.FindObjectOfType (typeof(ObstacleBehaviour)) as ObstacleBehaviour;

		PlayerPrefs.SetInt ("isMoving", 1);

		if(PlayerPrefs.GetFloat ("highscore") > 20)
			initialSpeed = 4f;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			pauseScript.pauseGame();
		}

		//update speed
		if (speed < finalSpeed)
			speed = IncreaseSpeed(obstacleScript.obstacleCount);

//		if (slowDown) {
//			//print ("Slow Down");
//			speed = speed / 1.5f;
//			transform.FindChild ("SlowMoBackground").gameObject.SetActive (true);
//			StartCoroutine (WaitAndDisableSlowDown ());
//		}

		//wiggling motion
		transform.position =new Vector3(Mathf.PingPong(Time.time*wigglingSpeed,max-min)+min, transform.position.y, transform.position.z);

	}

	void OnCollisionEnter2D(Collision2D ballCollider){
				
		stopMoving = true;

		Instantiate (GameOverMenu, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -0.05f), Quaternion.identity);
		Time.timeScale = 0;

	}

	public bool getStopMovementFlag(){
		return stopMoving;
	}

	public bool getGamePausedFlag(){
		return isPaused;
	}

	public void setGamePausedFlag(bool isPausedFlag){
		isPaused = isPausedFlag;
	}

	float IncreaseSpeed(float obstacleCount) {
		float currSpeed;
		currSpeed = initialSpeed + obstacleCount * 0.05f;

		if (currSpeed > finalSpeed)
			currSpeed = finalSpeed;

		return currSpeed;
	}

	IEnumerator WaitAndDisableSlowDown(){
		yield return new WaitForSeconds (slowDownTime);
		slowDown = false;
		transform.FindChild ("SlowMoBackground").gameObject.SetActive (false);
	}
		
}
