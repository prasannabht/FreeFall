﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	UIManager UIManagerScript;
	AudioManager AudioManagerScript;
	ObstacleManager ObstacleManagerScript;

	bool shakeCam = false;
	float shakeDuration = 0.2f;
	float duration;
	public Transform camTransform;
	Vector3 initCamPos;

	//public float speed = 3f;
	float initialSpeed = 3.5f;
	float finalSpeed = 6f;
	float superSpeed = 10f;
	float slowdownSpeed = 2f;

	public bool dontMove = false;
	public bool Testing = false;

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

	public static bool showAchievement = false;
	float achievedScore = 0f;


	public static float checkpointInitScore = 50;
	public static float checkpointRepeat = 30;
	public static float nextCheckpoint;

	public static float superspeedInitScore = 70;
	public static float superspeedRepeat = 30;
	public static float nextSuperspeed;

	public static float slowdownInitScore = 90;
	public static float slowdownRepeat = 30;
	public static float nextSlowdown;

	public static float lifecoinInitScore = 20;
	public static float lifecoinRepeat = 5;
	public static float lifecoinRedeem = 20;
	public static float lifecoinCollected = 0;
 	public static float backgroundColorChange = 20f;
	public static float achievementInterval = 50;
	public static float achievementReward = 25;

//	//For testing
//	public static float checkpointInitScore = 5;
//	public static float checkpointRepeat = 10;
//	public static float nextCheckpoint;
//
//	public static float superspeedInitScore = 5;
//	public static float superspeedRepeat = 10;
//	public static float nextSuperspeed;
//
//	public static float slowdownInitScore = 5;
//	public static float slowdownRepeat = 10;
//	public static float nextSlowdown;
//
//	public static float lifecoinInitScore = 5;
//	public static float lifecoinRepeat = 2;
//	public static float lifecoinRedeem = 5;
//	//public static float lifecoinCollected = 0;  
//	public static float backgroundColorChange = 5f;
//	public static float achievementInterval = 5;
//	public static float achievementReward = 10;



	void Awake (){
		leftX = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width * 0, 0, 0)).x;
		rightX = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width * 1, 0, 0)).x;
		topY = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height * 1, 0)).y;
		bottomY = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height * 0, 0)).y;

		UIManagerScript = FindObjectOfType<UIManager>();
		AudioManagerScript = FindObjectOfType<AudioManager>();
		ObstacleManagerScript = FindObjectOfType<ObstacleManager>();

		if (!PlayerPrefs.HasKey ("achievement")) {
			PlayerPrefs.SetFloat ("achievement", 0);
		}

		if (!PlayerPrefs.HasKey ("instructionsShown")) {
			PlayerPrefs.SetInt ("instructionsShown", 0);
		}
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

		//lifecoinCollected = PlayerPrefs.GetFloat ("lifeCoins");

		initCamPos = camTransform.localPosition;
		duration = shakeDuration;
	}
	

	void Update () {

		//update speed
		speed = IncreaseSpeed(ObstacleManagerScript.obstacleCount);

		if (UIManagerScript.IsSuperSpeed)
			speed = superSpeed;

		if (UIManagerScript.IsSlowDown)
			speed = slowdownSpeed;

		if (collisionFlag) {
			AudioManagerScript.Stop("Theme");
			DisplayScore ();
			SetBallFallingFlag (false);
			UIManagerScript.GameOver ();
			shakeCam = true;
			collisionFlag = false;
		}

		if (shakeCam) {
			if (duration > 0) {
				camTransform.localPosition = initCamPos + Random.insideUnitSphere * 0.2f;
				duration -= Time.deltaTime * 1;
			} else {
				camTransform.localPosition = initCamPos;
				duration = shakeDuration;
				shakeCam = false;
			}
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
		if (score == lifecoinInitScore) {
			UIManager.enableCoinsCounter = true;
		}
		if (score > PlayerPrefs.GetFloat ("highscore")) {
			UIManager.celebrateHighScore = true;
			PlayerPrefs.SetFloat ("highscore", score);
		}
		if (score % achievementInterval == 0) {
			if (score > PlayerPrefs.GetFloat ("achievement")) {
				print ("New Achievement: " + score);
				PlayerPrefs.SetFloat ("achievement", score);
				showAchievement = true;
			}
		}
	}

	public void DisplayScore(){

		highestScore = PlayerPrefs.GetFloat ("highscore", highScore);
		scoreText.text = score.ToString();
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
