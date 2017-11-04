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
	float score;
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


	void Awake (){
	//	DontDestroyOnLoad (this);
	//	scoreTextTemp = scoreText;

	//	DisplayScore ();
	}

	void Start () {

		leftX = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width * 0, 0, 0)).x;
		rightX = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width * 1, 0, 0)).x;
		topY = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height * 1, 0)).y;
		bottomY = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height * 0, 0)).y;

		score = 0;
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
			speed = IncreaseSpeed(FindObjectOfType<ObstacleManager> ().obstacleCount);

		if (collisionFlag) {
			DisplayScore ();
			score = 0;
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
	}

	public void DisplayScore(){
		if (score > PlayerPrefs.GetFloat ("highscore")) {
			PlayerPrefs.SetFloat ("highscore", score);
		}

		highestScore = PlayerPrefs.GetFloat ("highscore", highScore);

		currentScoreText.text = score.ToString() + "\nSCORE";
		highestScoreText.text = highestScore.ToString() + "\nHIGHEST";
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
