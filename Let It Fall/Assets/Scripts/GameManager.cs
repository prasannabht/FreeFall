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

	public bool collisionFlag = false;
	static float score = 0;
	static float highestScore;
	public static float highScore;
	static float obstacleCount=0;
	static float speed;
	static bool ballFallingFlag = false;
	static bool tempVar;

	void Awake (){
		DontDestroyOnLoad (this);
	}

	void Start () {
		print ("Score: " + score);
		print ("HighScore: " + highestScore);
		DisplayScore ();


		tempVar = dontMove;

		if(PlayerPrefs.GetFloat ("highscore") > 20)
			initialSpeed = 4f;

		speed = initialSpeed;

		//Play theme music
		FindObjectOfType<AudioManager>().Play("Theme");
	}
	

	void Update () {
		//update speed
		if (speed < finalSpeed)
			speed = IncreaseSpeed(obstacleCount);

		if (collisionFlag) {
			SetBallFallingFlag (false);
			//FindObjectOfType<ButtonBehaviour>().GameOverScreen();

			//collisionFlag = false;
			collisionFlag = false;
			Application.LoadLevel("Level 2");
		}
	}

	public void UpdateScore(float Increment){
		score += Increment;
		scoreText.text = "" + score;
	}

	void DisplayScore(){
		if (score > PlayerPrefs.GetFloat ("highscore")) {
			PlayerPrefs.SetFloat ("highscore", score);
		}

		highestScore = PlayerPrefs.GetFloat ("highscore", highScore);

		//print (currentScore.ToString () + "\nScore");
		currentScoreText.text = score.ToString() + "\nScore";
		highestScoreText.text = highestScore.ToString() + "\nHighest";
	}

	public static void SetBallFallingFlag(bool flag){
		ballFallingFlag = flag;
	}
	public static bool IsBallFalling(){
		return ballFallingFlag && !tempVar;
	}


	public static void SetObstacleCount(float count){
		obstacleCount = count;
	}
	public static float GetObstacleCount(){
		return obstacleCount;
	}


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
