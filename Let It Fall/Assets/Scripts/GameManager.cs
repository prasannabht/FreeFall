using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	UIManager UIManagerScript;
	AudioManager AudioManagerScript;
	ObstacleManager ObstacleManagerScript;

	//public float speed = 3f;
	float initialSpeed = 3.5f;
	float finalSpeed = 6f;
	float superSpeed = 10f;
	float slowdownSpeed = 2f;

	public bool dontMove = false;
	public Text scoreText;
	public Text currentScoreText;
	public Text highestScoreText;
	public Text lifeCoinsText;

	[HideInInspector]
	public bool collisionFlag = false;
	public float score;
	float highestScore;
	public static float highScore;
	//static float obstacleCount=0;
	static float speed;
	static bool ballFallingFlag = false;
	static bool tempVar;

	public static float leftX;
	public static float rightX;
	public static float topY;
	public static float bottomY;

	public static float checkpointInitScore = 50;
	public static float checkpointRepeat = 30;
	public static float nextCheckpoint;

	public static float superspeedInitScore = 70;
	public static float superspeedRepeat = 30;
	public static float nextSuperspeed;

	public static float slowdownInitScore = 90;
	public static float slowdownRepeat = 30;
	public static float nextSlowdown;

	public static float lifecoinInitScore = 30;
	public static float lifecoinRepeat = 10;
	public static float lifecoinRedeem = 20;
	public static float lifecoinCollected = 0;


	void Awake (){
		leftX = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width * 0, 0, 0)).x;
		rightX = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width * 1, 0, 0)).x;
		topY = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height * 1, 0)).y;
		bottomY = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height * 0, 0)).y;

		UIManagerScript = FindObjectOfType<UIManager>();
		AudioManagerScript = FindObjectOfType<AudioManager>();
		ObstacleManagerScript = FindObjectOfType<ObstacleManager>();
	}

	void Start () {
		score = 0;
		tempVar = dontMove;
		nextCheckpoint = checkpointInitScore;
		nextSuperspeed = superspeedInitScore;
		nextSlowdown = slowdownInitScore;

		if(PlayerPrefs.GetFloat ("highscore") > 20)
			initialSpeed = 4f;

		speed = initialSpeed;

		lifecoinCollected = PlayerPrefs.GetFloat ("lifeCoins");
		//Play theme music
		AudioManagerScript.Play("Theme");
	}
	

	void Update () {
		//update speed
		//if (speed < finalSpeed)
		speed = IncreaseSpeed(ObstacleManagerScript.obstacleCount);
		if (lifecoinCollected == lifecoinRedeem) {
			PlayerPrefs.SetFloat ("lifeAvailable", PlayerPrefs.GetFloat ("lifeAvailable") + 1);
			PlayerPrefs.SetFloat ("lifeCoins", 0);
			lifecoinCollected = 0;
			print ("Time to redeem lifecoins");
		}

		if (UIManagerScript.IsSuperSpeed)
			speed = superSpeed;

		if (UIManagerScript.IsSlowDown)
			speed = slowdownSpeed;

		//print ("Speed: " + speed + ": flag: " + FindObjectOfType<UIManager>().IsSuperSpeed);
		if (collisionFlag) {

			//if (!FindObjectOfType<UIManager> ().IsSuperSpeed) {

				DisplayScore ();

//				if (UIManager.checkpoint) {
//					score = UIManager.currentCheckpointScore;
//				} else {
//					score = 0;
//				}
//				scoreText.text = "" + score;
				SetBallFallingFlag (false);
				//FindObjectOfType<ButtonBehaviour>().GameOverScreen();
				UIManagerScript.GameOver ();
				//Application.LoadLevel("Level 2");
				//collisionFlag = false;
//			FindObjectOfType<ObstacleManager> ().ResetObstacles();
//			DisplayScore ();

				collisionFlag = false;
			//}
		}
	}

	public void UpdateScore(float Increment){
		score += Increment;
		scoreText.text = "" + score;
		//print ("Next superspeed: " + nextSuperspeed);  
		if (score == nextCheckpoint) {
			UIManager.enableCheckpoint = true;
		}
		if (score == nextSuperspeed) {
			//print ("next superspeed ==");
			UIManager.enableSuperSpeed = true;
		}
		if (score == nextSlowdown) {
			UIManager.enableSlowDown = true;
		}
		if (score > PlayerPrefs.GetFloat ("highscore")) {
			UIManager.celebrateHighScore = true;
			PlayerPrefs.SetFloat ("highscore", score);
		}
	}

	public void DisplayScore(){

		print ("Score: " + score);
		highestScore = PlayerPrefs.GetFloat ("highscore", highScore);

//		currentScoreText.text = score.ToString() + "\nSCORE";
//		highestScoreText.text = highestScore.ToString() + "\nHIGHEST";
		scoreText.text = score.ToString();
		currentScoreText.text = score.ToString();
		highestScoreText.text = highestScore.ToString();
		lifeCoinsText.text = PlayerPrefs.GetFloat ("lifeCoins").ToString ();
	}

	public static void SetBallFallingFlag(bool flag){
		ballFallingFlag = flag;
	}
	public static bool IsBallFalling(){
		return ballFallingFlag;
	}

	public static bool StopMovement(){
		return ballFallingFlag && !tempVar;
	}


//	public static void SetObstacleCount(float count){
//		obstacleCount = count;
//	}
//	public static float GetObstacleCount(){
//		return obstacleCount;
//	}


	public static float GetSpeed(){
		return speed;
	}

	float IncreaseSpeed(float obstacleCount) {
		float currSpeed;
		currSpeed = initialSpeed + obstacleCount * 0.01f;

		if (currSpeed > finalSpeed)
			currSpeed = finalSpeed;

		return currSpeed;

	}

		
}
