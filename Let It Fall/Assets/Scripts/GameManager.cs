using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	//public float speed = 3f;
	float initialSpeed = 3f;
	float finalSpeed = 6f;

	public bool dontMove = false;
	public Text scoreText;
	public Text currentScoreText;
	public Text highestScoreText;

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

	public static float checkpointInitScore = 40;
	public static float checkpointRepeat = 20;
	public static float nextCheckpoint;


	void Awake (){
		leftX = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width * 0, 0, 0)).x;
		rightX = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width * 1, 0, 0)).x;
		topY = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height * 1, 0)).y;
		bottomY = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height * 0, 0)).y;
	}

	void Start () {
		score = 0;
		tempVar = dontMove;
		nextCheckpoint = checkpointInitScore;

		if(PlayerPrefs.GetFloat ("highscore") > 20)
			initialSpeed = 4f;

		speed = initialSpeed;

		//Play theme music
		FindObjectOfType<AudioManager>().Play("Theme");
	}
	

	void Update () {
		//update speed
		if (speed < finalSpeed)
			speed = IncreaseSpeed(FindObjectOfType<ObstacleManager> ().obstacleCount);

		if (collisionFlag) {
			DisplayScore ();

			if (UIManager.checkpoint) {
				score = UIManager.currentCheckpointScore;
			} else {
				score = 0;
			}
			scoreText.text = "" + score;
			SetBallFallingFlag (false);
			//FindObjectOfType<ButtonBehaviour>().GameOverScreen();
			FindObjectOfType<UIManager>().GameOver();
			//Application.LoadLevel("Level 2");
			//collisionFlag = false;
//			FindObjectOfType<ObstacleManager> ().ResetObstacles();
//			DisplayScore ();

			collisionFlag = false;
		}
	}

	public void UpdateScore(float Increment){
		score += Increment;
		scoreText.text = "" + score;
		if (score == nextCheckpoint) {
			UIManager.enableCheckpoint = true;
		}
		if (score > PlayerPrefs.GetFloat ("highscore")) {
			UIManager.celebrateHighScore = true;
			PlayerPrefs.SetFloat ("highscore", score);
		}
	}

	public void DisplayScore(){


		highestScore = PlayerPrefs.GetFloat ("highscore", highScore);

//		currentScoreText.text = score.ToString() + "\nSCORE";
//		highestScoreText.text = highestScore.ToString() + "\nHIGHEST";
		currentScoreText.text = score.ToString();
		highestScoreText.text = highestScore.ToString();
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
